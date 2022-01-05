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
    public partial class Form1 : Form { // let Form1 be view

        public const int BUTTON_START_POSITION_X = 20;
        public const int BUTTON_START_POSITION_Y = 23;
        public const int BUTTON_PADDING_X = 82;
        public const int BUTTON_PADDING_Y = 89;

        public const int RED_PIECE_TAKEN_START_POSTION_X = 90;
        public const int RED_PIECE_TAKEN_START_POSTION_Y = 500;
        public const int BLACK_PIECE_TAKEN_START_POSTION_X = 90;
        public const int BLACK_PIECE_TAKEN_START_POSTION_Y = 550;
        public const int PIECE_TAKEN_PADDING = 30;

        private Button[,] pieceButtons = new Button[Rule.BOARD_WIDTH, Rule.BOARD_HEIGHT];
        private Button selectedButton = null;
        private Button attackButton = null;
        private List<PictureBox> redPiecesTakenPictures = new List<PictureBox>();
        private List<PictureBox> blackPiecesTakenPictures = new List<PictureBox>();

        private PlayModeInterface playMode;

        public Form1() {
            InitializeComponent();
            hidePlayInformation();
            showModeMenu();

        }

        public static Bitmap getPieceImage(int pieceID) {
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

        public void updatePiecesTaken(List<int> redPiecesTaken, List<int> blackPiecesTaken) {
            while (blackPiecesTakenPictures.Count < blackPiecesTaken.Count) {
                Bitmap piecePicture = getPieceImage(blackPiecesTaken[blackPiecesTakenPictures.Count]);
                PictureBox newPicture = new PictureBox();
                newPicture.BackgroundImage = piecePicture;
                newPicture.Size = new Size(30, 30);
                newPicture.BackgroundImageLayout = ImageLayout.Stretch;
                newPicture.Location = new Point(BLACK_PIECE_TAKEN_START_POSTION_X + blackPiecesTakenPictures.Count * PIECE_TAKEN_PADDING, BLACK_PIECE_TAKEN_START_POSTION_Y);
                Controls.Add(newPicture);
                blackPiecesTakenPictures.Add(newPicture);
                newPicture.BringToFront();
            }

            while (redPiecesTakenPictures.Count < redPiecesTaken.Count) {
                Bitmap piecePicture = getPieceImage(redPiecesTaken[redPiecesTakenPictures.Count]);
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

        public Button getResetButton() {
            return resetButton;
        }

        public void hidePlayInformation() {
            for (int i = 0; i < Rule.BOARD_WIDTH && !(pieceButtons is null); ++i) {
                for (int j = 0; j < Rule.BOARD_HEIGHT; ++j) {
                    if (pieceButtons[i, j] is null) {
                        continue;
                    }
                    pieceButtons[i, j].Hide();
                }
            }
            if (!(redPiecesTakenPictures is null)) {
                foreach (PictureBox pictureBox in redPiecesTakenPictures) {
                    pictureBox.Hide();
                }
            }

            if (!(blackPiecesTakenPictures is null)) {
                foreach (PictureBox pictureBox in blackPiecesTakenPictures) {
                    pictureBox.Hide();
                }
            
            }

            if (!(background is null)) {
                background.Hide();
            }

            if (!(player1ColorLabel is null)) {
                player1ColorLabel.Hide();
            }

            if (!(player2ColorLabel is null)) {
                player2ColorLabel.Hide();
            }

            if (!(player1Picture is null)) {
                player1Picture.Hide();
            }

            if (!(player2Picture is null)) {
                player2Picture.Hide();
            }

            if (!(resetButton is null)) {
                resetButton.Hide();
            }
        }

        public void showPlayInformation() {
            for (int i = 0; i < Rule.BOARD_WIDTH && !(pieceButtons is null); ++i) {
                for (int j = 0; j < Rule.BOARD_HEIGHT; ++j) {
                    if (pieceButtons[i, j] is null) {
                        continue;
                    }
                    pieceButtons[i, j].Show();
                }
            }
            if (!(redPiecesTakenPictures is null)) {
                foreach (PictureBox pictureBox in redPiecesTakenPictures) {
                    pictureBox.Show();
                }
            }

            if (!(blackPiecesTakenPictures is null)) {
                foreach (PictureBox pictureBox in blackPiecesTakenPictures) {
                    pictureBox.Show();
                }

            }

            if (!(background is null)) {
                background.Show();
            }

            if (!(player1ColorLabel is null)) {
                player1ColorLabel.Show();
            }

            if (!(player2ColorLabel is null)) {
                player2ColorLabel.Show();
            }

            if (!(player1Picture is null)) {
                player1Picture.Show();
            }

            if (!(player2Picture is null)) {
                player2Picture.Show();
            }

            if (!(resetButton is null)) {
                resetButton.Show();
            }
        }

        public Button getSelectedButton() {
            return selectedButton;
        }

        public void setSelectedButton(Button selectedButton) {
            this.selectedButton = selectedButton;
        }

        public Button getAttackButton() {
            return attackButton;
        }

        public void setAttackButton(Button attackButton) {
            this.attackButton = attackButton;
        }

        public Button[,] getPieceButtons() {
            return pieceButtons;
        }

        public PictureBox getBackground() {
            return background;
        }

        public PictureBox getPlayer1Picture() {
            return player1Picture;
        }

        public PictureBox getPlayer2Picture() {
            return player2Picture;
        }

        public void setPlayer1Picture(PictureBox player1Picture) {
            this.player1Picture = player1Picture;
        }

        public void setPlayer2Picture(PictureBox player2Picture) {
            this.player2Picture = player2Picture;
        }

        public Label getVictoryLabel() {
            return victoryLabel;
        }

        public Label getPlayer1ColorLabel() {
            return player1ColorLabel;
        }

        public Label getPlayer2ColorLabel() {
            return player2ColorLabel;
        }

        public List<PictureBox> getRedPiecesTakenPictures() {
            return redPiecesTakenPictures;
        }

        public List<PictureBox> getBlackPiecesTakenPictures() {
            return blackPiecesTakenPictures;
        }

        public void setRedPiecesTakenPictures(List<PictureBox> redPiecesTakenPictures) {
            this.redPiecesTakenPictures = redPiecesTakenPictures;
        }

        public void setBlackPiecesTakenPictures(List<PictureBox> blackPiecesTakenPictures) {
            this.blackPiecesTakenPictures = blackPiecesTakenPictures;
        }

        public void showModeMenu() {
            if (!(localPlayButton is null)) {
                localPlayButton.Show();
            }
            if (!(multiplayerButton is null)) {
                multiplayerButton.Show();
            }
        }

        public void hideModeMenu() {
            if (!(localPlayButton is null)) {
                localPlayButton.Hide();
            }
            if (!(multiplayerButton is null)) {
                multiplayerButton.Hide();
            }

        }

        private void localPlayButton_Click(object sender, EventArgs e) {
            hideModeMenu();
            playMode = new SinglePlayerMode(this);
            playMode.init();
        }

        private void multiplayerButton_Click(object sender, EventArgs e) {
            hideModeMenu();
            playMode = new MultiPlayerMode(this);
            playMode.init();
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
