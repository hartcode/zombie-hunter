#include <terrainobject.h>
#include <terrainmap.h>

TerrainObject::TerrainObject(unsigned int initialPosX, unsigned int initialPosY) {
  setX(initialPosX);
  setY(initialPosY);
  character = CHAR_TERRAINOBJECT;
}

TerrainObject::~TerrainObject() {}

void TerrainObject::setX(unsigned int x) {
  posX = x;
}

void TerrainObject::setY(unsigned int y) {
  posY = y;
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

char const * TerrainObject::getConversation() {
  return "There is nothing here";
}
