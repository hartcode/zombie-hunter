#include <display.h>
#include <stdarg.h>
#include <stdio.h>
#include <locale.h>
#include <input.h>
#include <iostream>
#include <string.h>
using namespace std;

#ifndef WINDOWS
  #include <sys/ioctl.h>
#endif

#include <ncurses.h>
WINDOW *create_newwin(int height, int width, int starty, int startx);
void destroy_win(WINDOW *local_win);

Display::Display(Input * const in)
{
  initscr();
  curs_set(0);
  cols = 0;
  rows = 0;
  input = in;
  getmaxyx(stdscr, rows, cols);
}

Display::~Display() {
  endwin();
}

unsigned int Display::getRows(void) {
  return rows;
}

unsigned int Display::getCols(void) {
  return cols;
}

void Display::clear() {
  erase();
}

void Display::print(const char * string, ...) {
  va_list args;
  va_start(args, string);
  vwprintw(stdscr, string, args);
  va_end(args);
}

void Display::printChar(char c)
{
  print("%c", c);
}

void Display::printConversation(const char * title, const char * string) {
  WINDOW* win;
  int height = 5;
  int width = cols-20;
  int namesize = strlen(title);
  int stringsize = strlen(string);
  win = create_newwin(height, width, (rows - height)/2, (cols - width)/2 );
  mvwprintw(win, 1, (width - namesize)/2, title);
  mvwprintw(win, 3, (width - stringsize)/2, string);
  wrefresh(win);
  getanykey();
  destroy_win(win);
}

void Display::getanykey() {
  double milliseconds;
  while (input->getkey(&milliseconds) == 0)
  {}  // getkey has a built in 100 millisecond wait
}

void Display::draw() {
  refresh();
}

WINDOW *create_newwin(int height, int width, int starty, int startx)
{  WINDOW *local_win;

  local_win = newwin(height, width, starty, startx);
  box(local_win, 0 , 0);    /* 0, 0 gives default characters
           * for the vertical and horizontal
           * lines      */
  wrefresh(local_win);    /* Show that box     */

  return local_win;
}

void destroy_win(WINDOW *local_win)
{
  /* box(local_win, ' ', ' '); : This won't produce the desired
   * result of erasing the window. It will leave it's four corners
   * and so an ugly remnant of window.
   */
  wborder(local_win, ' ', ' ', ' ',' ',' ',' ',' ',' ');
  /* The parameters taken are
   * 1. win: the window on which to operate
   * 2. ls: character to be used for the left side of the window
   * 3. rs: character to be used for the right side of the window
   * 4. ts: character to be used for the top side of the window
   * 5. bs: character to be used for the bottom side of the window
   * 6. tl: character to be used for the top left corner of the window
   * 7. tr: character to be used for the top right corner of the window
   * 8. bl: character to be used for the bottom left corner of the window
   * 9. br: character to be used for the bottom right corner of the window
   */
  wrefresh(local_win);
  delwin(local_win);
}
