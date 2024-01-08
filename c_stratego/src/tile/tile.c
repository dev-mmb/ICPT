#include "tile.h"
#include <stdlib.h>

//
// Created by MB on 22/11/2023.
//
Tile* constructTile(PIECE value, bool isPlayer) {
    Tile* tile = malloc(sizeof(Tile));
    tile->value = value;
    tile->isPlayer = isPlayer;
    return tile;
}