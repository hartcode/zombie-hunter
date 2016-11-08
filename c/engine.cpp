#include <engine.h>
#include <display.h>
#include <terrainmap.h>
#include <input.h>
#include <string.h>
#include <iostream>
#include <stdio.h>
#include <stdlib.h>
#include <time.h>
using namespace std;

unsigned int lastrows = 0;
unsigned int lastcols = 0;
int viewX = 0;
int viewY = 0;

#define MAP_WIDTH 100
#define MAP_HEIGHT 100
#define BUFFER_SIZE MAP_WIDTH * MAP_HEIGHT

#define TREE_COUNT 100
#define GRAVE_COUNT 100

void game_loop(void) {
  // setup random numbers
  srand(time(NULL));

  TerrainMap map = TerrainMap(MAP_WIDTH, MAP_HEIGHT, TREE_COUNT, GRAVE_COUNT);
  draw(&map, viewX, viewY);

  int key = getkey();
  while (key != ESCAPE_KEY) {
    if (key > 0) {
       switch(key) {
         case LEFT_KEY:
           viewX--;
         break;
         case RIGHT_KEY:
           viewX++;
         break;
         case UP_KEY:
           viewY--;
         break;
         case DOWN_KEY:
           viewY++;
         break;
       }

      draw(&map, viewX, viewY);
    }
    key = getkey();
  }
}

void draw(TerrainMap * const map, int viewX, int viewY) {
  unsigned int rows = getRows();
  unsigned int cols = getCols();
  unsigned int viewableRows = rows-2;
  if (rows != lastrows || cols != lastcols) {
    // we have a window size change
  }
  clear();
  cout << "Rows " << rows << " Cols " << cols << " ViewX " << viewX << " ViewY " << viewY << endl;
  char buffer[BUFFER_SIZE];
  memset(&buffer[0], 0, BUFFER_SIZE);
  for(unsigned int y = 0; y < viewableRows; y++) {
      for (unsigned int x = 0; x < cols; x++) {
        buffer[y*cols + x] = map->getAt(viewY + y, viewX + x);
      }
  }
  cout << buffer;
  lastrows = rows;
  lastcols = cols;
}
