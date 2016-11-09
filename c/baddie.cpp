#include <baddie.h>

Baddie::Baddie(unsigned int initialPosX, unsigned int initialPosY) : Avatar(initialPosX, initialPosY) {
  state = BADDIE_STATE_UNDERGROUND;
}


char Baddie::getCharacter() {
  char retval = CHAR_BADDIE_DEAD;
  switch(state) {
    case BADDIE_STATE_UNDERGROUND:
      retval = CHAR_BADDIE_UNDERGROUND[0];
    break;
    case BADDIE_STATE_MOBILE:
      retval = CHAR_BADDIE_MOBILE;
    break;
    default:
    case BADDIE_STATE_DEAD:
      retval = CHAR_BADDIE_DEAD;
    break;
  }
  return retval;
}
