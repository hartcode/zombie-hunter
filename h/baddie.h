#include <avatar.h>

#ifndef BADDIE_H
#define BADDIE_H

const char CHAR_BADDIE_UNDERGROUND[] = {(char)45,(char)126,(char)94};

const char CHAR_BADDIE_MOBILE = 1;

const char CHAR_BADDIE_DEAD = 120;


const unsigned int BADDIE_STATE_UNDERGROUND = 0;
const unsigned int BADDIE_STATE_MOBILE = 1;
const unsigned int BADDIE_STATE_DEAD = 2;


class Baddie : public Avatar {
  unsigned int state;
  public:
    Baddie(unsigned int initialPosX, unsigned int initialPosY);
    char getCharacter();
};

#endif
