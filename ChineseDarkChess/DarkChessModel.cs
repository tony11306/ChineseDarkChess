using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseDarkChess {
    class DarkChessModel {

        private int[,] board = new int[Rule.BOARD_WIDTH, Rule.BOARD_HEIGHT];
        private List<int> unFlipPieces;
        private Random random = new Random();
        public List<int> redPiecesTaken { 
            get; set; 
        }
        public List<int> blackPiecesTaken {
            get; set;
        }
        public DarkChessModel() {
            initBoard();
        }

        public void initBoard() {
            redPiecesTaken = new List<int>();
            blackPiecesTaken = new List<int>();
            unFlipPieces = new List<int> {
                2, 2, 2, 2, 2, -2, -2, -2, -2, -2,
                3, 3, -3, -3,
                4, 4, -4, -4,
                5, 5, -5, -5,
                6, 6, -6, -6,
                7, 7, -7, -7,
                8, -8
            };

            // shuffle pieces, copy it from stackoverflow :D
            unFlipPieces = unFlipPieces.OrderBy(a => random.Next()).ToList();

            for (int i = 0; i < Rule.BOARD_WIDTH; ++i) {
                for (int j = 0; j < Rule.BOARD_HEIGHT; ++j) {
                    board[i, j] = (int)PieceEnum.Unflip;
                }
            }

        }

        public int[,] getBoard() {
            return board;
        }

        public bool flip(int x, int y) {
            if (x < 0 || x >= Rule.BOARD_WIDTH || y < 0 || y >= Rule.BOARD_HEIGHT) {
                throw new Exception("x or y value is not valid.");
            }

            if (unFlipPieces.Count == 0) {
                return false;
            }

            if (board[x, y] != (int)PieceEnum.Unflip) {
                return false;
            }

            board[x, y] = unFlipPieces[0];
            unFlipPieces.RemoveAt(0);
            return true;

        }
        public bool sumbitMove(MoveData moveData) {

            if (Rule.isValidMove(board, moveData)) {

                if (board[moveData.toX, moveData.toY] != (int)PieceEnum.Empty) {
                    if (board[moveData.fromX, moveData.fromY] > 0) { // moving a black piece
                        redPiecesTaken.Add(board[moveData.toX, moveData.toY]);
                    } else { // moving a red piece
                        blackPiecesTaken.Add(board[moveData.toX, moveData.toY]);
                    }
                }

                board[moveData.toX, moveData.toY] = board[moveData.fromX, moveData.fromY];
                board[moveData.fromX, moveData.fromY] = (int)PieceEnum.Empty;
                return true;
            }

            return false;
        }

        public bool isBlackWin() {
            return redPiecesTaken.Count == Rule.PIECES_NUMBER_FOR_EACH_PLAYER;
        }

        public bool isRedWin() {
            return blackPiecesTaken.Count == Rule.PIECES_NUMBER_FOR_EACH_PLAYER;
        }

    }
}
