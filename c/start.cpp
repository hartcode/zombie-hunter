#include <start.h>
#include <display.h>
#include <input.h>
#include <iostream>
using namespace std;

void start_screen() {
  clear();

  unsigned int rows = getRows();
  unsigned int cols = getCols();
  cout << endl;
  bool first = false;
  for (unsigned int x = 1; x < rows-1; x++) {
    cout <<' ';
    for (unsigned int y = 1; y < cols-1; y++) {
      if (x == rows / 2) {
        if (y > cols /2 - ((21/2)+1) && y < cols /2 + ((21/2)+1)) {
          if (!first) {
            cout << "  Zombie Hunter 100  ";
            first = true;
          }
        } else {
          cout << (char)219;
        }
      } else {
        cout << (char)219;
      }
    }
    cout << endl;
  }
  getkey();
  clear();

  cout << "The year is 2016 and the human race has fallen to zombies." << endl << endl;
  sleep(1000);
  cout << "In a desperate attempt to save humanity an advanced Artificial Intelligence was developed in hopes of finding a cure." << endl << endl;
  sleep(2000);
  cout << "A cure was found, but it was too late, the last humans have become mindless zombies." << endl << endl;
  sleep(1000);
  cout << "With the humans gone, the Artificial Intelligence created 'Zombie Hunter' class robots to deliver the cure to the humans." << endl << endl;
  sleep(2000);
  cout << "As a Zombie Hunter it's up to YOU to save humanity." << endl;
  cout << endl << "Press Space to Continue" << endl;
  while (getkey() != FIRE_KEY); {
    sleep(100);
  }
}
