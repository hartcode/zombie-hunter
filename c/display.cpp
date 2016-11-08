
#include <display.h>
#ifdef WINDOWS
  #include <windows.h>
#else
  #include <stdio.h>
  #include <unistd.h>
  #include <sys/ioctl.h>
#endif

unsigned int getRows() {
  unsigned int rows = 0;
  #ifdef WINDOWS
    CONSOLE_SCREEN_BUFFER_INFO csbi;
    GetConsoleScreenBufferInfo(GetStdHandle(STD_OUTPUT_HANDLE), &csbi);
    rows = csbi.srWindow.Bottom - csbi.srWindow.Top + 1;
  #else
    struct winsize w;
    ioctl(STDOUT_FILENO, TIOCGWINSZ, &w);
    rows = w.ws_col;
  #endif
  return rows;
}

unsigned int getCols() {
  unsigned int cols = 0;
  #ifdef WINDOWS
    CONSOLE_SCREEN_BUFFER_INFO csbi;
    GetConsoleScreenBufferInfo(GetStdHandle(STD_OUTPUT_HANDLE), &csbi);
    cols = csbi.srWindow.Right - csbi.srWindow.Left + 1;
  #else
    struct winsize w;
    ioctl(STDOUT_FILENO, TIOCGWINSZ, &w);
    cols = w.ws_col;
  #endif
  return cols;
}
