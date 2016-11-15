#ifndef TERRAINMAP_H
#define TERRAINMAP_H

const int TERRAIN_EMPTY = 0;
const int TERRAIN_TREE = 1;
const int TERRAIN_GRAVESTONE = 2;
const int TERRAIN_PLAYER = 3;
const int TERRAIN_BADDIE = 4;
const int TERRAIN_WALL = 5;

char const * const CHAR_WALL = "\u2588\x00";//219;  // Wall
char const * const CHAR_EMPTY = "\u0020\x00";//32;  // Space
char const * const CHAR_TREE = "\u0054\x00";//84;   // T
char const * const CHAR_GRAVESTONE = "\u03A9\x00";//35;

class TerrainMap {
    unsigned int width;
    unsigned int height;
    unsigned int treeCount;
    unsigned int graveCount;
    unsigned int ** map;
  public:
    TerrainMap(unsigned int mapWidth, unsigned int mapHeight, unsigned int treeAmount, unsigned int graveAmount);
    ~TerrainMap();
    void setAt(unsigned int x, unsigned int y, unsigned int ch);
    unsigned int getAt(int X, int Y);
    char const * getCharacterAt(int X, int Y);

  private:
    void populateMap();
    void placeRandomObjects(unsigned int numberOfObjects, unsigned int terrain_object);
};
#endif
