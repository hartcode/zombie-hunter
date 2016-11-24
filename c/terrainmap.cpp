#include <terrainmap.h>
#include <terrainobject.h>
#include <tree.h>
#include <wall.h>
#include <gravestone.h>
#include <string.h>
#include <stdlib.h>

TerrainMap::TerrainMap(unsigned int mapWidth, unsigned int mapHeight, unsigned int treeAmount, unsigned int graveAmount)
{
    width = mapWidth;
    height = mapHeight;
    treeCount = treeAmount;
    graveCount = graveAmount;
    wall = new Wall(0,0);
    // initialize map
    map = new TerrainObject **[height];
    for (unsigned int x = 0; x < height; x++)
    {
      map[x] = new TerrainObject *[width];
      for (unsigned int y = 0; y < width; y++) {
        map[x][y] = 0;
      }
    }
    populateMap();
}

TerrainMap::~TerrainMap() {
    delete wall;
    // destroy map
    for (unsigned int x = 0; x < height; x++)
    {
      for (unsigned int y = 0; y < width; y++) {
        if (map[x][y] != 0) {
          delete map[x][y];
          map[x][y] = 0;
        }
      }
      delete map[x];
    }
    delete map;
}

void TerrainMap::setAt(unsigned int x, unsigned int y, TerrainObject * ch)
{
   //if (x >= 0 && x < height && y >= 0 && y < width) {
   if (map[x][y] != 0) {
     delete map[x][y];
     map[x][y] = 0;
   }
   map[x][y] = ch;
}

void TerrainMap::moveObject(unsigned int newX, unsigned int newY, TerrainObject * ch) {
  map[ch->getX()][ch->getY()] = 0;
  ch->setX(newX);
  ch->setY(newY);
  map[ch->getX()][ch->getY()] = ch;
}

TerrainObject * TerrainMap::getAt(int x, int y) {
  TerrainObject * retval = wall;
  if (y >= 0 && y < (int)width && x >= 0 && x < (int)height){
    retval = map[x][y];
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
    if (terrain_object == TERRAIN_TREE) {
      if (map[x][y] != 0) {
        delete map[x][y];
        map[x][y] = 0;
      }
      map[x][y] = new Tree(x,y);
    } else if (terrain_object == TERRAIN_GRAVESTONE) {
      if (map[x][y] != 0) {
        delete map[x][y];
        map[x][y] = 0;
      }
      map[x][y] = new Gravestone(x,y);
    }
  }
}

bool TerrainMap::update() {
  bool retval = false;
  for (unsigned int x = 0; x < height; x++)
  {
    for (unsigned int y = 0; y < width; y++) {
      if (map[x][y] != 0) {
         retval |= map[x][y]->update(this);
      }
    }
  }
  return retval;
}
