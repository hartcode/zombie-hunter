#include <display.h>
#include <start.h>
#include <engine.h>

int main(int argc, char** argv) {
Display display = Display();
  //start_screen(&display);
  game_loop(&display);

  return 0;
};
