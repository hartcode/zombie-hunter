#include <input.h>
#include <stdio.h>
#include <time.h>
using namespace std;

#ifdef WINDOWS
  #include <windows.h>
#endif

Input::Input() {
  #ifdef WINDOWS
    hInput = GetStdHandle(STD_INPUT_HANDLE);
    DWORD fdwMode;
  // disable mouse and window input
    GetConsoleMode(hInput, &fdwOldMode);
    fdwMode = fdwOldMode && ENABLE_MOUSE_INPUT && ENABLE_WINDOW_INPUT;
    SetConsoleMode(hInput, fdwMode);
    FlushConsoleInputBuffer(hInput);
  #endif
}

Input::~Input() {
  #ifdef WINDOWS
    // reset console mode
    SetConsoleMode(hInput, fdwOldMode);
  #endif
}

int Input::getkey(double * milliseconds) {
  int retval = 0;
  #ifdef WINDOWS
  DWORD NumInputs = 0;
  DWORD InputsRead = 0;
  INPUT_RECORD irInput;

    time_t now;
    time_t later;
    time(&now);

    if (WaitForSingleObject(hInput,100) == WAIT_OBJECT_0)
    {
      time(&later);
      *milliseconds = 100 - (double)(later - now);
      GetNumberOfConsoleInputEvents(hInput, &NumInputs);
      ReadConsoleInput(hInput, &irInput, 1, &InputsRead);
      for (DWORD i = 0; i < InputsRead; i++) {
        if (irInput.EventType == KEY_EVENT && irInput.Event.KeyEvent.bKeyDown) {
          switch(irInput.Event.KeyEvent.wVirtualKeyCode)
          {
            case VK_ESCAPE:
              retval = ESCAPE_KEY;
              break;

            case VK_LEFT:
              retval = LEFT_KEY;
              break;

            case VK_UP:
              retval = UP_KEY;
              break;

            case VK_RIGHT:
              retval = RIGHT_KEY;
              break;

            case VK_DOWN:
              retval = DOWN_KEY;
              break;

            case VK_SPACE:
              retval = FIRE_KEY;
              break;
          }
        }
      }
    }

  #endif
  return retval;
}

void Input::sleepy(unsigned int milliSeconds) {
  #if WINDOWS
    Sleep(milliSeconds);
  #else
    sleep(milliSeconds);
  #endif
}
