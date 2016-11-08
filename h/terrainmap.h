#ifndef TERRAINMAP_H
#define TERRAINMAP_H

const int TERRAIN_EMPTY = 0;
const int TERRAIN_TREE = 1;
const int TERRAIN_GRAVESTONE = 2;

const char CHAR_WALL = 219;  // Wall
const char CHAR_EMPTY = 32;  // Space
const char CHAR_TREE = 84;   // T
const char CHAR_GRAVESTONE = 35;

class TerrainMap {
    unsigned int width;
    unsigned int height;
    unsigned int treeCount;
    unsigned int graveCount;
    unsigned int ** map;
  public:
    TerrainMap(unsigned int mapWidth, unsigned int mapHeight, unsigned int treeAmount, unsigned int graveAmount);
    ~TerrainMap();
    char getAt(int X, int Y);

  private:
    void populateMap();
    void placeRandomObjects(unsigned int numberOfObjects, unsigned int terrain_object);
};
#endif
