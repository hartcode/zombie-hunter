#include <avatar.h>
#include <terrainmap.h>

#ifndef BULLET_H
#define BULLET_H

const char CHAR_BULLET = '*';


const unsigned int MOVEMENT_FRAME_BULLET = 10;


class Bullet : public Avatar {
  unsigned int movementFrameStep;
  unsigned int movomentFrame;
  public:
    Bullet(unsigned int initialPosX, unsigned int initialPosY);
    virtual ~Bullet();
    char getCharacter();
    bool update(TerrainMap * const map);
};

#endif
