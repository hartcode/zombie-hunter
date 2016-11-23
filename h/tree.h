#include <terrainobject.h>
#ifndef TREE_H
#define TREE_H

const char CHAR_TREE = 'T';  //"\u263A\x00";

class Tree : public TerrainObject {
  public:
    Tree(unsigned int initialPosX, unsigned int initialPosY);
    virtual ~Tree();
    virtual char const * getName();
    virtual char const * getConversation();
};

#endif
