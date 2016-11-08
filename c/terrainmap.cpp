#include <terrainmap.h>
#include <string.h>

TerrainMap::TerrainMap(unsigned int mapWidth, unsigned int mapHeight)
{
    width = mapWidth;
    height = mapHeight;
    // initialize map
    map = new int*[height];
    for (unsigned int x = 0; x < height; x++)
    {
      map[x] = new int[width];
      for (unsigned int y = 0; y < width; y++) {
        map[x][y] = TERRAIN_EMPTY;
      }
    }
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
      memset(buffer,'*',viewColCount);
    } else {
      for (unsigned int x = 0; x < viewColCount; x++) {
        if (viewX + x < 0 || viewX + x >= width){
          buffer[x] = '*';
        } else {
          switch(map[viewX + x][viewY]) {
            case TERRAIN_EMPTY:
              buffer[x] = ' ';
              break;
          }
        }
      }
    }
  }
}
