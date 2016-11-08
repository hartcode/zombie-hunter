#include <engine.h>
#include <display.h>
#include <terrainmap.h>
#include <input.h>
#include <avatar.h>

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

#define MAP_WIDTH 10
#define MAP_HEIGHT 10
#define BUFFER_SIZE 65536

#define TREE_COUNT MAP_WIDTH
#define GRAVE_COUNT MAP_HEIGHT

void game_loop(void) {
  // setup random numbers
  srand(time(NULL));

  TerrainMap map = TerrainMap(MAP_WIDTH, MAP_HEIGHT, TREE_COUNT, GRAVE_COUNT);
  viewY = ((getRows() - MAP_HEIGHT)/2) * -1;
  viewX = ((getCols() - MAP_WIDTH)/2) * -1;

  Avatar player = Avatar(MAP_WIDTH/2, MAP_HEIGHT/2);

  draw(&player, &map, viewX, viewY);

  int key = getkey();
  bool change = false;
  while (key != ESCAPE_KEY) {
    if (key > 0) {
       switch(key) {
         case LEFT_KEY:
           if (map.getAt(player.getY(), player.getX() - 1) == CHAR_EMPTY) {
             viewX--;
             player.setX(player.getX() - 1);
             change = true;
           }
         break;
         case RIGHT_KEY:
           if (map.getAt(player.getY(), player.getX() + 1) == CHAR_EMPTY) {
             viewX++;
             player.setX(player.getX() + 1);
             change = true;
           }
         break;
         case UP_KEY:
           if (map.getAt(player.getY() - 1, player.getX()) == CHAR_EMPTY) {
             viewY--;
             player.setY(player.getY() - 1);
             change = true;
           }
         break;
         case DOWN_KEY:
           if (map.getAt(player.getY() + 1, player.getX()) == CHAR_EMPTY) {
             viewY++;
             player.setY(player.getY() + 1);
             change = true;
           }
         break;
       }
      if (change) {
        change = false;
       draw(&player, &map, viewX, viewY);
      }
    }
    key = getkey();
  }
}

void draw(Avatar * const avatar, TerrainMap * const map, int viewX, int viewY) {
  unsigned int rows = getRows();
  unsigned int cols = getCols();
  unsigned int viewableRows = rows-2;
  if (rows != lastrows || cols != lastcols) {
    viewY = ((getRows() - MAP_HEIGHT)/2) * -1;
    viewX = ((getCols() - MAP_WIDTH)/2) * -1;
  }
  clear();
  cout << "Rows " << rows << " Cols " << cols << " ViewX " << viewX << " ViewY " << viewY << " Px " << avatar->getX() << " Py " << avatar->getY() << endl;
  char buffer[BUFFER_SIZE];
  memset(&buffer[0], 0, BUFFER_SIZE);
  char * ptr = &buffer[0];
  for(unsigned int y = 0; y < viewableRows; y++) {
    *ptr = ' ';
    ptr++;
      for (unsigned int x = 1; x < cols-1; x++) {
        if (avatar->getX() == viewX + x && avatar->getY() == viewY + y) {
          *ptr = avatar->getCharacter();
        } else {
          *ptr = map->getAt(viewY + y, viewX + x);
        }
        ptr++;
      }
      *ptr = '\n';
      ptr++;
  }
  cout << buffer;
  lastrows = rows;
  lastcols = cols;
}
