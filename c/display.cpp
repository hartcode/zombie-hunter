#include <wchar.h>
#include <display.h>
#include <stdio.h>
#include <iostream>
using namespace std;

#ifdef WINDOWS
  #include <windows.h>
#else
  #include <unistd.h>
  #include <sys/ioctl.h>
#endif

#include <ncurses/ncurses.h>

Display::Display()
{
  initscr();
  getmaxyx(stdscr, rows,cols);
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

void Display::print(const char * string,...) {
  va_list args;
  va_start(args,string);
  printw(string,args);
  va_end(args);
}

void Display::getanykey() {
  getch();
}

void Display::draw() {
  refresh();
}

void sleepy(unsigned int milliSeconds) {
  #if WINDOWS
    Sleep(milliSeconds);
  #else
    sleep(milliSeconds);
  #endif
}
