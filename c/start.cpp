#include <start.h>
#include <display.h>
#include <input.h>
#include <iostream>
using namespace std;

void start_screen() {
  clear();

  unsigned int rows = getRows();
  unsigned int cols = getCols();
  CONSOLE_OUT << endl;
  bool first = false;
  for (unsigned int x = 1; x < rows-1; x++) {
    CONSOLE_OUT <<' ';
    for (unsigned int y = 1; y < cols-1; y++) {
      if (x == rows / 2) {
        if (y > cols /2 - ((21/2)+1) && y < cols /2 + ((21/2)+1)) {
          if (!first) {
            CONSOLE_OUT << "  Zombie Hunter 100  ";
            first = true;
          }
        } else {
          CONSOLE_OUT << (char)219;
        }
      } else {
        CONSOLE_OUT << (char)219;
      }
    }
    CONSOLE_OUT << endl;
  }
  while (getkey() != FIRE_KEY);
  clear();

  CONSOLE_OUT << "The year is 2016 and the human race has fallen to zombies." << endl << endl;
  sleepy(1000);
  CONSOLE_OUT << "In a desperate attempt to save humanity an advanced Artificial Intelligence was developed in hopes of finding a cure." << endl << endl;
  sleepy(2000);
  CONSOLE_OUT << "A cure was found, but it was too late, the last humans have become mindless zombies." << endl << endl;
  sleepy(1000);
  CONSOLE_OUT << "With the humans gone, the Artificial Intelligence created 'Zombie Hunter' class robots to deliver the cure to the humans." << endl << endl;
  sleepy(2000);
  CONSOLE_OUT << "As a Zombie Hunter it's up to YOU to save humanity." << endl;
  CONSOLE_OUT << endl << "Press Space to Continue" << endl;
  while (getkey() != FIRE_KEY);
}
