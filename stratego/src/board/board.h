//
// Created by MB on 22/11/2023.
//

#ifndef STRATEGO_BOARD_H
#define STRATEGO_BOARD_H
typedef struct Board  {
    struct Tile*** tiles;
    int w;
    int h;
} Board;


Board* constructBoard();
Board* seedBoard(Board* board);
void move(Board* board, int fromX, int fromY, int toX, int toY);
void deleteBoard(Board* board);

#endif //STRATEGO_BOARD_H
