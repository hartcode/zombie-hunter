#include <avatar.h>
#include <terrainmap.h>

#ifndef BADDIE_H
#define BADDIE_H

char const * const CHAR_BADDIE_UNDERGROUND[] = {"\u002D","\u007E","\u002D","\u007E","\u002D","\u007E","\u005E"};

char const * const CHAR_BADDIE_ZOMBIE = "\u263B";

char const * const CHAR_BADDIE_HUMAN = "\u263A";

char const * const CHAR_BADDIE_DEAD = "\u0078";

const unsigned int BADDIE_STATE_UNDERGROUND = 0;
const unsigned int BADDIE_STATE_ZOMBIE = 1;
const unsigned int BADDIE_STATE_HUMAN = 2;

const unsigned int MOVEMENT_FRAME_ZOMBIE = 30;
const unsigned int MOVEMENT_FRAME_HUMAN = 10;
const unsigned int MOVEMENT_FRAME_UNDERGROUND = 10;


class Baddie : public Avatar {
  unsigned int state;
  unsigned int charUndergroundIndex;
  unsigned int movementFrameStep;
  unsigned int movomentFrame;
  public:
    Baddie(unsigned int initialPosX, unsigned int initialPosY);
    virtual ~Baddie();
    char const * getCharacter();
    bool update(TerrainMap * const map);
    void turnHuman();
    void turnZombie();
};

#endif
