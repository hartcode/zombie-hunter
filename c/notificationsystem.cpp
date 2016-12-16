#include <notificationsystem.h>
#include <display.h>
NotificationSystem::NotificationSystem(Display * const indisplay) {
  display = indisplay;
  durationInTicks = -1;
}

NotificationSystem::~NotificationSystem() {

}

void NotificationSystem::showNotification(const char * note, int indurationInTicks)
{
    durationInTicks = indurationInTicks;
    display->createNotification(note);
}

void NotificationSystem::update() {
  if (durationInTicks >= 0) {
    if (durationInTicks == 0){
      display->destroyNotification();
    }
    durationInTicks--;
  }
}
