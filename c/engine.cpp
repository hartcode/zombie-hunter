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

#define MAP_WIDTH 100
#define MAP_HEIGHT 100
#define BUFFER_SIZE 65536

#define TREE_COUNT MAP_WIDTH
#define GRAVE_COUNT MAP_HEIGHT

#define BADDIE_INITIAL_COUNT 10
#define BADDIE_MAX_COUNT 100
#define AVATAR_COUNT 1 + BADDIE_MAX_COUNT
#define AVATAR_START_X MAP_HEIGHT/2
#define AVATAR_START_Y MAP_WIDTH/2

const int BADDIE_SPAWN_STEP = 100; // 1 new Baddie spawns every 10 seconds 

unsigned int avatar_count = 0;

void game_loop(void) {
  int viewX = 0;
  int viewY = 0;
  bool change = false;
  bool cancel = false;
  int key = 0;
  int baddieStep = 0;

  // setup random numbers
  srand(time(NULL));

  TerrainMap map = TerrainMap(MAP_WIDTH, MAP_HEIGHT, TREE_COUNT, GRAVE_COUNT);

  // this is the first calculation :)
  recalculateViewPosition(&viewX, &viewY);

  Avatar ** avatars = new Avatar*[AVATAR_COUNT];


  // Create player
  avatar_count = 0;
  avatars[avatar_count] = new Avatar(AVATAR_START_X, AVATAR_START_Y);
  map.setAt(avatars[avatar_count]->getX(), avatars[avatar_count]->getY(), TERRAIN_PLAYER);
  Avatar *player = avatars[avatar_count];
  avatar_count++;

  // Create Baddies
  for (int baddieIndex = 0; baddieIndex < BADDIE_INITIAL_COUNT; baddieIndex++)
  {
    if (avatar_count < AVATAR_COUNT) {
      int baddieX = player->getX() + (rand()%30)-15;
      int baddieY = player->getY() + (rand()%30)-15;
      while(map.getAt(baddieX,baddieY) != TERRAIN_EMPTY)
      {
        baddieX = player->getX() + (rand()%30)-15;
        baddieY = player->getY() + (rand()%30)-15;
      }
      avatars[avatar_count] = new Baddie(baddieX, baddieY);
      map.setAt(avatars[avatar_count]->getX(), avatars[avatar_count]->getY(), TERRAIN_BADDIE);
      avatar_count++;
    }
  }

  // Draw initial screen
  draw(avatars, avatar_count, &map, &viewX, &viewY);

  // Main Loop
  while (!cancel) {
    baddieStep++;
    if (baddieStep >= BADDIE_SPAWN_STEP && avatar_count < AVATAR_COUNT) {
      int baddieX = player->getX() + (rand()%30)-15;
      int baddieY = player->getY() + (rand()%30)-15;
      while(map.getAt(baddieX,baddieY) != TERRAIN_EMPTY)
      {
        baddieX = player->getX() + (rand()%30)-15;
        baddieY = player->getY() + (rand()%30)-15;
      }
      avatars[avatar_count] = new Baddie(baddieX, baddieY);
      map.setAt(avatars[avatar_count]->getX(), avatars[avatar_count]->getY(), TERRAIN_BADDIE);
      avatar_count++;
      baddieStep = 0;
    }
    key = getkey();
    if (key > 0) {
       switch(key) {
         case LEFT_KEY:
           if (map.getAt(player->getX(), player->getY() - 1) == TERRAIN_EMPTY) {
             viewY--;
             map.setAt(player->getX(), player->getY(), TERRAIN_EMPTY);
             player->setY(player->getY() - 1);
             map.setAt(player->getX(), player->getY(), TERRAIN_PLAYER);
             change = true;
           }
           player->setDirection(AVATAR_DIRECTION_LEFT);
         break;
         case RIGHT_KEY:
           if (map.getAt(player->getX(), player->getY() + 1) == TERRAIN_EMPTY) {
             viewY++;
             map.setAt(player->getX(), player->getY(), TERRAIN_EMPTY);
             player->setY(player->getY() + 1);
             map.setAt(player->getX(), player->getY(), TERRAIN_PLAYER);
             change = true;
           }
           player->setDirection(AVATAR_DIRECTION_RIGHT);
         break;
         case UP_KEY:
           if (map.getAt(player->getX() - 1, player->getY()) == TERRAIN_EMPTY) {
             viewX--;
             map.setAt(player->getX(), player->getY(), TERRAIN_EMPTY);
             player->setX(player->getX() - 1);
             map.setAt(player->getX(), player->getY(), TERRAIN_PLAYER);
             change = true;
           }
           player->setDirection(AVATAR_DIRECTION_UP);
         break;
         case DOWN_KEY:
           if (map.getAt(player->getX() + 1, player->getY()) == TERRAIN_EMPTY) {
             viewX++;
             map.setAt(player->getX(), player->getY(), TERRAIN_EMPTY);
             player->setX(player->getX() + 1);
             map.setAt(player->getX(), player->getY(), TERRAIN_PLAYER);
             change = true;
           }
           player->setDirection(AVATAR_DIRECTION_DOWN);
         break;
         case FIRE_KEY:
           switch(player->getDirection())
           {
             case AVATAR_DIRECTION_LEFT:
               for (unsigned int avatarIndex = 1; avatarIndex < avatar_count; avatarIndex++) {
                 if(avatars[avatarIndex]->getX() == player->getX() && avatars[avatarIndex]->getY() == player->getY() - 1) {
                   ((Baddie*)avatars[avatarIndex])->turnHuman();
                   change = true;
                 }
               }
             break;
             case AVATAR_DIRECTION_RIGHT:
               for (unsigned int avatarIndex = 1; avatarIndex < avatar_count; avatarIndex++) {
                 if(avatars[avatarIndex]->getX() == player->getX() && avatars[avatarIndex]->getY() == player->getY() + 1) {
                   ((Baddie*)avatars[avatarIndex])->turnHuman();
                   change = true;
                 }
               }
             break;
             case AVATAR_DIRECTION_UP:
             for (unsigned int avatarIndex = 1; avatarIndex < avatar_count; avatarIndex++) {
               if(avatars[avatarIndex]->getX() == player->getX() - 1 && avatars[avatarIndex]->getY() == player->getY()) {
                 ((Baddie*)avatars[avatarIndex])->turnHuman();
                 change = true;
               }
             }
             break;
             case AVATAR_DIRECTION_DOWN:
             for (unsigned int avatarIndex = 1; avatarIndex < avatar_count; avatarIndex++) {
               if(avatars[avatarIndex]->getX() == player->getX() + 1 && avatars[avatarIndex]->getY() == player->getY()) {
                 ((Baddie*)avatars[avatarIndex])->turnHuman();
                 change = true;
               }
             }
             break;
           }
         break;
       }
      cancel = key == ESCAPE_KEY;
    }
    for (unsigned int avatarIndex = 0; avatarIndex < avatar_count; avatarIndex++) {
      change |= avatars[avatarIndex]->update(&map);
    }
    if (change) {
     change = false;
     draw(avatars, avatar_count, &map, &viewX, &viewY);
    }
  } // Main loop


  // Cleanup avatars
  for (unsigned int avatarIndex = 0; avatarIndex < avatar_count; avatarIndex++) {
    delete avatars[avatarIndex];
  }
  delete avatars;
  clear();
}

