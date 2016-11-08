#include <display.h>
#include <iostream>
using namespace std;

int main(int argc, char** argv) {
  int rows = getRows();
  int cols = getCols();
  cout << "Rows " << rows << " Cols " << cols << endl;
  return 0;
};
