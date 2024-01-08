//
// Created by MB on 23/11/2023.
//
#include "str.h"
#include <stdlib.h>
#include <stdio.h>
#include <string.h>


void concatString(String* left, String* right) {
    String* result = malloc(sizeof(String));
    result->data = malloc(sizeof(char) * (left->size + right->size));
    strcpy(result->data, left->data);
    strcat(result->data, right->data);
    free(left->data);
    left->data = result->data;
    left->size = result->size;
    free(result);
}

String* constructString(const char* data, int size) {
    String* str = malloc(sizeof(String));
    str->data = malloc(sizeof(char) * size);
    str->size = size;
    for (int i = 0; i < size; i++) {
        str->data[i] = data[i];
    }
    return str;
}

String* intToString(int num) {
    char numBuffer[10];
    int len = sprintf(numBuffer, "%d", num);
    return constructString(numBuffer, len);
}

String* tileToString(Tile* tile) {
    if (tile->value != 0) {
        if (tile->isPlayer == true) return intToString(tile->value);
        else return constructString("*", 1);
    }
    return constructString("0", 1);
}

void deleteString(String* string) {
    free(string->data);
    free(string);
}