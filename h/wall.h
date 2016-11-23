#ifndef WALL_H
#define WALL_H
#include <terrainobject.h>

const char CHAR_WALL = 219;  // Wall
const int TERRAIN_WALL = 5;

class Wall : public TerrainObject {
  public:
    Wall(unsigned int initialPosX, unsigned int initialPosY);
    virtual ~Wall();
    virtual char const * getName();
    virtual char const * getConversation();
};

#endif
