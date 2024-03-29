﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using packet;
using moveData;
using System.Drawing;

namespace ChineseDarkChess {
    class MultiPlayerMode : PlayModeInterface {
        public const int MAX_PACKET_BYTES = 2048;
        public const int PORT = 8885;

        private string serverAddress = "";
        private Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private Thread recvThread;
        private static byte[] result = new byte[MAX_PACKET_BYTES];
        private Form1 view;
        private delegate void DelegateExecuteCommandBaseOnPacket(Packet pkt);
        private delegate void DelegateHandleUnexpectedDisconnection();
        private int[,] board = null;
        private bool isPlayerTurn = false;
        private bool isPlayerBlack;
        private bool isPlayer1;
        private bool isGameStart = false;

        public MultiPlayerMode(Form1 view, string serverAddress) {
            this.view = view;
            this.serverAddress = serverAddress;
        }

        ~MultiPlayerMode() {
            try {
                if (!(recvThread is null)) {
                    recvThread.Abort();
                }
                view.getLeaveGameButton().Click -= onLeaveGameButtonClick;
            } catch {
                // error handling
            }
        }

        private void onSurrenderButtonClick(object sender, EventArgs e) {
            Packet packet = new Packet(Command.SURRENDER, isPlayerBlack);
            clientSocket.Send(Packet.Serialize(packet).Data);

        }
        private bool isPlayerMoveInCorrectTurn(Pair<int, int> clickedButtonPair) {
            int[,] board = this.board;
            int x = clickedButtonPair.First;
            int y = clickedButtonPair.Second;

            if (!isPlayerTurn) {
                return false;
            }

            if (board[x, y] == (int)PieceEnum.Unflip) {
                return true;
            }

            if (!(view.getSelectedButton() is null)) {
                return true;
            }

            if (isPlayerBlack) {
                return board[x, y] > 0;
            }

            return board[x, y] < 0;
        }

        private void onLeaveGameButtonClick(object sender, EventArgs e) {
            ((Button)sender).Click -= onLeaveGameButtonClick;
            if (clientSocket.Connected) {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
                Console.WriteLine("與伺服器斷開連線");
            }
            view.hidePlayInformation();
            view.showModeMenu();
        }

        private void handleUnexpectedDisconnection() {
            if (view.InvokeRequired) {
                DelegateHandleUnexpectedDisconnection delegateHandleUnexpectedDisconnection = new DelegateHandleUnexpectedDisconnection(handleUnexpectedDisconnection);
                view.Invoke(delegateHandleUnexpectedDisconnection);
                return;
            }
            view.getSurrenderButton().Click -= onSurrenderButtonClick;
            if (clientSocket.Connected) {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
                Console.WriteLine("與伺服器斷開連線");
            }
            view.showModeMenu();
            if (!(view.getPieceButtons() is null)) {
                foreach (Button button in view.getPieceButtons()) {
                    if (button is null) {
                        continue;
                    }
                    button.Dispose();
                    view.Controls.Remove(button);
                }
            }
            view.hidePlayInformation();
            view.getSurrenderButton().Click -= onSurrenderButtonClick;

        }

