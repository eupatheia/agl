#include "agl/window.h"

using glm::vec3;
class MyWindow : public agl::Window {
  void setup() {
    setupPerspectiveScene(vec3(0.0), vec3(10.0));
  }

  void draw() {
    renderer.scale(vec3(10.0f));
    renderer.plane();
  }

  void keyUp(int key, int mods) {
    if (key == 'C') {
      std::cout << "Toggle camera controls " << getCameraEnabled() << std::endl;
      setCameraEnabled(!getCameraEnabled());
    }
  }
};

int main() {
  MyWindow window;
  window.run();
}
