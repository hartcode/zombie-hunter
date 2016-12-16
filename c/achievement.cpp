#include <achievement.h>
#include <stdlib.h>
#include <string.h>
using namespace std;
Achievement::Achievement() {

}
Achievement::Achievement(unsigned int inid, unsigned int instatus, char const * const inname, char const * const indescription)
{
  id = inid;
  status = instatus;
  if (status != ACHIEVEMENT_STATUS_ACHIEVED) {
    int namelen = strlen(inname)+1 + UNACHIEVED_SIZE;
    name = (char *)calloc(  namelen + 2 ,sizeof(char) );
    strncpy(name, UNACHIEVED_CHAR, UNACHIEVED_SIZE);
    strncpy(&name[UNACHIEVED_SIZE], inname, strlen(inname));
  } else {
    int namelen = strlen(inname)+1 + ACHIEVED_SIZE;
    name = (char *)calloc(  namelen + 2 ,sizeof(char));
    strncpy(name, ACHIEVED_CHAR, ACHIEVED_SIZE);
    strncpy(&name[ACHIEVED_SIZE], inname, strlen(inname));
  }

  int desclen = strlen(indescription)+1;
  description = (char *)calloc(  desclen + 2 ,sizeof(char) );
  strncpy(description, indescription, strlen(indescription));
}
Achievement::~Achievement() {
  if (name != 0)
  {
    free(name);
    name = 0;
  }

  if (description != 0)
  {
    free(description);
    description = 0;
  }
}

unsigned int Achievement::getID(void) {
  return id;
}
unsigned int Achievement::getStatus(void)
{
  return status;
}

void Achievement::reveal() {
    if (getStatus() == ACHIEVEMENT_STATUS_HIDDEN)
    {
      status = ACHIEVEMENT_STATUS_REVEALED;
    }
}
void Achievement::achieve() {
  if (getStatus() != ACHIEVEMENT_STATUS_ACHIEVED)
  {
    status = ACHIEVEMENT_STATUS_ACHIEVED;
  }
}

char const * Achievement::getName() {
  char const * retval = HIDDEN_NAME;
  if (getStatus() != ACHIEVEMENT_STATUS_HIDDEN)
  {
    retval = name;
  }
  return retval;
}
char const * Achievement::getDescription() {
  char const * retval = HIDDEN_DESCRIPTION;
  if (getStatus() != ACHIEVEMENT_STATUS_HIDDEN)
  {
    retval = description;
  }
  return retval;
}
