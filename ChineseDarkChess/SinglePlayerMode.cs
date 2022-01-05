using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ChineseDarkChess {
    class SinglePlayerMode : PlayModeInterface {

        private DarkChessModel darkChessModel;
        private Form1 view;
        private bool isPlayer1Turn = true;
        private bool isPlayer1Black = false;
        private bool isGameStart = false;
        public SinglePlayerMode(Form1 view) {
            darkChessModel = new DarkChessModel();
            view.getResetButton().Click += onResetButtonClick;
            this.view = view;
        }

        private bool isPlayerMoveInCorrectTurn(Pair<int, int> clickedButtonPair) {
            int[,] board = darkChessModel.getBoard();
            int x = clickedButtonPair.First;
            int y = clickedButtonPair.Second;


            if (board[x, y] == (int)PieceEnum.Unflip) {
                return true;
            }

            if (!(view.getSelectedButton() is null)) {
                return true;
            }

            if (isPlayer1Turn) {
                if (isPlayer1Black) {
                    return board[x, y] > 0;
                } else {
                    return board[x, y] < 0;
                }
            } else {
                if (isPlayer1Black) {
                    return board[x, y] < 0;
                } else {
                    return board[x, y] > 0;
                }
            }
        }

        private void updateBoard(int x, int y) {
            if (x < 0 || x >= Rule.BOARD_WIDTH || y < 0 || y >= Rule.BOARD_HEIGHT) {
                throw new Exception("x or y value is not valid.");
            }

            view.getPieceButtons()[x, y].BackgroundImage = Form1.getPieceImage(darkChessModel.getBoard()[x, y]);
        }
        private void onPieceButtonClick(object sender, EventArgs e) {
            Button clickedButton = (Button)sender;
            Pair<int, int> clickedButtonPair = (Pair<int, int>)clickedButton.Tag;
            bool hasMoved = false;

            if (!isPlayerMoveInCorrectTurn(clickedButtonPair)) {
                return;
            }

            if (!(view.getSelectedButton() is null)) {
                view.setAttackButton(clickedButton);
                Pair<int, int> fromPos = (Pair<int, int>)view.getSelectedButton().Tag;
                Pair<int, int> toPos = (Pair<int, int>)view.getAttackButton().Tag;
                MoveData moveData = new MoveData(fromPos.First, fromPos.Second, toPos.First, toPos.Second);
                hasMoved = darkChessModel.sumbitMove(moveData);
                updateBoard(fromPos.First, fromPos.Second);
                updateBoard(toPos.First, toPos.Second);
                view.getSelectedButton().BackColor = Color.Transparent;
                view.setSelectedButton(null);
                view.setAttackButton(null);
            } else if (darkChessModel.getBoard()[clickedButtonPair.First, clickedButtonPair.Second] == (int)PieceEnum.Unflip) {
                Pair<int, int> p = (Pair<int, int>)clickedButton.Tag;
                hasMoved = darkChessModel.flip(p.First, p.Second);
                updateBoard(p.First, p.Second);
            } else {
                view.setSelectedButton(clickedButton);
                view.getSelectedButton().BackColor = Color.Red;
            }

            if (!isGameStart) {
                isGameStart = true;
                isPlayer1Black = darkChessModel.getBoard()[clickedButtonPair.First, clickedButtonPair.Second] > 0;
                view.getPlayer1Picture().BackgroundImage = isPlayer1Black ? Form1.getPieceImage((int)PieceEnum.BlackKing) : Form1.getPieceImage((int)PieceEnum.RedKing);
                view.getPlayer2Picture().BackgroundImage = !isPlayer1Black ? Form1.getPieceImage((int)PieceEnum.BlackKing) : Form1.getPieceImage((int)PieceEnum.RedKing);
            }


            if (hasMoved) {
                isPlayer1Turn = !isPlayer1Turn;
                if (isPlayer1Turn) {
                    view.getPlayer1ColorLabel().BackColor = Color.RosyBrown;
                    view.getPlayer2ColorLabel().BackColor = Color.Transparent;
                } else {
                    view.getPlayer1ColorLabel().BackColor = Color.Transparent;
                    view.getPlayer2ColorLabel().BackColor = Color.RosyBrown;
                }
                view.updatePiecesTaken(darkChessModel.redPiecesTaken, darkChessModel.blackPiecesTaken);
            }

            if (darkChessModel.isBlackWin()) {
                view.getVictoryLabel().Text = "黑方獲勝";
            } else if (darkChessModel.isRedWin()) {
                view.getVictoryLabel().Text = "紅方獲勝";
            }
        }

        public void onResetButtonClick(object sender, EventArgs e) {
            init();
        }
        public void init() {
            darkChessModel = new DarkChessModel();
            isGameStart = false;
            isPlayer1Turn = true;
            view.setSelectedButton(null);
            view.setAttackButton(null);
            view.getPlayer1Picture().BackgroundImage = null;
            view.getPlayer2Picture().BackgroundImage = null;
            view.getVictoryLabel().Text = "";
            Button[,] pieceButtons = view.getPieceButtons();

            foreach (PictureBox pictureBox in view.getRedPiecesTakenPictures()) {
                view.Controls.Remove(pictureBox);
                pictureBox.Dispose();
            }

            foreach (PictureBox pictureBox in view.getBlackPiecesTakenPictures()) {
                view.Controls.Remove(pictureBox);
                pictureBox.Dispose();
            }
            Console.WriteLine("test");
            view.setRedPiecesTakenPictures(new List<PictureBox>());
            view.setBlackPiecesTakenPictures(new List<PictureBox>());

            for (int i = 0; i < Rule.BOARD_WIDTH; ++i) {
                for (int j = 0; j < Rule.BOARD_HEIGHT; ++j) {

                    if (pieceButtons[i, j] != null) {
                        view.Controls.Remove(pieceButtons[i, j]);
                        pieceButtons[i, j].Dispose();
                    }

                    pieceButtons[i, j] = new Button();
                    view.Controls.Add(pieceButtons[i, j]);
                    pieceButtons[i, j].Size = new Size(65, 65);
                    pieceButtons[i, j].BackgroundImage = Properties.Resources.unflip;
                    pieceButtons[i, j].FlatStyle = FlatStyle.Flat;
                    pieceButtons[i, j].TabStop = false;
                    pieceButtons[i, j].Tag = new Pair<int, int>(i, j);
                    pieceButtons[i, j].Click += onPieceButtonClick;
                    pieceButtons[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                    pieceButtons[i, j].Location = new Point(i * Form1.BUTTON_PADDING_X + Form1.BUTTON_START_POSITION_X, j * Form1.BUTTON_PADDING_Y + Form1.BUTTON_START_POSITION_Y);
                    pieceButtons[i, j].BringToFront();
                    pieceButtons[i, j].Parent = view.getBackground();
                    pieceButtons[i, j].FlatAppearance.BorderSize = 0;

                }
            }

            view.showPlayInformation();


        }

        

    }
}
