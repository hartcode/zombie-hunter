#ifndef ACHIEVEMENT_H
#define ACHIEVEMENT_H

#define ACHIEVEMENT_STATUS_HIDDEN 0
#define ACHIEVEMENT_STATUS_REVEALED 1
#define ACHIEVEMENT_STATUS_ACHIEVED 2

#define ACHIEVEMENT_SIZE 10

const int UNACHIEVED_SIZE = 4;
const int ACHIEVED_SIZE = 4;
char const * const UNACHIEVED_CHAR = "[ ] ";
char const * const ACHIEVED_CHAR = "[X] ";

char const * const HIDDEN_NAME = "???????";

char const * const HIDDEN_DESCRIPTION = "- ???????";

class Achievement {
  unsigned int id;
  unsigned int status;
  char * name;
  char * description;
public:
  Achievement();
  Achievement(unsigned int inid, unsigned int instatus, char const * const inname, char const * const indescription);
  virtual ~Achievement();
  unsigned int getID(void);
  unsigned int getStatus(void);
  void reveal();
  void achieve();
  virtual char const * getName();
  virtual char const * getDescription();
};

#endif
