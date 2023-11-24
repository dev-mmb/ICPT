#include <stdio.h>
#include <stdbool.h>
#include <string.h>
#include "src/board/board.h"
#include "src/util/util.h"

char buffer[100];

int main() {
    Board* board = constructBoard();
    board = seedBoard(board);
    int fromX = 0, fromY = 0;

    while (true) {
        printBoard(board);
        printf("\nfrom x: ");
        scanf("%d", &fromX);
        printf("\nfrom y: ");
        scanf("%d", &fromY);

        printf("\nup, down, left or right: ");
        scanf("%s", buffer);
        if (strcmp(buffer, "up") == 0)
            move(board, fromX, fromY, fromX - 1, fromY);
        else if (strcmp(buffer, "down") == 0)
            move(board, fromX, fromY, fromX + 1, fromY);
        else if (strcmp(buffer, "left") == 0)
            move(board, fromX, fromY, fromX, fromY - 1);
        else if (strcmp(buffer, "right") == 0)
            move(board, fromX, fromY, fromX, fromY + 1);
        else if (strcmp(buffer, "quit") == 0) break;
        else printf("unknown command!");
    }

    deleteBoard(board);

    return 0;
}
