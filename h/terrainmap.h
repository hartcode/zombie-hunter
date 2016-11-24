#ifndef TERRAINMAP_H
#define TERRAINMAP_H
#include <terrainobject.h>

const char CHAR_EMPTY = ' '; //"\u0020\x00";//32;  // Space

class TerrainMap {
    unsigned int width;
    unsigned int height;
    unsigned int treeCount;
    unsigned int graveCount;
    TerrainObject *** map;
    TerrainObject * wall;
  public:
    TerrainMap(unsigned int mapWidth, unsigned int mapHeight, unsigned int treeAmount, unsigned int graveAmount);
    ~TerrainMap();
    void setAt(unsigned int x, unsigned int y, TerrainObject * ch);
    void moveObject(unsigned int newX, unsigned int newY, TerrainObject * ch);
    TerrainObject * getAt(int X, int Y);
    bool update();
  private:
    void populateMap();
    void placeRandomObjects(unsigned int numberOfObjects, unsigned int terrain_object);
};
#endif
