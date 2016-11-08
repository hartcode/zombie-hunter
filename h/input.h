#ifndef INPUT_H
#define INPUT_H

extern "C" {
  const int ESCAPE_KEY = 1;
  const int LEFT_KEY = 2;
  const int RIGHT_KEY = 3;
  const int UP_KEY = 4;
  const int DOWN_KEY = 5;
  const int FIRE_KEY = 6;
  int getkey(void);
}
#endif
