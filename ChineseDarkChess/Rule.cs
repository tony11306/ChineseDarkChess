using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseDarkChess {


    /*  痾.. 將錯就錯吧
        (0, 0), (1, 0), (2, 0)...., (7, 0)
        (0, 1), (1, 1), (2, 1)...., (7, 1)
        .
        .
        .
        (0, 7), (1, 7), (2, 7)...., (7, 7)

     */
    class Rule {
        public const int BOARD_WIDTH = 8;
        public const int BOARD_HEIGHT = 4;
        public const int PIECES_NUMBER_FOR_EACH_PLAYER = 16;

        // The function does not consider the flip action.
        public static bool isValidMove(int[,] board, MoveData moveData) {

            // if the board is not 8 by 3, then throw exception.
            if (board.GetLength(0) != BOARD_WIDTH || board.GetLength(1) != BOARD_HEIGHT) {
                throw new Exception("The board size must be 8 by 3, where 8 is for width, and 3 is for height.");
            }


            int fromX = moveData.fromX;
            int fromY = moveData.fromY;
            int toX = moveData.toX;
            int toY = moveData.toY;

            // if it does not move, then return false.
            if (fromX == toX && fromY == toY) {
                return false;
            }

            // if the moving piece is not cannon and the distance it moved is not equal to 1, then early return.
            if (!isSameTypePiece((int)PieceEnum.BlackCannon, board[fromX, fromY]) && Math.Abs(fromX - toX) + Math.Abs(fromY - toY) != 1) {
                return false;
            }

            // if the moving piece is unflip, then return false.
            if (isSameTypePiece((int)PieceEnum.Unflip, board[fromX, fromY])) {
                return false;
            }

            // if the moving piece is empty, then return false.
            if (isSameTypePiece((int)PieceEnum.Empty, board[fromX, fromY])) {
                return false;
            }

            // if it's attacking a unflip piece, then return false.
            if (isSameTypePiece((int)PieceEnum.Unflip, board[toX, toY])) {
                return false;
            }

            // if the moving piece moves to a empty square and it's not cannon, then return true.
            if (isSameTypePiece((int)PieceEnum.Empty, board[toX, toY]) && !isSameTypePiece((int)PieceEnum.BlackCannon, board[fromX, fromY])) {
                return true;
            }

            // if it's attacking ally, then return false.
            if (isAttackingAlly(board[fromX, fromY], board[toX, toY])) {
                return false;
            }

            // cannon is special case
            if (isSameTypePiece((int)PieceEnum.BlackCannon, board[fromX, fromY])) {

                if (Math.Abs(fromX - toX) + Math.Abs(fromY - toY) == 1 && isSameTypePiece((int)PieceEnum.Empty, board[toX, toY])) {
                    return true;
                } else {

                    int barrierCount = 0;
                    if (fromX == toX && fromY != toY) { // if it moves horizontally
                       
                        for (int i = Math.Min(fromY, toY) + 1; i < Math.Max(fromY, toY); ++i) {
                            if (!isSameTypePiece((int)PieceEnum.Empty, board[fromX, i])) {
                                barrierCount++;
                            }
                        }

                    } else if (fromX != toX && fromY == toY) { // if it moves vertically
                        
                        for (int i = Math.Min(fromX, toX) + 1; i < Math.Max(fromX, toX); ++i) {
                            if (!isSameTypePiece((int)PieceEnum.Empty, board[i, fromY])) {
                                barrierCount++;
                            }
                        }

                    } else { // if it moves diagonally.
                        return false;
                    }

                    return barrierCount == 1;

                }

            } else if (isSameTypePiece((int)PieceEnum.BlackKing, board[fromX, fromY])) {

                if (isSameTypePiece((int)PieceEnum.BlackPawn, board[toX, toY])) {
                    return false;
                }

            } else if (isSameTypePiece((int)PieceEnum.BlackPawn, board[fromX, fromY])) {

                if (isSameTypePiece((int)PieceEnum.BlackKing, board[toX, toY])) {
                    return true;
                }

            }

            return Math.Abs(board[fromX, fromY]) >= Math.Abs(board[toX, toY]);
        }

        private static bool isSameTypePiece(int piece1, int piece2) {
            return Math.Abs(piece1) == Math.Abs(piece2);
        }

        private static bool isAttackingAlly(int pieceFrom, int pieceTo) {
            return pieceFrom * pieceTo > 0;
        }

    }
}
