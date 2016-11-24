#include <terrainmap.h>
#include <terrainobject.h>
#include <stdlib.h>

TerrainObject::TerrainObject(unsigned int initialPosX, unsigned int initialPosY, unsigned int ids) {
  setX(initialPosX);
  setY(initialPosY);
  character = CHAR_TERRAINOBJECT;
  terrainid = ids;
  id = rand() % 1;
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

unsigned int TerrainObject::getID() {
  return id;
}

const char TerrainObject::getCharacter() {
  return character;
}

bool TerrainObject::update(TerrainMap * const map) {
  return false;
}

char const * TerrainObject::names[] = {
"Empty Space"
};

char const * TerrainObject::conversations[] = {
"There is nothing here"
};

char const * TerrainObject::getName() {
  return names[getID()];
}

char const * TerrainObject::getConversation() {
  return conversations[getID()];
}
