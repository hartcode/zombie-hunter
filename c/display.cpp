#include <display.h>
#include <stdio.h>
#include <iostream>
using namespace std;

#ifdef WINDOWS
  #include <windows.h>
  #include <conio.h>
#else
  #include <stdio.h>
  #include <unistd.h>
  #include <sys/ioctl.h>
#endif

unsigned int getRows(void) {
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

unsigned int getCols(void) {
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

void clear(void) {
  #ifdef WINDOWS
   DWORD n;                         /* Number of characters written */
   DWORD size;                      /* number of visible characters */
   COORD coord = {0};               /* Top left screen position */
   CONSOLE_SCREEN_BUFFER_INFO csbi;

   /* Get a handle to the console */
   HANDLE h = GetStdHandle ( STD_OUTPUT_HANDLE );

   GetConsoleScreenBufferInfo ( h, &csbi );

   /* Find the number of characters to overwrite */
   size = csbi.dwSize.X * csbi.dwSize.Y;

   /* Overwrite the screen buffer with whitespace */
   FillConsoleOutputCharacter ( h, TEXT ( ' ' ), size, coord, &n );
   GetConsoleScreenBufferInfo ( h, &csbi );
   FillConsoleOutputAttribute ( h, csbi.wAttributes, size, coord, &n );

   /* Reset the cursor to the top left position */
   SetConsoleCursorPosition ( h, coord );
  #else
   fprintf(stdout, "\033[2J");
   fprintf(stdout, "\033[1;1H");
   #endif
}

void printchar(char c)
{
  CONSOLE_OUT << c;
}

void sleepy(unsigned int milliSeconds) {
  #if WINDOWS
    Sleep(milliSeconds);
  #else
    sleep(milliSeconds);
  #endif
}
