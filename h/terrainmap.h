#ifndef TERRAINMAP_H
#define TERRAINMAP_H

const int TERRAIN_EMPTY = 0;

class TerrainMap {
    unsigned int width;
    unsigned int height;
    int ** map;
  public:
    TerrainMap(unsigned int width, unsigned int height);
    ~TerrainMap();
    void getRow(int viewX, int viewY, unsigned int viewColCount, unsigned int viewRowCount, char * const buffer, unsigned int bufferSize);
};
#endif
