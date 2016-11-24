#ifndef TERRAINOBJECT_H
#define TERRAINOBJECT_H


const char CHAR_TERRAINOBJECT = ' ';  //"\u263A\x00";
const int TERRAIN_EMPTY = 0;
class TerrainMap;
class TerrainObject {
    unsigned int posX;
    unsigned int posY;
    unsigned int terrainid;
  protected:
    char character;
  public:
    TerrainObject(unsigned int initialPosX, unsigned int initialPosY, unsigned int ids);
    virtual ~TerrainObject();
    unsigned int getX();
    unsigned int getY();
    void setX(unsigned int x);
    void setY(unsigned int y);
    virtual bool update(TerrainMap * const map);
    virtual unsigned int getTerrainID();
    virtual const char getCharacter();
    virtual char const * getName();
    virtual char const * getConversation();
};

#endif
