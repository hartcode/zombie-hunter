#include <display.h>
#ifndef NOTIFICATIONSYSTEM_H
#define NOTIFICATIONSYSTEM_H

class NotificationSystem {
  unsigned int durationInTicks;
  Display * display;
public:
  NotificationSystem(Display * const indisplay);
  virtual ~NotificationSystem();
  void showNotification(const char * note, int indurationInTicks);
  void update();
  };

#endif
