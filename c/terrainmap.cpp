#include <terrainmap.h>
#include <terrainobject.h>
#include <string.h>
#include <stdlib.h>

TerrainMap::TerrainMap(unsigned int mapWidth, unsigned int mapHeight, unsigned int treeAmount, unsigned int graveAmount)
{
    width = mapWidth;
    height = mapHeight;
    treeCount = treeAmount;
    graveCount = graveAmount;
    // initialize map
    map = new TerrainObject **[height];
    for (unsigned int x = 0; x < height; x++)
    {
      map[x] = new TerrainObject *[width];
      for (unsigned int y = 0; y < width; y++) {
        map[x][y] = new TerrainObject(x, y);
      }
    }
    populateMap();
}

TerrainMap::~TerrainMap() {
    // destroy map
    for (unsigned int x = 0; x < height; x++)
    {
      for (unsigned int y = 0; y < width; y++) {
        delete map[x][y];
      }
      delete map[x];
    }
    delete map;
}

void TerrainMap::setAt(unsigned int x, unsigned int y, TerrainObject * ch)
{
   //if (x >= 0 && x < height && y >= 0 && y < width) {
   delete map[x][y];
   map[x][y] = ch;
   //}
}

TerrainObject * TerrainMap::getAt(int x, int y) {
  TerrainObject * retval;
  if (y >= 0 && y < (int)width && x >= 0 && x < (int)height){
    retval = map[x][y];
  }
  return retval;
}

const char TerrainMap::getCharacterAt(int x, int y) {
  char retval = CHAR_EMPTY;
  if (y < 0 || y >= (int)width){
      retval = CHAR_WALL;
    } else {
        if (x < 0 || x >= (int)height){
          retval = CHAR_WALL;
        } else {
          retval = map[x][y]->getCharacter();
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
    delete map[x][y];
    map[x][y] = new TerrainObject(x, y);
  }
}
