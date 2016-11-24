#include <gravestone.h>
#include <terrainobject.h>
#include <stdlib.h>

Gravestone::Gravestone(unsigned int initialPosX, unsigned int initialPosY) : TerrainObject(initialPosX, initialPosY, TERRAIN_GRAVESTONE) {
  character = CHAR_GRAVESTONE;
    id = rand() % 1;
}

Gravestone::~Gravestone() {}

char const * Gravestone::names[] = {
"A Gravestone"
};

char const * Gravestone::conversations[] = {
"Here lies some random person."
};

char const * Gravestone::getName() {
  return names[getID()];
}

char const * Gravestone::getConversation() {
  return conversations[getID()];
}
