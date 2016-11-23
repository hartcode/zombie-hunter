#ifndef TERRAINOBJECT_H
#define TERRAINOBJECT_H

const char CHAR_TERRAINOBJECT = ' ';  //"\u263A\x00";

class TerrainObject {
    unsigned int posX;
    unsigned int posY;
  protected:
    char character;
  public:
    TerrainObject(unsigned int initialPosX, unsigned int initialPosY);
    virtual ~TerrainObject();
    unsigned int getX();
    unsigned int getY();
    void setX(unsigned int x);
    void setY(unsigned int y);
    virtual const char getCharacter();
    virtual char const * getName();
    virtual char const * getConversation();
};

#endif
