#include <avatar.h>
#include <bullet.h>
#include <terrainmap.h>
#include <stdlib.h>

Bullet::Bullet(unsigned int initialPosX, unsigned int initialPosY) : Avatar(initialPosX, initialPosY) {
  movementFrameStep = (rand()%3)*10;  // Start at 0, 10, 20
  movomentFrame = MOVEMENT_FRAME_BULLET;
}

Bullet::~Bullet() {}

char Bullet::getCharacter() {
  return CHAR_BULLET;
}

bool Bullet::update( TerrainMap * const map) {
  bool retval = false;
  movementFrameStep++;
  if (movementFrameStep >= movomentFrame)
  {
    // do movement

    int vx = 0;
    int vy = 0;

    switch (getDirection())
    {
        case AVATAR_DIRECTION_UP:
        vx--;
        break;
        case AVATAR_DIRECTION_DOWN:
        vx++;
        break;
        case AVATAR_DIRECTION_LEFT:
        vy--;
        break;
        case AVATAR_DIRECTION_RIGHT:
        vy++;
        break;
    }
    if (map->getAt(getX() + vx, getY() + vy) == TERRAIN_EMPTY) {
       map->setAt(getX(), getY(), TERRAIN_EMPTY);
       setX(getX() + vx);
       setY(getY() + vy);
       map->setAt(getX(), getY(), TERRAIN_BADDIE);
    }
    retval = true;
    movementFrameStep = 0;
  }
  return retval;
}
