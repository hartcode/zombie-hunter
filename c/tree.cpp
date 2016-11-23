#include <tree.h>
#include <terrainobject.h>

Tree::Tree(unsigned int initialPosX, unsigned int initialPosY) : TerrainObject(initialPosX, initialPosY, TERRAIN_TREE) {
  character = CHAR_TREE;
}

Tree::~Tree() {}

char const * Tree::getName() {
  return "A Tree";
}

char const * Tree::getConversation() {
  return "This tree looks barky";
}