        private void onPieceButtonClick(object sender, EventArgs e) {
            Button clickedButton = (Button)sender;
            Pair<int, int> clickedButtonPair = (Pair<int, int>)clickedButton.Tag;
            if (!isGameStart) {
                return;
            }

            if (!isPlayerMoveInCorrectTurn(clickedButtonPair)) {
                return;
            }

            if (!(view.getSelectedButton() is null)) {
                view.setAttackButton(clickedButton);
                Pair<int, int> fromPos = (Pair<int, int>)view.getSelectedButton().Tag;
                Pair<int, int> toPos = (Pair<int, int>)view.getAttackButton().Tag;
                moveData.MoveData moveData = new moveData.MoveData(fromPos.First, fromPos.Second, toPos.First, toPos.Second);
                //Upload to Server
                Packet packet = new Packet(Command.MOVE, moveData);
                clientSocket.Send(Packet.Serialize(packet).Data);

                view.getSelectedButton().BackColor = Color.Transparent;
                view.setSelectedButton(null);
                view.setAttackButton(null);
            } else if (this.board[clickedButtonPair.First, clickedButtonPair.Second] == (int)PieceEnum.Unflip) {
                Pair<int, int> p = (Pair<int, int>)clickedButton.Tag;
                //upload to server
                moveData.MoveData moveData = new moveData.MoveData(p.First, p.Second, 0, 0);
                Packet packet = new Packet(Command.FLIP, moveData);
                clientSocket.Send(Packet.Serialize(packet).Data);

            } else {
                view.setSelectedButton(clickedButton);
                view.getSelectedButton().BackColor = Color.Red;
            }

            if (!isGameStart) {
                isGameStart = true;
                isPlayerBlack = board[clickedButtonPair.First, clickedButtonPair.Second] > 0;
                view.getPlayer1Picture().BackgroundImage = isPlayerBlack ? Form1.getPieceImage((int)PieceEnum.BlackKing) : Form1.getPieceImage((int)PieceEnum.RedKing);
                view.getPlayer2Picture().BackgroundImage = !isPlayerBlack ? Form1.getPieceImage((int)PieceEnum.BlackKing) : Form1.getPieceImage((int)PieceEnum.RedKing);
            }



        }
        public void init() {
            try {
                IPAddress ip = IPAddress.Parse(serverAddress);
                clientSocket.Connect(new IPEndPoint(ip, PORT));
                recvThread = new Thread(recv);
                recvThread.IsBackground = true;
                recvThread.Start();
                Console.WriteLine("連接伺服器成功");
                view.showPlayInformation();
                view.getResetButton().Hide();
                view.getLeaveGameButton().Click += onLeaveGameButtonClick;
                
            } catch {
                if (clientSocket.Connected) {
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                    Console.WriteLine("與伺服器斷開連線");
                } else {
                    Console.WriteLine("連接伺服器失敗");
                }
                view.showModeMenu();
                view.hidePlayInformation();
            }
        }

        private void initButtons() {
            isGameStart = false;
            view.setSelectedButton(null);
            view.setAttackButton(null);
            view.getPlayer1ColorLabel().BackColor = Color.RosyBrown;
            view.getPlayer2ColorLabel().BackColor = Color.Transparent;

            if (isPlayer1) {
                view.getPlayer1ColorLabel().Text = "你(玩家1)";
                view.getPlayer2ColorLabel().Text = "對手(玩家2)";
            } else {
                view.getPlayer1ColorLabel().Text = "對手(玩家1)";
                view.getPlayer2ColorLabel().Text = "你(玩家2)";
            }

            foreach (PictureBox pictureBox in view.getRedPiecesTakenPictures()) {
                view.Controls.Remove(pictureBox);
                pictureBox.Dispose();
            }

            foreach (PictureBox pictureBox in view.getBlackPiecesTakenPictures()) {
                view.Controls.Remove(pictureBox);
                pictureBox.Dispose();
            }

            view.setBlackPiecesTakenPictures(new List<PictureBox>());
            view.setRedPiecesTakenPictures(new List<PictureBox>());

            Button[,] pieceButtons = view.getPieceButtons();

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

        }

        private void renderPiecesTaken(Packet packet) {
            while (view.getBlackPiecesTakenPictures().Count < packet.blackPiecesTaken.Count) {
                Bitmap piecePicture = Form1.getPieceImage(packet.blackPiecesTaken[view.getBlackPiecesTakenPictures().Count]);
                PictureBox newPicture = new PictureBox();
                newPicture.BackgroundImage = piecePicture;
                newPicture.Size = new Size(30, 30);
                newPicture.BackgroundImageLayout = ImageLayout.Stretch;
                newPicture.Location = new Point(Form1.BLACK_PIECE_TAKEN_START_POSTION_X + view.getBlackPiecesTakenPictures().Count * Form1.PIECE_TAKEN_PADDING, Form1.BLACK_PIECE_TAKEN_START_POSTION_Y);
                view.Controls.Add(newPicture);
                view.getBlackPiecesTakenPictures().Add(newPicture);
                newPicture.BringToFront();
            }

            while (view.getRedPiecesTakenPictures().Count < packet.redPiecesTaken.Count) {
                Bitmap piecePicture = Form1.getPieceImage(packet.redPiecesTaken[view.getRedPiecesTakenPictures().Count]);
                PictureBox newPicture = new PictureBox();
                newPicture.BackgroundImage = piecePicture;
                newPicture.Size = new Size(30, 30);
                newPicture.BackgroundImageLayout = ImageLayout.Stretch;
                newPicture.Location = new Point(Form1.RED_PIECE_TAKEN_START_POSTION_X + view.getRedPiecesTakenPictures().Count * Form1.PIECE_TAKEN_PADDING, Form1.RED_PIECE_TAKEN_START_POSTION_Y);
                view.Controls.Add(newPicture);
                view.getRedPiecesTakenPictures().Add(newPicture);
                newPicture.BringToFront();
            }
        }

