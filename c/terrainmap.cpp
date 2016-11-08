#include <terrainmap.h>
#include <string.h>
#include <stdlib.h>

TerrainMap::TerrainMap(unsigned int mapWidth, unsigned int mapHeight, unsigned int treeAmount, unsigned int graveAmount)
{
    width = mapWidth;
    height = mapHeight;
    treeCount = treeAmount;
    graveCount = graveAmount;
    // initialize map
    map = new unsigned int*[height];
    for (unsigned int x = 0; x < height; x++)
    {
      map[x] = new unsigned int[width];
      for (unsigned int y = 0; y < width; y++) {
        map[x][y] = TERRAIN_EMPTY;
      }
    }
    populateMap();
}

TerrainMap::~TerrainMap() {
    // destroy map
    for (unsigned int x = 0; x < height; x++)
    {
      delete map[x];
    }
    delete map;
}

char TerrainMap::getAt(int x, int y) {
  char retval = CHAR_EMPTY;
  if (y < 0 || y >= (int)height){
      retval = CHAR_WALL;
    } else {
        if (x < 0 || x >= (int)width){
          retval = CHAR_WALL;
        } else {
          switch(map[x][y]) {
            case TERRAIN_EMPTY:
              retval = CHAR_EMPTY;
              break;
            case TERRAIN_TREE:
              retval = CHAR_TREE;
              break;
            case TERRAIN_GRAVESTONE:
              retval = CHAR_GRAVESTONE;
              break;
          }
        }
    }
    return retval;
}

void TerrainMap::populateMap() {
  placeRandomObjects(treeCount, TERRAIN_TREE);
  placeRandomObjects(graveCount, TERRAIN_GRAVESTONE);
}

void TerrainMap::placeRandomObjects(unsigned int numberOfObjects, unsigned int terrain_object) {
  int x = 0;
  int y = 0;
  for (unsigned int i = 0; i < numberOfObjects; i++)
  {
    x = rand()%height;
    y = rand()%width;
    map[x][y] = terrain_object;
  }
}
