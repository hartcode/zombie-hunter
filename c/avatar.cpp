#include <avatar.h>
#include <terrainmap.h>
#include <terrainobject.h>

Avatar::Avatar(unsigned int initialPosX, unsigned int initialPosY) : TerrainObject(initialPosX, initialPosY) {
  character = CHAR_AVATAR;
  direction = AVATAR_DIRECTION_UNKNOWN;
}

Avatar::~Avatar() {}

bool Avatar::update(TerrainMap * const map) {
  return false;
}

void Avatar::setDirection(unsigned int newDirection) {
  direction = newDirection;
}

unsigned int Avatar::getDirection() {
  return direction;
}

char const * Avatar::getName() {
  return "Player";
}

char const * Avatar::getConversation() {
  return "Talking to yourself again?";
}
