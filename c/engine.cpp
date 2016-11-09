#include <engine.h>
#include <display.h>
#include <terrainmap.h>
#include <input.h>
#include <avatar.h>
#include <baddie.h>

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

#define AVATAR_COUNT 2

void game_loop(void) {
  // setup random numbers
  srand(time(NULL));

  TerrainMap map = TerrainMap(MAP_WIDTH, MAP_HEIGHT, TREE_COUNT, GRAVE_COUNT);
  viewY = ((getRows() - MAP_HEIGHT)/2) * -1;
  viewX = ((getCols() - MAP_WIDTH)/2) * -1;

  Avatar ** avatars = new Avatar*[AVATAR_COUNT];

  //Avatar avatars[AVATAR_COUNT] = Avatar(MAP_WIDTH/2, MAP_HEIGHT/2);
  avatars[0] = new Avatar(MAP_WIDTH/2, MAP_HEIGHT/2);
  avatars[1] = new Baddie(0, 0);

  //Baddie baddie = Baddie(MAP_WIDTH/2, MAP_HEIGHT/2);
  Avatar *player = avatars[0];

  map.setAt(player->getX(), player->getY(), TERRAIN_PLAYER);
  map.setAt(avatars[1]->getX(), avatars[1]->getY(), TERRAIN_BADDIE);

  draw(avatars, AVATAR_COUNT, &map, viewX, viewY);

  int key = getkey();
  bool change = false;
  while (key != ESCAPE_KEY) {
    if (key > 0) {
       switch(key) {
         case LEFT_KEY:
           if (map.getAt(player->getY(), player->getX() - 1) == TERRAIN_EMPTY) {
             viewX--;
             map.setAt(player->getX(), player->getY(), TERRAIN_EMPTY);
             player->setX(player->getX() - 1);
             map.setAt(player->getX(), player->getY(), TERRAIN_PLAYER);
             change = true;
           }
         break;
         case RIGHT_KEY:
           if (map.getAt(player->getY(), player->getX() + 1) == TERRAIN_EMPTY) {
             viewX++;
             map.setAt(player->getX(), player->getY(), TERRAIN_EMPTY);
             player->setX(player->getX() + 1);
             map.setAt(player->getX(), player->getY(), TERRAIN_PLAYER);
             change = true;
           }
         break;
         case UP_KEY:
           if (map.getAt(player->getY() - 1, player->getX()) == TERRAIN_EMPTY) {
             viewY--;
             map.setAt(player->getX(), player->getY(), TERRAIN_EMPTY);
             player->setY(player->getY() - 1);
             map.setAt(player->getX(), player->getY(), TERRAIN_PLAYER);
             change = true;
           }
         break;
         case DOWN_KEY:
           if (map.getAt(player->getY() + 1, player->getX()) == TERRAIN_EMPTY) {
             viewY++;
             map.setAt(player->getX(), player->getY(), TERRAIN_EMPTY);
             player->setY(player->getY() + 1);
             map.setAt(player->getX(), player->getY(), TERRAIN_PLAYER);
             change = true;
           }
         break;
       }
      if (change) {
        change = false;
       draw(avatars, AVATAR_COUNT, &map, viewX, viewY);
      }
    }
    key = getkey();
  }
  for (unsigned int avatarIndex = 0; avatarIndex < AVATAR_COUNT; avatarIndex++) {
    delete avatars[avatarIndex];
  }
  delete avatars;
  clear();
}

void draw(Avatar ** avatar, unsigned int avatarCount, TerrainMap * const map, int viewX, int viewY) {
  unsigned int rows = getRows();
  unsigned int cols = getCols();
  unsigned int viewableRows = rows-2;
  if (rows != lastrows || cols != lastcols) {
    viewY = ((getRows() - MAP_HEIGHT)/2) * -1;
    viewX = ((getCols() - MAP_WIDTH)/2) * -1;
  }
  // Clear Screen
  clear();
  // Draw HEADER
  cout << "Rows " << rows << " Cols " << cols << " ViewX " << viewX << " ViewY " << viewY << " Px " << avatar[0]->getX() << " Py " << avatar[0]->getY() << endl;
  // Draw BOARD
  char buffer[BUFFER_SIZE];
  memset(&buffer[0], 0, BUFFER_SIZE);
  char * ptr = &buffer[0];
  for(unsigned int y = 0; y < viewableRows; y++) {
    *ptr = CHAR_EMPTY;
    ptr++;
      for (unsigned int x = 1; x < cols-1; x++) {
        char terrainChar = map->getCharacterAt(viewY + y, viewX + x);
        for (unsigned int avatarIndex = 0; avatarIndex < AVATAR_COUNT; avatarIndex++) {
          if(avatar[avatarIndex]->getX() == x + viewX && avatar[avatarIndex]->getY() == y + viewY) {
            terrainChar = avatar[avatarIndex]->getCharacter();
          }
        }
        *ptr = terrainChar;
        ptr++;
      }
      *ptr = '\n';
      ptr++;
  }
  cout << buffer;
  lastrows = rows;
  lastcols = cols;
}
