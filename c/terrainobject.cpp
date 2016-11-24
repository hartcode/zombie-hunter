#include <terrainmap.h>
#include <terrainobject.h>

TerrainObject::TerrainObject(unsigned int initialPosX, unsigned int initialPosY, unsigned int ids) {
  setX(initialPosX);
  setY(initialPosY);
  character = CHAR_TERRAINOBJECT;
  terrainid = ids;
}

TerrainObject::~TerrainObject() {}

void TerrainObject::setX(unsigned int x) {
  posX = x;
}

void TerrainObject::setY(unsigned int y) {
  posY = y;
}

unsigned int TerrainObject::getTerrainID(){
  return terrainid;
}

unsigned int TerrainObject::getX() {
  return posX;
}
unsigned int TerrainObject::getY() {
  return posY;
}

const char TerrainObject::getCharacter() {
  return character;
}

char const * TerrainObject::getName() {
  return "Empty Space";
}

bool TerrainObject::update(TerrainMap * const map) {
  return false;
}

char const * TerrainObject::getConversation() {
  return "There is nothing here";
}
