#include <wall.h>
#include <terrainobject.h>
#include <stdlib.h>

Wall::Wall(unsigned int initialPosX, unsigned int initialPosY) : TerrainObject(initialPosX, initialPosY, TERRAIN_WALL) {
  character = CHAR_WALL;
    id = rand() % 1;
}

Wall::~Wall() {}

char const * Wall::names[] = {
"A Wall"
};

char const * Wall::conversations[] = {
"* The wall gives you a blank stare *"
};

char const * Wall::getName() {
  return names[getID()];
}

char const * Wall::getConversation() {
  return conversations[getID()];
}
