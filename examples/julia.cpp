// Copyright 2020, Savvy Sine, Aline Normoyle, JL

#include "agl/window.h"

using glm::vec2;
using glm::vec3;

class MyWindow : public agl::Window {
  void setup() {
    setWindowSize(600, 400);
    setCameraEnabled(false);
    lookAt(vec3(0), vec3(0, 0, -2));
    ortho(0, width(), 0, height(), -1, 1);
    background(vec3(0));

    renderer.loadShader("shadertoy",
        "../shaders/shadertoy.vs",
        "../shaders/julia.fs");  // change shader here
  }

  void draw() {
    renderer.beginShader("shadertoy");
    renderer.setUniform("iGlobalTime", elapsedTime());
    renderer.setUniform("iResolution", vec2(width(), height()));
    renderer.setUniform("iMin", vec2(-2.0f, -1.12f));
    renderer.setUniform("iMax", vec2(2.0f, 1.12f));
    renderer.setUniform("iIterations", 100);
    renderer.setUniform("iCX", -0.75f);  // real component of parameter c
    renderer.setUniform("iCY", 0.1f);  // imaginary component of parameter c
    renderer.translate(vec3(width()*0.5, height()*0.5, 0.0));
    renderer.scale(vec3(width(), height(), 1.0f));
    renderer.rotate(kPI/2, vec3(1, 0, 0));
    renderer.plane();
    renderer.endShader();
  }

  void resize(int width, int height) {
    ortho(0, width, 0, height, -1, 1);
  }
};

int main() {
  MyWindow window;
  window.run();
}
