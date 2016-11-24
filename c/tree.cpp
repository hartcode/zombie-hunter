#include <tree.h>
#include <terrainobject.h>
#include <stdlib.h>

Tree::Tree(unsigned int initialPosX, unsigned int initialPosY) : TerrainObject(initialPosX, initialPosY, TERRAIN_TREE) {
  character = CHAR_TREE;
  id = rand() % 4;
}

Tree::~Tree() {}

char const * Tree::names[] = {
"The Larch (Larix decidua)",
"Sugar Mapple (Acer saccharum)",
"A Tree",
"This other Tree"
};

char const * Tree::conversations[] = {
"There is a large python in it's branches.",
"Eww, it's all sticky.",
"This tree looks ready to leave.",
"This tree has lost it's bark, but not it's bite."
};

char const * Tree::getName() {
  return names[getID()];
}

char const * Tree::getConversation() {
  return conversations[getID()];
}
