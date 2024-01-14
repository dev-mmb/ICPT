//
// Created by MB on 22/11/2023.
//
#include <stdlib.h>
#include <printf.h>
#import "board.h"
#include "../util/str.h"

#define BOARD_SIZE 4

Board* constructBoard() {
    Board* board = malloc(sizeof(Board));
    board->w = BOARD_SIZE;
    board->h = BOARD_SIZE;

    board->tiles = malloc(sizeof(Tile) * board->w);
    for (int x = 0; x < board->w; x++) {
        board->tiles[x] = malloc(sizeof(Tile) * board->h);
        struct Tile** row = board->tiles[x];
        for (int y = 0; y < board->h; y++) {
            row[y] = NULL;
        }
    }
    return board;
}
void deleteBoard(Board* board) {
    for (int x = 0; x < board->w; x++) {
        Tile** row = board->tiles[x];
        for (int y = 0; y < board->h; y++) {
            // delete the tile if it exists
            if (row[y] != NULL) 
                free(row[y]);
        }
        free(row);
    }
    free(board->tiles);
    free(board);
}

Board* seedBoard(Board* board) {
    board->tiles[1][2] = constructTile(CONOLEL, false);
    board->tiles[2][2] = constructTile(GENERAL, true);
    return board;
}

void printTileInfo(Tile* tile) {
    String* enemyValueStr = intToString(tile->value);
    String* str = constructString("position contains: ", 19);
    concatString(enemyValueStr, str);

    printf("%s", str->data);
    printf("\n");

    deleteString(enemyValueStr);
    deleteString(str);
}

void move(Board* board, int fromX, int fromY, int toX, int toY) {
    if (fromX < 0 || fromX >= board->h) {
        printf("from x is outside bounds of board!\n");
        return;
    }
    if (fromY < 0 || fromY >= board->w) {
        printf("from y is outside bounds of board!\n");
        return;
    }

    Tile** fromRow = board->tiles[fromX];
    Tile** toRow = board->tiles[toX];

    Tile* from = fromRow[fromY];
    Tile* to = toRow[toY];

    if (from == NULL || from->isPlayer != true) {
        printf("Tile does not contain a player unit\n");
        return;
    }

    if (to == NULL) {
        printf("Moved\n");
        fromRow[fromY] = NULL;
        toRow[toY] = from;
        return;
    }

    printTileInfo(to);

    if (from->value >= to->value) {
        printf("Player value is higher\n");
        free(to);
        fromRow[fromY] = NULL;
        toRow[toY] = from;
        return;
    }
    else {
        printf("Enemy value is higher\n");
        free(from);
        fromRow[fromY] = NULL;
    }    printf("\n");

}

