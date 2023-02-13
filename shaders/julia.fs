#version 400

uniform vec2 iResolution;
uniform float iGlobalTime;
uniform vec2 iMin;
uniform vec2 iMax;
uniform int iIterations;
uniform float iCX;  // real component of parameter c
uniform float iCY;  // imaginary component of parameter c

out vec4 FragColor;

/* Compute the filled Julia set for the function Q_c(z) = z^2 + c,
 * where complex number z = x + iy, given components of c
 * 
 * Filled Julia set (for Q_c): the set of points whose orbits are bounded
 */
void computeSet(out vec4 fragColor, in vec2 p) {
  float xfrac, yfrac, x, y, xtmp;
  int iter;

  // translate coordinates to center mandelbrot object
  xfrac = p.x / iResolution.x;
  yfrac = p.y / iResolution.y;
  // this coordinate on the "graph" will be the seed
  x = iMin.x + xfrac * (iMax.x - iMin.x);
  y = iMin.y + yfrac * (iMax.y - iMin.y);

  iter = 0;
  // square both to avoid using sqrt
  float cutoff = max(iCX*iCX + iCY*iCY, 2*2);
  while (iter < iIterations && x*x + y*y < cutoff) {
    xtmp = x*x - y*y + iCX;
    y = 2*x*y + iCY;
    x = xtmp;
    iter++;
  }
  
  if (iter < iIterations) {
    // escaped
    // raising to a power makes higher iterations brighter
    // fragColor.xyz = pow(vec3(0, iter, iter), vec3(2.2f)) / vec3(iIterations);
    
    int section = iIterations / 5;
    if (iter < section) {
      fragColor.xyz = vec3(1, float(iter % section) / section, 0);
    } else if (iter < section * 2) {
      fragColor.xyz = vec3(1 - (float(iter % section) / section), 1, 0);
    } else if (iter < section * 3) {
      fragColor.xyz = vec3(0, 1, float(iter % section) / section);
    } else if (iter < section * 4) {
      fragColor.xyz = vec3(0, 1 - (float(iter % section) / section), 1);
    } else {
      fragColor.xyz = vec3(float(iter % section) / section, 0, 1);
    }
  } else {
    // did not escape, use color black
    fragColor.xyz = vec3(0, 0, 0);
  }
}

void main() {
  vec2 p = gl_FragCoord.xy;  // a 2D coordinate point
  vec4 fragColor = vec4(p.xy,0,1);
  computeSet(fragColor, p);
  FragColor = fragColor;
}
