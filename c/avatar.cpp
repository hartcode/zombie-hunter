#include <avatar.h>

Avatar::Avatar(unsigned int initialPosX, unsigned int initialPosY) {
  setX(initialPosX);
  setY(initialPosY);
  character = CHAR_AVATAR;
}
void Avatar::setX(unsigned int x) {

  posX = x;
}

void Avatar::setY(unsigned int y) {
  posY = y;
}

unsigned int Avatar::getX() {
  return posX;
}
unsigned int Avatar::getY() {
  return posY;
}

char Avatar::getCharacter() {
  return character;
}
