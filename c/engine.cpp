#include <engine.h>
#include <display.h>
#include <terrainmap.h>
#include <input.h>
#include <avatar.h>
#include <baddie.h>
#include <bullet.h>

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

Bullet bullet = Bullet(0, 0);

void game_loop(Display * const display, Input * const in) {
  int viewX = 0;
  int viewY = 0;
  bool change = false;
  bool cancel = false;
  int key = 0;
  int baddieStep = 0;
  double milliseconds;

  // setup random numbers
  srand(time(NULL));

  TerrainMap map = TerrainMap(MAP_WIDTH, MAP_HEIGHT, TREE_COUNT, GRAVE_COUNT);

  // this is the first calculation :)
  recalculateViewPosition(&viewX, &viewY, display);

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
  draw(avatars, avatar_count, &map, &viewX, &viewY, display);

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

    if (bullet.getFired()) {
        change |= bullet.update(&map);
    }

    // Input
    key = in->getkey(&milliseconds);
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
           int vx = 0;
           int vy = 0;
           switch(player->getDirection())
           {
             case AVATAR_DIRECTION_LEFT:
                vy = -1;
             break;
             case AVATAR_DIRECTION_RIGHT:
                vy = 1;
             break;
             case AVATAR_DIRECTION_UP:
                vx = -1;
             break;
             case AVATAR_DIRECTION_DOWN:
                vx = 1;
             break;
           }
           switch(map.getAt(player->getX() + vx, player->getY() + vy)) {
             case TERRAIN_TREE:
                display->printConversation("Birch Tree", "Shh! I'm sleeping.");
             break;
             case TERRAIN_GRAVESTONE:
                display->printConversation("Gravestone", "Here Lies Steven Baker");
             break;
             case TERRAIN_BADDIE:
               for (unsigned int avatarIndex = 1; avatarIndex < avatar_count; avatarIndex++) {
                 if (player->getX() + vx == avatars[avatarIndex]->getX() && player->getY() + vy == avatars[avatarIndex]->getY()  && ((Baddie*)avatars[avatarIndex])->getState() == BADDIE_STATE_HUMAN) {
                    display->printConversation(avatars[avatarIndex]->getName(), "yo!");
                 }
               }
             break;
             case TERRAIN_WALL:
                 display->printConversation("Wall", "* The wall gives you a hard stare. *");
             break;
           }

           // fire a bullet
           if (!bullet.getFired()) {
             bullet.setDirection(player->getDirection());
             bullet.setX(player->getX() + vx);
             bullet.setY(player->getY() + vy);
             bullet.setFired(true);
             change |= true;
           }
         break;
       }
      cancel = key == ESCAPE_KEY;
    }

    for (unsigned int avatarIndex = 0; avatarIndex < avatar_count; avatarIndex++) {
      change |= avatars[avatarIndex]->update(&map);
    }

    if (bullet.getFired()) {
      if (map.getAt(bullet.getX(), bullet.getY()) != TERRAIN_EMPTY) {
         bullet.setFired(false);
         change |= true;
      }

      for (unsigned int avatarIndex = 1; avatarIndex < avatar_count; avatarIndex++) {
        if (bullet.getX() == avatars[avatarIndex]->getX() && bullet.getY() == avatars[avatarIndex]->getY()) {
          bullet.setFired(false);
          ((Baddie *)avatars[avatarIndex])->turnHuman();
          change |= true;
        }
      }
    }

    if (change) {
     change = false;
     draw(avatars, avatar_count, &map, &viewX, &viewY, display);
    }
    in->sleepy(milliseconds);
  } // Main loop


  // Cleanup avatars
  for (unsigned int avatarIndex = 0; avatarIndex < avatar_count; avatarIndex++) {
    delete avatars[avatarIndex];
  }
  delete avatars;
  display->clear();
}

void draw(Avatar ** avatar, unsigned int avatarCount, TerrainMap * const map, int * viewX, int * viewY, Display * const display) {
  unsigned int rows = display->getRows();
  unsigned int cols = display->getCols();
  unsigned int viewableRows = rows-2;

  if (rows != lastrows || cols != lastcols) {
    recalculateViewPosition(viewX, viewY, display);
  }

  // Clear Screen
  display->clear();
  // Draw HEADER
  //display->print("Rows %u Cols %u ViewX %i ViewY %i Px %i Py %i\n", rows, cols, *viewX, *viewY, avatar[0]->getX(), avatar[0]->getY());
  // Draw BOARD
  char buffer[BUFFER_SIZE];
  memset(&buffer[0], 0, BUFFER_SIZE);
  display->printChar(NEWLINE_CHAR);
  for(unsigned int x = 0; x < viewableRows; x++) {
     display->printChar(CHAR_EMPTY);
      for (unsigned int y = 1; y < cols-1; y++) {
        char terrainChar = map->getCharacterAt(*viewX + x, *viewY + y);
        for (unsigned int avatarIndex = 0; avatarIndex < avatarCount; avatarIndex++) {
          if(avatar[avatarIndex]->getX() == x + *viewX && avatar[avatarIndex]->getY() == y + *viewY) {
            terrainChar = avatar[avatarIndex]->getCharacter();
          }
        }
        if (bullet.getFired() && bullet.getX() == x + *viewX && bullet.getY() == y + *viewY) {
          terrainChar = bullet.getCharacter();
        }
       display->printChar(terrainChar);
      }
    display->printChar(NEWLINE_CHAR);
  }

  display->draw();
  lastrows = rows;
  lastcols = cols;
}

void recalculateViewPosition(int * viewX, int * viewY, Display * const display)
{
  if ((int)display->getRows() - MAP_HEIGHT > 0) {
    *viewX = (((int)display->getRows() - MAP_HEIGHT)/2) * -1;
  } else {
    *viewX =  AVATAR_START_X - (int)display->getRows()/2;
  }
  if ((int)display->getCols() - MAP_WIDTH > 0) {
    *viewY = (((int)display->getCols() - MAP_WIDTH)/2) * -1;
  } else {
    *viewY = AVATAR_START_Y - (int)display->getCols()/2;
  }
}
