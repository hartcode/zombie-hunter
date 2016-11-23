#include <terrainmap.h>
#include <terrainobject.h>

#ifndef AVATAR_H
#define AVATAR_H

const char CHAR_AVATAR = '@';  //"\u263A\x00";

const unsigned int AVATAR_DIRECTION_UNKNOWN = 0;
const unsigned int AVATAR_DIRECTION_UP = 1;
const unsigned int AVATAR_DIRECTION_DOWN = 2;
const unsigned int AVATAR_DIRECTION_LEFT = 3;
const unsigned int AVATAR_DIRECTION_RIGHT = 4;

class Avatar : public TerrainObject {
    unsigned int direction;
  public:
    Avatar(unsigned int initialPosX, unsigned int initialPosY);
    virtual ~Avatar();
    void setDirection(unsigned int newDirection);
    unsigned int getDirection();
    virtual bool update(TerrainMap * const map);
    virtual char const * getName();
    virtual char const * getConversation();
};

#endif
