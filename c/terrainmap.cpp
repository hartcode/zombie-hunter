#include <terrainmap.h>
#include <string.h>
#include <stdlib.h>
#include <time.h>

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

void TerrainMap::getRow(int viewX, int viewY, unsigned int viewColCount, unsigned int viewRowCount, char * const buffer, unsigned int bufferSize)
{
  if (bufferSize < viewColCount)
  {
    // We have a big problem the buffersize should always be atleast the same size as the viewColCount;
  } else {
    if (viewY < 0 || viewY >= (int)height){
      memset(buffer,CHAR_WALL,viewColCount);
    } else {
      for (unsigned int x = 0; x < viewColCount; x++) {
        if (viewX + x < 0 || viewX + x >= width){
          buffer[x] = CHAR_WALL;
        } else {
          switch(map[viewX + x][viewY]) {
            case TERRAIN_EMPTY:
              buffer[x] = CHAR_EMPTY;
              break;
            case TERRAIN_TREE:
              buffer[x] = CHAR_TREE;
              break;
            case TERRAIN_GRAVESTONE:
              buffer[x] = CHAR_GRAVESTONE;
              break;
          }
        }
      }
    }
  }
}

void TerrainMap::populateMap() {
  srand(time(NULL));
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
