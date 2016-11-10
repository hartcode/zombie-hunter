#include <start.h>
#include <display.h>
#include <input.h>
#include <iostream>
using namespace std;

void start_screen() {
  clear();
  cout << "Booting";
  for (int i = 0; i < 3; i++) {
  sleep(100);
  cout << '.';
  }
  cout << endl << "Online" << endl << endl << endl;
  sleep(500);
  clear();

  unsigned int rows = getRows();
  unsigned int cols = getCols();
  cout << endl;
  bool first = false;
  for (unsigned int x = 1; x < rows-1; x++) {
    cout <<' ';
    for (unsigned int y = 1; y < cols-1; y++) {
      if (!first && x == rows / 2) {
        if (!first && y > 10 && y < cols - 10) {
          cout << "Zombie Hunter 100";
          first = true;
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

}
