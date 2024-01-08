//
// Created by MB on 22/11/2023.
//
#import "util.h"
#include "str.h"
#include <stdio.h>

void printFirstNums(int size) {
    String* str = constructString("", 0);
    String* space = constructString(" ", 1);

    concatString(str, space);
    for (int x = 0; x < size; x++) {
        String* num = intToString(x);

        concatString(str, space);
        concatString(str, num);

        deleteString(num);
    }
    printf("%s", str->data);
    printf(" y ");
    deleteString(str);
    deleteString(space);
}



void printRow(Tile** tiles, int width, int y) {
    String* result = intToString(y);
    String* space = constructString(" ", 1);

    concatString(result, space);
    for (int x = 0; x < width; x++) {
        Tile* tile = tiles[x];
        String* tileStr;
        if (tile != NULL) {
            tileStr = tileToString(tile);
        }
        else {
            tileStr = constructString("0", 1);
        }
        concatString(result, tileStr);
        deleteString(tileStr);
        concatString(result, space);
    }

    printf("%s", result->data);

    deleteString(space);
    deleteString(result);
}

void printBoard(Board* board) {
    printFirstNums(board->w);
    printf("\n");
    for (int y = 0; y < board->h; y++) {
        struct Tile** row = board->tiles[y];
        printRow(row, board->w, y);
        printf("\n");
    }
    printf("x\n");
}