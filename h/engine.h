#include <avatar.h>
#include <terrainmap.h>
#include <display.h>

#ifndef ENGINE_H
#define ENGINE_H

void game_loop(Display * const display);
void draw(Avatar ** avatar, unsigned int avatarCount, TerrainMap * const map, int * viewX, int * viewY, Display * const display);
void recalculateViewPosition(int * viewX, int * viewY, Display * const display);
#endif
