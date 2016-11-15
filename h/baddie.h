#include <avatar.h>
#include <terrainmap.h>

#ifndef BADDIE_H
#define BADDIE_H

char const * const CHAR_BADDIE_UNDERGROUND[] = {"\u002D\x00","\u007E\x00","\u002D\x00","\u007E\x00","\u002D\x00","\u007E\x00","\u005E\x00"};

char const * const CHAR_BADDIE_ZOMBIE = "\u263B\x00";

char const * const CHAR_BADDIE_HUMAN = "\u263A\x00";

char const * const CHAR_BADDIE_DEAD = "\u0078\x00";

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
