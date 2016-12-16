#include <achievement.h>
#include <input.h>

#include <ncurses.h>

#ifndef DISPLAY_H
#define DISPLAY_H

#define CONSOLE_OUT wcout
#define NEWLINE_CHAR '\n'

#define MENU_CANCEL 0
#define MENU_ACHIEVEMENTS 1
#define MENU_EXIT 2

class Display {
  unsigned int rows;
  unsigned int cols;
  Input * input;
  WINDOW* notificationWindow;
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
  int displayMenu();
  void displayAchievements(Achievement ** achievements);
  void createNotification(const char *);
  void destroyNotification();
};

#endif
