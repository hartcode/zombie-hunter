#include <display.h>
#include <start.h>
#include <engine.h>

int main(int argc, char** argv) {
  Input input = Input();
  Display display = Display(&input);
  //start_screen(&display, &input);
  game_loop(&display, &input);

  return 0;
};