void draw(Avatar ** avatar, unsigned int avatarCount, TerrainMap * const map, int * viewX, int * viewY) {
  unsigned int rows = getRows();
  unsigned int cols = getCols();
  unsigned int viewableRows = rows-2;

  if (rows != lastrows || cols != lastcols) {
    recalculateViewPosition(viewX, viewY);
  }

  // Clear Screen
  clear();
  // Draw HEADER
  cout << "Rows " << rows << " Cols " << cols << " ViewX " << *viewX << " ViewY " << *viewY << " Px " << avatar[0]->getX() << " Py " << avatar[0]->getY() << endl;
  // Draw BOARD
  char buffer[BUFFER_SIZE];
  memset(&buffer[0], 0, BUFFER_SIZE);
  char * ptr = &buffer[0];
  for(unsigned int x = 0; x < viewableRows; x++) {
    *ptr = CHAR_EMPTY;
    ptr++;
      for (unsigned int y = 1; y < cols-1; y++) {
        char terrainChar = map->getCharacterAt(*viewX + x, *viewY + y);
        for (unsigned int avatarIndex = 0; avatarIndex < avatarCount; avatarIndex++) {
          if(avatar[avatarIndex]->getX() == x + *viewX && avatar[avatarIndex]->getY() == y + *viewY) {
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

void recalculateViewPosition(int * viewX, int * viewY)
{
  if ((int)getRows() - MAP_HEIGHT > 0) {
    *viewX = (((int)getRows() - MAP_HEIGHT)/2) * -1;
  } else {
    *viewX =  AVATAR_START_X - (int)getRows()/2;
  }
  if ((int)getCols() - MAP_WIDTH > 0) {
    *viewY = (((int)getCols() - MAP_WIDTH)/2) * -1;
  } else {
    *viewY = AVATAR_START_Y - (int)getCols()/2;
  }
}
