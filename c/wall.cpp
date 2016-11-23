#include <wall.h>
#include <terrainobject.h>

Wall::Wall(unsigned int initialPosX, unsigned int initialPosY) : TerrainObject(initialPosX, initialPosY, TERRAIN_WALL) {
  character = CHAR_WALL;
}

Wall::~Wall() {}

char const * Wall::getName() {
  return "A Wall";
}

char const * Wall::getConversation() {
  return "* The wall gives you a blank stare *";
}
