#include <engine.h>
#include <display.h>
#include <iostream>
using namespace std;

void draw1(void) {
  int rows = getRows();
  int cols = getCols();
  clear();
  cout << "Rows " << rows << " Cols " << cols << endl;
}
