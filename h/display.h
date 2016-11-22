#include <input.h>

#ifndef DISPLAY_H
#define DISPLAY_H

#define CONSOLE_OUT wcout
#define NEWLINE_CHAR '\n'

class Display {
  unsigned int rows;
  unsigned int cols;
  Input * input;
public:
  Display(Input * const in);
  virtual ~Display();
  unsigned int getRows(void);
  unsigned int getCols(void);
  void clear();
  void print(const char * string, ...);
  void printChar(char);
  void getanykey();
  void draw();
  void printConversation(const char *, const char *);
};

#endif
