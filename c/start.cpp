#include <start.h>
#include <input.h>
#include <display.h>

using namespace std;

void start_screen(Display * const display, Input * const input) {
  display->clear();
  display->print("Zombie Hunter 100");
  display->draw();
  display->getanykey();

  display->clear();
  display->print("The year is 2016 and the human race has fallen to zombies.\n\n");
  display->draw();
  input->sleepy(1000);
  display->print("In a desperate attempt to save humanity an advanced Artificial Intelligence was developed in hopes of finding a cure.\n\n");
  display->draw();
  input->sleepy(2000);
  display->print("A cure was found, but it was too late, the last humans have become mindless zombies.\n\n");
  display->draw();
  input->sleepy(1000);
  display->print("With the humans gone, the Artificial Intelligence created 'Zombie Hunter' class robots to deliver the cure to the humans.\n\n");
  display->draw();
  input->sleepy(2000);
  display->print("As a Zombie Hunter it's up to YOU to save humanity.\n\n");
  display->draw();

  display->getanykey();
}
