#include <terrainobject.h>
#ifndef WALL_H
#define WALL_H

const char CHAR_WALL = 219;  // Wall

class Wall : public TerrainObject {
  public:
    Wall(unsigned int initialPosX, unsigned int initialPosY);
    virtual ~Wall();
    virtual char const * getName();
    virtual char const * getConversation();
};

#endif
