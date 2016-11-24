#include <tree.h>
#include <terrainobject.h>
#include <stdlib.h>

Tree::Tree(unsigned int initialPosX, unsigned int initialPosY) : TerrainObject(initialPosX, initialPosY, TERRAIN_TREE) {
  character = CHAR_TREE;
  id = rand() % 3;
}

Tree::~Tree() {}

char const * Tree::names[] = {
"A Tree",
"Another Tree",
"This other Tree"
};

char const * Tree::conversations[] = {
"This tree looks barky.",
"This tree looks ready to leave.",
"This tree has lost it's bark, but not it's bite."
};

char const * Tree::getName() {
  return names[getID()];
}

char const * Tree::getConversation() {
  return conversations[getID()];
}
