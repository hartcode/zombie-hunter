#include <avatar.h>
#include <terrainmap.h>

#ifndef BADDIE_H
#define BADDIE_H

const char CHAR_BADDIE_UNDERGROUND[] = {(char)45,(char)126,(char)45,(char)126,(char)45,(char)126,(char)94};

const char CHAR_BADDIE_ZOMBIE = 1;

const char CHAR_BADDIE_HUMAN = 2;

const char CHAR_BADDIE_DEAD = 120;

const unsigned int BADDIE_STATE_UNDERGROUND = 0;
const unsigned int BADDIE_STATE_ZOMBIE = 1;
const unsigned int BADDIE_STATE_HUMAN = 2;


class Baddie : public Avatar {
  unsigned int state;
  unsigned int charUndergroundIndex;
  public:
    Baddie(unsigned int initialPosX, unsigned int initialPosY);
    virtual ~Baddie();
    char getCharacter();
    bool update(TerrainMap * const map);
};

#endif
