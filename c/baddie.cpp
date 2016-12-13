#include <avatar.h>
#include <baddie.h>
#include <terrainmap.h>
#include <stdlib.h>
#include <string.h>

Baddie::Baddie(unsigned int initialPosX, unsigned int initialPosY) : Avatar(initialPosX, initialPosY, TERRAIN_BADDIE) {
  state = BADDIE_STATE_UNDERGROUND;
  charUndergroundIndex = 0;
  movementFrameStep = (rand()%3)*10;  // Start at 0, 10, 20
  movomentFrame = MOVEMENT_FRAME_UNDERGROUND;
  id = rand() % 1;
}

Baddie::~Baddie() {}

const char  Baddie::getCharacter() {
  char retval = CHAR_BADDIE_ZOMBIE;
  switch(state) {
    case BADDIE_STATE_UNDERGROUND:
      retval = CHAR_BADDIE_UNDERGROUND[charUndergroundIndex];
    break;
    case BADDIE_STATE_ZOMBIE:
      retval = CHAR_BADDIE_ZOMBIE;
    break;
    default:
    case BADDIE_STATE_HUMAN:
      retval = CHAR_BADDIE_HUMAN;
    break;
  }
  return retval;
}
unsigned int Baddie::getState() {
  return state;
}

bool Baddie::update( TerrainMap * const map) {
  bool retval = false;
  movementFrameStep++;
  if (movementFrameStep >= movomentFrame)
  {
    switch(state){
      case BADDIE_STATE_UNDERGROUND:
        if (charUndergroundIndex < sizeof(CHAR_BADDIE_UNDERGROUND)) {
          charUndergroundIndex++;
        }
        if (charUndergroundIndex >= sizeof(CHAR_BADDIE_UNDERGROUND)) {
          state = BADDIE_STATE_ZOMBIE;
          movomentFrame = MOVEMENT_FRAME_ZOMBIE;
          movementFrameStep = 0;
        }
        retval = true;
      break;
      case BADDIE_STATE_ZOMBIE:
      case BADDIE_STATE_HUMAN:
       int vx = (rand()%3) - 1;  //-1, 0, 1
       int vy = (rand()%3) - 1;  //-1, 0, 1
       if (vx != 0)
       {
          if (map->getAt(getX() + vx, getY()) == 0) {
             map->moveObject(getX() + vx, getY(), map->getAt(getX(),getY()));
          }
          retval = true;
       }
       if (vy != 0)
       {
         if (map->getAt(getX(), getY() + vy) == 0) {
           map->moveObject(getX(), getY() + vy, map->getAt(getX(),getY()));
         }
         retval = true;
       }
      break;
    }
    movementFrameStep = 0;
  }
  return retval;
}

void Baddie::turnHuman() {
  if (state == BADDIE_STATE_ZOMBIE) {
    state = BADDIE_STATE_HUMAN;
    movomentFrame = MOVEMENT_FRAME_HUMAN;
    movementFrameStep = 0;
  }
}

void Baddie::turnZombie() {
  if (state == BADDIE_STATE_HUMAN) {
    state = BADDIE_STATE_ZOMBIE;
    movomentFrame = MOVEMENT_FRAME_ZOMBIE;
    movementFrameStep = 0;
  }
}

char const * Baddie::getName() {
  char const * retval;
  switch(state) {
    case BADDIE_STATE_UNDERGROUND:
      retval = "A Strange Mound";
    break;
    case BADDIE_STATE_ZOMBIE:
      retval = "A Random Zombie";
    break;
    default:
    case BADDIE_STATE_HUMAN:
      retval = "A Random Human";
    break;
  }
  return retval;
}

char const * Baddie::getConversation() {
  char const * retval;

  switch(state) {
    case BADDIE_STATE_UNDERGROUND:
      retval = "Almost as if something is digging up from underground.";
    break;
    case BADDIE_STATE_ZOMBIE:
      retval = "Moan ... Brains ...";
    break;
    default:
    case BADDIE_STATE_HUMAN:
      retval = "Hey there";
    break;
  }
  return retval;
}

char const * Baddie::names[] = {
"Player"
};

char const * Baddie::conversations[] = {
"Talking to yourself again?"
};
