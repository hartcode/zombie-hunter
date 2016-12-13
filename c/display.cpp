#include <display.h>
#include <stdarg.h>
#include <stdio.h>
#include <stdlib.h>
#include <locale.h>
#include <input.h>
#include <iostream>
#include <string.h>
using namespace std;

#ifndef WINDOWS
  #include <sys/ioctl.h>
#endif

#include <ncurses.h>
#include <menu.h>
#define ARRAY_SIZE(a) (sizeof(a) / sizeof(a[0]))
#define CTRLD 	4

void print_in_middle(WINDOW *win, int starty, int startx, int width, char const * const string, chtype color);
WINDOW *create_newwin(int height, int width, int starty, int startx);
void destroy_win(WINDOW *local_win);

Display::Display(Input * const in)
{
  initscr();
  scrollok(stdscr,TRUE);
  curs_set(0);
  cols = 0;
  rows = 0;
  input = in;
  getmaxyx(stdscr, rows, cols);
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

void Display::print(const char * string, ...) {
  va_list args;
  va_start(args, string);
  vwprintw(stdscr, string, args);
  va_end(args);
}

void Display::printChar(char c)
{
  print("%c", c);
}

void Display::printConversation(const char * title, const char * string) {
  WINDOW* win;
  int height = 5;
  int width = cols-20;
  int namesize = strlen(title);
  int stringsize = strlen(string);
  win = create_newwin(height, width, (rows - height)/2, (cols - width)/2 );
  mvwprintw(win, 1, (width - namesize)/2, title);
  mvwprintw(win, 3, (width - stringsize)/2, string);
  wrefresh(win);
  getanykey();
  destroy_win(win);
}

int Display::displayMenu(){
  int retval = MENU_CANCEL;
  const char *choices[] = {
                          "Achievements",
                          "Quit",
                          (char *)NULL,
                    };
  int height = 10;
  int width = 20;
  ITEM **my_items;
  int c;
	MENU *my_menu;
  WINDOW* win;
  WINDOW* subwin;
	int n_choices, i;
	ITEM *cur_item;
  win = create_newwin(height, width, (rows - height) /2, (cols - width)/2);
  keypad(win, TRUE);// Set main window and sub window

  // Create items
  n_choices = ARRAY_SIZE(choices);
  my_items = (ITEM **)calloc(n_choices, sizeof(ITEM *));
  for(i = 0; i < n_choices; ++i)
     my_items[i] = new_item(choices[i], "");

	// Crate menu
	my_menu = new_menu((ITEM **)my_items);

  set_menu_win(my_menu, win);
  subwin = derwin(win, 6, width-2, 3, 1);
  set_menu_sub(my_menu, subwin);

	// Set menu mark to the string " * "
  set_menu_mark(my_menu, " * ");

	// Print a border around the main window and print a title

	print_in_middle(win, 1, 0, width, "Menu", COLOR_PAIR(1));
	mvwaddch(win, 2, 0, ACS_LTEE);
	mvwhline(win, 2, 1, ACS_HLINE, width-2);
	mvwaddch(win, 2, width-1, ACS_RTEE);
	wrefresh(win);

	post_menu(my_menu);
	wrefresh(win);
	while(c != 27)
  {
    c = wgetch(win);
    switch(c)
	    {
      case KEY_DOWN:
		        menu_driver(my_menu, REQ_DOWN_ITEM);
			  break;
			case KEY_UP:
				menu_driver(my_menu, REQ_UP_ITEM);
				break;
      case KEY_NPAGE:
				menu_driver(my_menu, REQ_SCR_DPAGE);
				break;
			case KEY_PPAGE:
				menu_driver(my_menu, REQ_SCR_UPAGE);
				break;
      case 10:
			  const char * iname;
				cur_item = current_item(my_menu);
        iname = item_name(cur_item);
        if (strncmp(iname,choices[0],sizeof(*choices[0])) == 0)
        {
          retval = MENU_ACHIEVEMENTS;
        } else if (strncmp(iname,choices[1],sizeof(*choices[1])) == 0)
        {
          retval = MENU_EXIT;
        }
          c = 27;
				break;
		  }
    wrefresh(win);
	}

    unpost_menu(my_menu);
    free_menu(my_menu);
    for(i = 0; i < n_choices; ++i)
        free_item(my_items[i]);
    destroy_win(win); // and delete

  return retval;
}

void Display::getanykey() {
  double milliseconds;
  while (input->getkey(&milliseconds) == 0)
  {}  // getkey has a built in 100 millisecond wait
}

void Display::draw() {
  refresh();
}

WINDOW *create_newwin(int height, int width, int starty, int startx)
{  WINDOW *local_win;

  local_win = newwin(height, width, starty, startx);
  box(local_win, 0 , 0);    /* 0, 0 gives default characters
           * for the vertical and horizontal
           * lines      */
  wrefresh(local_win);    /* Show that box     */

  return local_win;
}

void destroy_win(WINDOW *local_win)
{
  /* box(local_win, ' ', ' '); : This won't produce the desired
   * result of erasing the window. It will leave it's four corners
   * and so an ugly remnant of window.
   */
  wborder(local_win, ' ', ' ', ' ',' ',' ',' ',' ',' ');
  /* The parameters taken are
   * 1. win: the window on which to operate
   * 2. ls: character to be used for the left side of the window
   * 3. rs: character to be used for the right side of the window
   * 4. ts: character to be used for the top side of the window
   * 5. bs: character to be used for the bottom side of the window
   * 6. tl: character to be used for the top left corner of the window
   * 7. tr: character to be used for the top right corner of the window
   * 8. bl: character to be used for the bottom left corner of the window
   * 9. br: character to be used for the bottom right corner of the window
   */
  wrefresh(local_win);
  delwin(local_win);
}

void print_in_middle(WINDOW *win, int starty, int startx, int width, char const * const string, chtype color)
{	int length, x, y;
	float temp;

	if(win == NULL)
		win = stdscr;
	getyx(win, y, x);
	if(startx != 0)
		x = startx;
	if(starty != 0)
		y = starty;
	if(width == 0)
		width = 80;

	length = strlen(string);
	temp = (width - length)/ 2;
	x = startx + (int)temp;
	wattron(win, color);
	mvwprintw(win, y, x, "%s", string);
	wattroff(win, color);
	refresh();
}

void Display::displayAchievements(Achievement ** achievements) {
  int height = 10;
  int width = 60;
  ITEM **my_items;
  int c = 0;
  MENU *my_menu;
  WINDOW* win;
  WINDOW* subwin;
  int n_choices, i;

  win = create_newwin(height, width, (rows - height) /2, (cols - width)/2);
  keypad(win, TRUE); // Set main window and sub window

  // Create items
  n_choices = ACHIEVEMENT_SIZE + 1;
  my_items = (ITEM **)calloc(n_choices, sizeof(ITEM *));
  for(i = 0; i < n_choices-1; ++i) {
     my_items[i] = new_item((*achievements)->getName(), (*achievements)->getDescription());
     if ((*achievements)->getStatus() == ACHIEVEMENT_STATUS_ACHIEVED) {
       set_item_value(my_items[i], 1);
     }
     achievements++;
   }
   my_items[i] = new_item((char *)NULL, (char *)NULL);


  // Crate menu
  my_menu = new_menu((ITEM **)my_items);

  menu_opts_off(my_menu, O_ONEVALUE);

  set_menu_win(my_menu, win);
  subwin = derwin(win, 6, width-2, 3, 1);
  set_menu_sub(my_menu, subwin);

  set_menu_format(my_menu, 6, 1);
  // Set menu mark to the string " * "
  set_menu_mark(my_menu, " * ");
  //set_menu_opts(my_menu, O_ONEVALUE);

  // Print a border around the main window and print a title
  print_in_middle(win, 1, 0, width, "Achievements", COLOR_PAIR(1));
  mvwaddch(win, 2, 0, ACS_LTEE);
  mvwhline(win, 2, 1, ACS_HLINE, width-2);
  mvwaddch(win, 2, width-1, ACS_RTEE);
  wrefresh(win);
  post_menu(my_menu);
  wrefresh(win);
  while(c != 27)
  {
    c = wgetch(win);
    switch(c)
      {
      case KEY_DOWN:
        menu_driver(my_menu, REQ_DOWN_ITEM);
        break;
      case KEY_UP:
        menu_driver(my_menu, REQ_UP_ITEM);
        break;
      }
    wrefresh(win);
  }

    unpost_menu(my_menu);
    free_menu(my_menu);
    for(i = 0; i < n_choices; ++i)
        free_item(my_items[i]);
    destroy_win(win); // and delete
}
