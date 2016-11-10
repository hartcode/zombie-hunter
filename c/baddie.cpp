#include <baddie.h>

Baddie::Baddie(unsigned int initialPosX, unsigned int initialPosY) : Avatar(initialPosX, initialPosY) {
  state = BADDIE_STATE_UNDERGROUND;
  charUndergroundIndex = 0;
}

Baddie::~Baddie() {}

char Baddie::getCharacter() {
  char retval = CHAR_BADDIE_DEAD;
  switch(state) {
    case BADDIE_STATE_UNDERGROUND:
      retval = CHAR_BADDIE_UNDERGROUND[charUndergroundIndex];
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

bool Baddie::update() {
  bool retval = false;
  switch(state){
    case BADDIE_STATE_UNDERGROUND:
      if (charUndergroundIndex < sizeof(CHAR_BADDIE_UNDERGROUND)) {
        charUndergroundIndex++;
      } else {
        state = BADDIE_STATE_MOBILE;
      }
      retval = true;
    break;
    case BADDIE_STATE_MOBILE:
      // TODO: Move the baddie
    break;
  }
  return retval;
}
