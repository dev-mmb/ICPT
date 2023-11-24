//
// Created by MB on 22/11/2023.
//

#ifndef STRATEGO_TILE_H
#define STRATEGO_TILE_H

#include <stdbool.h>
#include "../../data/pieces.h"

typedef struct Tile {
    enum PIECE value;
    bool isPlayer;
} Tile;

Tile* constructTile(PIECE value, bool isPlayer);

#endif //STRATEGO_TILE_H
