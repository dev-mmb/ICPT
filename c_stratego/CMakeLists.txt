cmake_minimum_required(VERSION 3.26)
project(stratego C)

set(CMAKE_C_STANDARD 11)

add_executable(stratego main.c
        data/pieces.h
        src/board/board.h
        src/board/board.c
        src/tile/tile.h
        src/tile/tile.c
        src/util/util.h
        src/util/str.h
        src/util/util.c
        src/util/str.c
)
