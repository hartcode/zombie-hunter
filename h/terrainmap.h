#ifndef TERRAINMAP_H
#define TERRAINMAP_H

const int TERRAIN_EMPTY = 0;
const int TERRAIN_TREE = 1;
const int TERRAIN_GRAVESTONE = 2;
const int TERRAIN_PLAYER = 3;
const int TERRAIN_BADDIE = 4;
const int TERRAIN_WALL = 5;

const char CHAR_WALL = 219;  // Wall
const char CHAR_EMPTY = ' '; //"\u0020\x00";//32;  // Space
const char CHAR_TREE = 'T'; //"\u0054\x00";//84;   // T
const char CHAR_GRAVESTONE = '#'; //"\u03A9\x00";//35;

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
    const char getCharacterAt(int X, int Y);

  private:
    void populateMap();
    void placeRandomObjects(unsigned int numberOfObjects, unsigned int terrain_object);
};
#endif
