#version 400

uniform vec2 iResolution;
uniform float iGlobalTime;
uniform vec2 iMin;
uniform vec2 iMax;
uniform int iIterations;

out vec4 FragColor;

/* Recall that complex quadratic function is Q(z) = z^2 + c,
 * where complex number z = x + iy
 */
void computeMandelbrot(out vec4 fragColor, in vec2 p) {
  float xfrac, yfrac, x0, y0, x, y, xtmp;
  int iter;

  // translate coordinates to center mandelbrot object
  xfrac = p.x / iResolution.x;
  yfrac = p.y / iResolution.y;
  x0 = iMin.x + xfrac * (iMax.x - iMin.x);
  y0 = iMin.y + yfrac * (iMax.y - iMin.y);

  // start at z = 0 + 0i
  x = 0;
  y = 0;
  iter = 0;
  while (iter < iIterations && x*x + y*y < 2*2) {
    xtmp = x*x - y*y + x0;
    y = 2*x*y + y0;
    x = xtmp;
    iter++;
  }
  
  if (iter < iIterations) {
    // escaped
    fragColor.xyz = pow(vec3(iter), vec3(2.2f)) / vec3(iIterations);
  } else {
    // did not escape, use color black
    fragColor.xyz = vec3(0, 0, 0);
  }
}

void main() {
  vec2 p = gl_FragCoord.xy;  // a 2D coordinate point
  vec4 fragColor = vec4(p.xy,0,1);
  computeMandelbrot(fragColor, p);
  FragColor = fragColor;
}
