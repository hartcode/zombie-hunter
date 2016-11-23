#include <terrainobject.h>
#ifndef GRAVESTONE_H
#define GRAVESTONE_H

const char CHAR_GRAVESTONE = '#'; //"\u03A9\x00";//35;

class Gravestone : public TerrainObject {
  public:
    Gravestone(unsigned int initialPosX, unsigned int initialPosY);
    virtual ~Gravestone();
    virtual char const * getName();
    virtual char const * getConversation();
};

#endif
