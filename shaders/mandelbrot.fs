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
  // compute c-value associated with this point on the plane, c = x0 + iy0
  x0 = iMin.x + xfrac * (iMax.x - iMin.x);  // real component
  y0 = iMin.y + yfrac * (iMax.y - iMin.y);  // imaginary component

  // start at z = 0 + 0i
  x = 0;
  y = 0;
  iter = 0;
  // recall modulus of z written as |z| = sqrt(x^2 + y^2)
  // must have |z| <= 2
  // (square modulus function to confirm loop condition))
  while (iter < iIterations && x*x + y*y < 2*2) {
    xtmp = x*x - y*y + x0;  // real component of next z-val
    y = 2*x*y + y0;  // imaginary component of next z-val
    x = xtmp;
    iter++;
  }
  
  if (iter < iIterations) {
    // escaped
    // raising to a power makes lower iterations brighter
    fragColor.xyz = pow(vec3(iter), vec3(2.2f)) / vec3(iIterations);
    
    // int cutoff = iIterations / 5;
    // if (iter < cutoff) {
    //   fragColor.xyz = vec3(1, float(iter) / cutoff, 0);
    // } else if (iter < cutoff * 2) {
    //   fragColor.xyz = vec3(1 - (float(iter) / cutoff), 1, 0);
    // } else if (iter < cutoff * 3) {
    //   fragColor.xyz = vec3(0, 1, float(iter) / cutoff);
    // } else if (iter < cutoff * 4) {
    //   fragColor.xyz = vec3(0, 1 - (float(iter) / cutoff), 1);
    // } else {
    //   fragColor.xyz = vec3(float(iter) / cutoff, 0, 1);
    // }
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
