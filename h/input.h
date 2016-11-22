#ifdef WINDOWS
  typedef unsigned long DWORD;
  typedef void * HANDLE;
#endif

#ifndef INPUT_H
#define INPUT_H

const int ESCAPE_KEY = 1;
const int LEFT_KEY = 2;
const int RIGHT_KEY = 3;
const int UP_KEY = 4;
const int DOWN_KEY = 5;
const int FIRE_KEY = 6;

class Input {
  #ifdef WINDOWS
    HANDLE hInput;
    DWORD fdwOldMode;
  #endif
public:
  Input();
  virtual ~Input();
  int getkey(double * milliseconds);
  void sleepy(unsigned int milliSeconds);
};
#endif
