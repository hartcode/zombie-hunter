#include <baddie.h>
#include <terrainmap.h>
#include <stdlib.h>

Baddie::Baddie(unsigned int initialPosX, unsigned int initialPosY) : Avatar(initialPosX, initialPosY) {
  state = BADDIE_STATE_UNDERGROUND;
  charUndergroundIndex = 0;
}

Baddie::~Baddie() {}

char Baddie::getCharacter() {
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

bool Baddie::update( TerrainMap * const map) {
  bool retval = false;
  switch(state){
    case BADDIE_STATE_UNDERGROUND:
      if (charUndergroundIndex < sizeof(CHAR_BADDIE_UNDERGROUND)) {
        charUndergroundIndex++;
      } else {
        state = BADDIE_STATE_ZOMBIE;
      }
      retval = true;
    break;
    case BADDIE_STATE_ZOMBIE:
    case BADDIE_STATE_HUMAN:
     int vx = (rand()%3) - 1;  //-1, 0, 1
     int vy = (rand()%3) - 1;  //-1, 0, 1
     if (vx != 0)
     {
        if (map->getAt(getX() + vx, getY()) == TERRAIN_EMPTY) {
           map->setAt(getX(), getY(), TERRAIN_EMPTY);
           setX(getX() + vx);
           map->setAt(getX(), getY(), TERRAIN_BADDIE);
        }
        retval = true;
     }
     if (vy != 0)
     {
       if (map->getAt(getX(), getY() + vy) == TERRAIN_EMPTY) {
         map->setAt(getX(), getY(), TERRAIN_EMPTY);
         setY(getY() + vy);
         map->setAt(getX(), getY(), TERRAIN_BADDIE);
       }
       retval = true;
     }
    break;
  }
  return retval;
}
