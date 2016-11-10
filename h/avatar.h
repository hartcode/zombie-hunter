#ifndef AVATAR_H
#define AVATAR_H

const char CHAR_AVATAR = 228;

const unsigned int AVATAR_DIRECTION_UNKNOWN = 0;
const unsigned int AVATAR_DIRECTION_UP = 1;
const unsigned int AVATAR_DIRECTION_DOWN = 2;
const unsigned int AVATAR_DIRECTION_LEFT = 3;
const unsigned int AVATAR_DIRECTION_RIGHT = 4;

class Avatar {
    unsigned int posX;
    unsigned int posY;
    unsigned int direction;
  protected:
    char character;
  public:
    Avatar(unsigned int initialPosX, unsigned int initialPosY);
    virtual ~Avatar();
    unsigned int getX();
    unsigned int getY();
    void setX(unsigned int x);
    void setY(unsigned int y);
    void setDirection(unsigned int newDirection);
    unsigned int getDirection();
    virtual char getCharacter();
    virtual bool update();
};

#endif