        private void renderBoard() {
            for (int i = 0; i < Rule.BOARD_WIDTH; ++i) {
                for (int j = 0; j < Rule.BOARD_HEIGHT; ++j) {
                    view.getPieceButtons()[i, j].BackgroundImage = Form1.getPieceImage(board[i, j]);
                }
            }
        }

        private void recv() {
            while (true) {
                try {
                    int recvDataSize = clientSocket.Receive(result);
                    if (recvDataSize == 0) {
                        throw new Exception(clientSocket.RemoteEndPoint.ToString() + " has disconnected.");
                    }

                    byte[] serializedData = (byte[])result.Clone();
                    Array.Resize(ref serializedData, recvDataSize);
                    object packet = Packet.Deserialize(serializedData);

                    if (packet is Packet) {
                        executeCommandBaseOnPacket((Packet)packet);
                    } else {
                        Console.WriteLine(String.Format("接收伺服端 {0} 訊息 {1}", clientSocket.RemoteEndPoint.ToString(), Encoding.ASCII.GetString(result, 0, recvDataSize)));
                    }

                } catch {
                    handleUnexpectedDisconnection();
                    break;
                }
            }
        }

        private void switchTurn() {
            isPlayerTurn = !isPlayerTurn;
            Color temp = view.getPlayer1ColorLabel().BackColor;
            view.getPlayer1ColorLabel().BackColor = view.getPlayer2ColorLabel().BackColor;
            view.getPlayer2ColorLabel().BackColor = temp;
        }

        private void executeCommandBaseOnPacket(Packet packet) {
            if (view.InvokeRequired) {
                DelegateExecuteCommandBaseOnPacket delegateExecuteCommandBaseOnPacket = new DelegateExecuteCommandBaseOnPacket(executeCommandBaseOnPacket);
                view.Invoke(delegateExecuteCommandBaseOnPacket, packet);
                return;
            }

            switch (packet.command) {
                case Command.GAME_START:
                    board = packet.board;
                    isPlayer1 = packet.playerStatusChange;
                    isPlayerTurn = isPlayer1;
                    initButtons();
                    isGameStart = true;
                    view.getSurrenderButton().Click += onSurrenderButtonClick;
                    view.getSurrenderButton().Show();
                    break;
                case Command.COLOR_ASSIGN:
                    isPlayerBlack = packet.playerStatusChange;
                    view.getPlayer1Picture().BackgroundImage = (isPlayer1 == isPlayerBlack) ? Form1.getPieceImage((int)PieceEnum.BlackKing) : Form1.getPieceImage((int)PieceEnum.RedKing);
                    view.getPlayer2Picture().BackgroundImage = (isPlayer1 != isPlayerBlack) ? Form1.getPieceImage((int)PieceEnum.BlackKing) : Form1.getPieceImage((int)PieceEnum.RedKing);
                    isGameStart = true;
                    break;
                case Command.UPDATE_BOARD:
                    board = packet.board;
                    switchTurn();
                    renderPiecesTaken(packet);
                    renderBoard();
                    view.getPieceMoveSound().Play();
                    break;
                case Command.MOVEFAIL:
                    break;
                case Command.GAME_RESULT:
                    view.getSurrenderButton().Click -= onSurrenderButtonClick;
                    if (isPlayerBlack) {
                        if (packet.playerStatusChange) {
                            view.getVictoryLabel().Text = "你贏了";
                        } else {
                            view.getVictoryLabel().Text = "你輸了";
                        }
                    } else {
                        if (packet.playerStatusChange) {
                            view.getVictoryLabel().Text = "你輸了";
                        } else {
                            view.getVictoryLabel().Text = "你贏了";
                        }
                    }
                    isPlayerTurn = false;
                    break;
                default:
                    break;
            }

        }

    }
}
