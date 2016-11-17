#include <avatar.h>
#include <terrainmap.h>
#include <display.h>
#include <input.h>

#ifndef ENGINE_H
#define ENGINE_H

void game_loop(Display * const display, Input * const in);
void draw(Avatar ** avatar, unsigned int avatarCount, TerrainMap * const map, int * viewX, int * viewY, Display * const display);
void recalculateViewPosition(int * viewX, int * viewY, Display * const display);
#endif
