#include <avatar.h>
#include <terrainmap.h>
#include <terrainobject.h>
#include <stdlib.h>

Avatar::Avatar(unsigned int initialPosX, unsigned int initialPosY) : TerrainObject(initialPosX, initialPosY, TERRAIN_PLAYER) {
  character = CHAR_AVATAR;
  direction = AVATAR_DIRECTION_UNKNOWN;
  id = rand() % 1;
}

Avatar::Avatar(unsigned int initialPosX, unsigned int initialPosY, unsigned int ids) : TerrainObject(initialPosX, initialPosY, ids) {
  character = CHAR_AVATAR;
  direction = AVATAR_DIRECTION_UNKNOWN;
  id = rand() % 1;
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
  return names[getID()];
}

char const * Avatar::getConversation() {
  return conversations[getID()];
}

char const * Avatar::names[] = {
"Player"
};

char const * Avatar::conversations[] = {
"Talking to yourself again?"
};
