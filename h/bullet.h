#include <avatar.h>
#include <terrainmap.h>

#ifndef BULLET_H
#define BULLET_H

const char CHAR_BULLET = '*'; //"\u002A\x00";


const unsigned int MOVEMENT_FRAME_BULLET = 1;


class Bullet : public Avatar {
  unsigned int movementFrameStep;
  unsigned int movomentFrame;
  bool fired;
  public:
    Bullet(unsigned int initialPosX, unsigned int initialPosY);
    virtual ~Bullet();
    const char getCharacter();
    bool update(TerrainMap * const map);
    void setFired(bool b);
    bool getFired();
};

#endif
