#include <engine.h>
#include <display.h>
#include <terrainmap.h>
#include <input.h>
#include <string.h>
#include <iostream>
#include <stdio.h>
using namespace std;

int lastrows = 0;
int lastcols = 0;
int viewX = 0;
int viewY = 0;

#define BUFFER_SIZE 256
#define MAP_WIDTH 20
#define MAP_HEIGHT 20
#define TREE_COUNT 10
#define GRAVE_COUNT 10

void game_loop(void) {
  TerrainMap map = TerrainMap(MAP_WIDTH, MAP_HEIGHT, TREE_COUNT, GRAVE_COUNT);
  draw(&map, viewX, viewY);
  int key = getkey();
  while (key != ESCAPE_KEY) {
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
    key = getkey();
  }
}

void draw(TerrainMap * const map, int viewX, int viewY) {
  int rows = getRows();
  int cols = getCols();
  int viewableRows = rows-2;
  if (rows != lastrows || cols != lastcols) {
    // we have a window size change
  }
  clear();
  cout << "Rows " << rows << " Cols " << cols << " ViewX " << viewX << " ViewY " << viewY << endl;
  char buffer[BUFFER_SIZE];
  memset(&buffer[0], 0, BUFFER_SIZE);
  for(int y = 0; y < viewableRows; y++) {
    map->getRow(viewX ,viewY + y,cols,viewableRows,&buffer[0],BUFFER_SIZE);
    printf("%s",buffer);
  }

  lastrows = rows;
  lastcols = cols;
}
