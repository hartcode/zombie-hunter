#include <terrainmap.h>
#include <avatar.h>

#ifndef ENGINE_H
#define ENGINE_H

void game_loop(void);
void draw(Avatar ** avatar, unsigned int avatarCount, TerrainMap * const map, int viewX, int viewY);
void recalculateViewPosition(void);
#endif
