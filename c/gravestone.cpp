#include <gravestone.h>
#include <terrainobject.h>

Gravestone::Gravestone(unsigned int initialPosX, unsigned int initialPosY) : TerrainObject(initialPosX, initialPosY, TERRAIN_GRAVESTONE) {
  character = CHAR_GRAVESTONE;
}

Gravestone::~Gravestone() {}

char const * Gravestone::getName() {
  return "A Gravestone";
}

char const * Gravestone::getConversation() {
  return "Here lies some random person.";
}
