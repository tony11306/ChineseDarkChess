using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChineseDarkChess {
    public partial class Form1 : Form {

        private const int BUTTON_START_POSITION_X = 20;
        private const int BUTTON_START_POSITION_Y = 23;
        private const int BUTTON_PADDING_X = 82;
        private const int BUTTON_PADDING_Y = 89;

        private const int RED_PIECE_TAKEN_START_POSTION_X = 90;
        private const int RED_PIECE_TAKEN_START_POSTION_Y = 500;
        private const int BLACK_PIECE_TAKEN_START_POSTION_X = 90;
        private const int BLACK_PIECE_TAKEN_START_POSTION_Y = 550;
        private const int PIECE_TAKEN_PADDING = 30;

        private Button[,] pieceButtons = new Button[Rule.BOARD_WIDTH, Rule.BOARD_HEIGHT];
        private Button selectedButton = null;
        private Button attackButton = null;
        private DarkChessModel darkChessModel;
        private List<PictureBox> redPiecesTakenPictures = new List<PictureBox>();
        private List<PictureBox> blackPiecesTakenPictures = new List<PictureBox>();

        private bool isPlayer1Turn = true;
        private bool isPlayer1Black = false;
        private bool isGameStart = false;

        public Form1() {
            InitializeComponent();
            initButtons();
        }

        private void initButtons() {

            darkChessModel = new DarkChessModel();
            isGameStart = false;
            isPlayer1Turn = true;
            selectedButton = null;
            attackButton = null;

            foreach (PictureBox pictureBox in redPiecesTakenPictures) {
                Controls.Remove(pictureBox);
                pictureBox.Dispose();
            }

            foreach (PictureBox pictureBox in blackPiecesTakenPictures) {
                Controls.Remove(pictureBox);
                pictureBox.Dispose();
            }

            redPiecesTakenPictures = new List<PictureBox>();
            blackPiecesTakenPictures = new List<PictureBox>();

            for (int i = 0; i < Rule.BOARD_WIDTH; ++i) {
                for (int j = 0; j < Rule.BOARD_HEIGHT; ++j) {

                    if (pieceButtons[i, j] != null) {
                        Controls.Remove(pieceButtons[i, j]);
                        pieceButtons[i, j].Dispose();
                    }

                    pieceButtons[i, j] = new Button();
                    Controls.Add(pieceButtons[i, j]);
                    pieceButtons[i, j].Size = new Size(65, 65);
                    pieceButtons[i, j].BackgroundImage = Properties.Resources.unflip;
                    pieceButtons[i, j].FlatStyle = FlatStyle.Flat;
                    pieceButtons[i, j].TabStop = false;
                    pieceButtons[i, j].Tag = new Pair<int, int>(i, j);
                    pieceButtons[i, j].Click += buttonClick;
                    pieceButtons[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                    pieceButtons[i, j].Location = new Point(i * BUTTON_PADDING_X + BUTTON_START_POSITION_X, j * BUTTON_PADDING_Y + BUTTON_START_POSITION_Y);
                    pieceButtons[i, j].BringToFront();
                    pieceButtons[i, j].Parent = background;
                    pieceButtons[i, j].FlatAppearance.BorderSize = 0;

                }
            }
        }

        private bool isPlayerMoveInCorrectTurn(Pair<int, int> clickedButtonPair) {
            int[,] board = darkChessModel.getBoard();
            int x = clickedButtonPair.First;
            int y = clickedButtonPair.Second;


            if (board[x, y] == (int)PieceEnum.Unflip) {
                return true;
            }

            if (!(selectedButton is null)) {
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

        private void buttonClick(object sender, EventArgs e) {
            Button clickedButton = (Button)sender;
            Pair<int, int> clickedButtonPair = (Pair<int, int>)clickedButton.Tag;
            bool hasMoved = false;

            if (!isPlayerMoveInCorrectTurn(clickedButtonPair)) {
                Console.WriteLine("test");
                return;
            }

            if (!(selectedButton is null)) {
                attackButton = clickedButton;
                Pair<int, int> fromPos = (Pair<int, int>)selectedButton.Tag;
                Pair<int, int> toPos = (Pair<int, int>)attackButton.Tag;
                MoveData moveData = new MoveData(fromPos.First, fromPos.Second, toPos.First, toPos.Second);
                hasMoved = darkChessModel.sumbitMove(moveData);
                updateBoard(fromPos.First, fromPos.Second);
                updateBoard(toPos.First, toPos.Second);
                selectedButton.BackColor = Color.Transparent;
                selectedButton = null;
                attackButton = null;
            } else if (darkChessModel.getBoard()[clickedButtonPair.First, clickedButtonPair.Second] == (int)PieceEnum.Unflip) {
                Pair<int, int> p = (Pair<int, int>)clickedButton.Tag;
                hasMoved = darkChessModel.flip(p.First, p.Second);
                updateBoard(p.First, p.Second);
            } else {
                selectedButton = clickedButton;
                selectedButton.BackColor = Color.Red;
            }

            if (!isGameStart) {
                isGameStart = true;
                isPlayer1Black = darkChessModel.getBoard()[clickedButtonPair.First, clickedButtonPair.Second] > 0;
                player1Picture.BackgroundImage = isPlayer1Black ? getPieceImage((int)PieceEnum.BlackKing) : getPieceImage((int)PieceEnum.RedKing);
                player2Picture.BackgroundImage = !isPlayer1Black ? getPieceImage((int)PieceEnum.BlackKing) : getPieceImage((int)PieceEnum.RedKing);
            }

            
            if (hasMoved) {
                isPlayer1Turn = !isPlayer1Turn;
                if (isPlayer1Turn) {
                    player1ColorLabel.BackColor = Color.RosyBrown;
                    player2ColorLabel.BackColor = Color.Transparent;
                } else {
                    player1ColorLabel.BackColor = Color.Transparent;
                    player2ColorLabel.BackColor = Color.RosyBrown;
                }
                updatePiecesTaken();
            }

            if (darkChessModel.isBlackWin()) {
                victoryLabel.Text = "黑方獲勝";
            } else if (darkChessModel.isRedWin()) {
                victoryLabel.Text = "紅方獲勝";
            }

        }

        private Bitmap getPieceImage(int pieceID) {
            switch (pieceID) {
                case (int)PieceEnum.BlackCannon:
                    return Properties.Resources.blackCannon;
                case (int)PieceEnum.RedCannon:
                    return Properties.Resources.redCannon;
                case (int)PieceEnum.BlackKing:
                    return Properties.Resources.blackKing;
                case (int)PieceEnum.RedKing:
                    return Properties.Resources.redKing;
                case (int)PieceEnum.BlackGuard:
                    return Properties.Resources.blackGuard;
                case (int)PieceEnum.RedGuard:
                    return Properties.Resources.redGuard;
                case (int)PieceEnum.BlackElephant:
                    return Properties.Resources.blackElephant;
                case (int)PieceEnum.RedElephant:
                    return Properties.Resources.redElephant;
                case (int)PieceEnum.BlackRook:
                    return Properties.Resources.blackRook;
                case (int)PieceEnum.RedRook:
                    return Properties.Resources.redRook;
                case (int)PieceEnum.BlackKnight:
                    return Properties.Resources.blackKnight;
                case (int)PieceEnum.RedKnight:
                    return Properties.Resources.redKnight;
                case (int)PieceEnum.BlackPawn:
                    return Properties.Resources.blackPawn;
                case (int)PieceEnum.RedPawn:
                    return Properties.Resources.redPawn;
                case (int)PieceEnum.Unflip:
                    return Properties.Resources.unflip;
                case (int)PieceEnum.Empty:
                    return null;
            }

            return null;
        }

        private void updatePiecesTaken() {
            while (blackPiecesTakenPictures.Count < darkChessModel.blackPiecesTaken.Count) {
                Bitmap piecePicture = getPieceImage(darkChessModel.blackPiecesTaken[blackPiecesTakenPictures.Count]);
                PictureBox newPicture = new PictureBox();
                newPicture.BackgroundImage = piecePicture;
                newPicture.Size = new Size(30, 30);
                newPicture.BackgroundImageLayout = ImageLayout.Stretch;
                newPicture.Location = new Point(BLACK_PIECE_TAKEN_START_POSTION_X + blackPiecesTakenPictures.Count * PIECE_TAKEN_PADDING, BLACK_PIECE_TAKEN_START_POSTION_Y);
                Controls.Add(newPicture);
                blackPiecesTakenPictures.Add(newPicture);
                newPicture.BringToFront();
            }

            while (redPiecesTakenPictures.Count < darkChessModel.redPiecesTaken.Count) {
                Bitmap piecePicture = getPieceImage(darkChessModel.redPiecesTaken[redPiecesTakenPictures.Count]);
                PictureBox newPicture = new PictureBox();
                newPicture.BackgroundImage = piecePicture;
                newPicture.Size = new Size(30, 30);
                newPicture.BackgroundImageLayout = ImageLayout.Stretch;
                newPicture.Location = new Point(RED_PIECE_TAKEN_START_POSTION_X + redPiecesTakenPictures.Count * PIECE_TAKEN_PADDING, RED_PIECE_TAKEN_START_POSTION_Y);
                Controls.Add(newPicture);
                redPiecesTakenPictures.Add(newPicture);
                newPicture.BringToFront();
            }
        }

        private void updateBoard(int x, int y) {
            if (x < 0 || x >= Rule.BOARD_WIDTH || y < 0 || y >= Rule.BOARD_HEIGHT) {
                throw new Exception("x or y value is not valid.");
            }

            pieceButtons[x, y].BackgroundImage = getPieceImage(darkChessModel.getBoard()[x, y]);

        }

        private void resetButton_Click(object sender, EventArgs e) {
            initButtons();
        }
    }

    public class Pair<T, U> {
        public Pair() {
        }

        public Pair(T first, U second) {
            this.First = first;
            this.Second = second;
        }

        public T First {
            get; set;
        }
        public U Second {
            get; set;
        }

    }

}
