#include <input.h>
#include <stdio.h>
using namespace std;

#ifdef WINDOWS
  #include <windows.h>
#endif



int getkey(void) {
  int retval = 0;
  #ifdef WINDOWS
    HANDLE hInput = GetStdHandle(STD_INPUT_HANDLE);
    DWORD NumInputs = 0;
    DWORD InputsRead = 0;
    INPUT_RECORD irInput;
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
  #endif
  return retval;
}
