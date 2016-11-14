#ifndef DISPLAY_H
#define DISPLAY_H

#define CONSOLE_OUT wcout

extern "C" {
  unsigned int getRows(void);
  unsigned int getCols(void);
  void clear(void);
  void printchar(char c);
  void sleepy(unsigned int milliSeconds);
}

#endif
