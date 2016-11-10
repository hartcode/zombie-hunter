#ifndef AVATAR_H
#define AVATAR_H

const char CHAR_AVATAR = 2;

class Avatar {
    unsigned int posX;
    unsigned int posY;
  protected:
    char character;
  public:
    Avatar(unsigned int initialPosX, unsigned int initialPosY);
    virtual ~Avatar();
    unsigned int getX();
    unsigned int getY();
    void setX(unsigned int x);
    void setY(unsigned int y);
    virtual char getCharacter();
    virtual bool update();
};

#endif
