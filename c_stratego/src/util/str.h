//
// Created by MB on 22/11/2023.
//

#ifndef STRATEGO_STR_H
#define STRATEGO_STR_H

#include "../tile/tile.h"

typedef struct String {
    char* data;
    int size;
} String;

void concatString(String* left, String* right);
String* constructString(const char* data, int size);
String* intToString(int num);
String* tileToString(Tile* tile);
void deleteString(String* string);

#endif //STRATEGO_STR_H
