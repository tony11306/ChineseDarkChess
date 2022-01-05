
namespace ChineseDarkChess {
    partial class Form1 {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.background = new System.Windows.Forms.PictureBox();
            this.player1ColorLabel = new System.Windows.Forms.Label();
            this.victoryLabel = new System.Windows.Forms.Label();
            this.resetButton = new System.Windows.Forms.Button();
            this.player2ColorLabel = new System.Windows.Forms.Label();
            this.player1Picture = new System.Windows.Forms.PictureBox();
            this.player2Picture = new System.Windows.Forms.PictureBox();
            this.localPlayButton = new System.Windows.Forms.Button();
            this.multiplayerButton = new System.Windows.Forms.Button();
            this.leaveGameButton = new System.Windows.Forms.Button();
            this.ipTextBox = new System.Windows.Forms.TextBox();
            this.connectConfirmButton = new System.Windows.Forms.Button();
            this.ipTextLabel = new System.Windows.Forms.Label();
            this.gameIcon = new System.Windows.Forms.PictureBox();
            this.gameTitle = new System.Windows.Forms.Label();
            this.surrenderButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.background)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.player1Picture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.player2Picture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gameIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // background
            // 
            this.background.BackColor = System.Drawing.Color.Transparent;
            this.background.BackgroundImage = global::ChineseDarkChess.Properties.Resources.background;
            this.background.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.background.Location = new System.Drawing.Point(133, 154);
            this.background.Name = "background";
            this.background.Size = new System.Drawing.Size(900, 459);
            this.background.TabIndex = 1;
            this.background.TabStop = false;
            // 
            // player1ColorLabel
            // 
            this.player1ColorLabel.AutoSize = true;
            this.player1ColorLabel.Font = new System.Drawing.Font("標楷體", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.player1ColorLabel.ForeColor = System.Drawing.Color.White;
            this.player1ColorLabel.Location = new System.Drawing.Point(127, 33);
            this.player1ColorLabel.Name = "player1ColorLabel";
            this.player1ColorLabel.Size = new System.Drawing.Size(202, 33);
            this.player1ColorLabel.TabIndex = 2;
            this.player1ColorLabel.Text = "玩家1顏色: ";
            // 
            // victoryLabel
            // 
            this.victoryLabel.AutoSize = true;
            this.victoryLabel.Font = new System.Drawing.Font("新細明體", 20F);
            this.victoryLabel.ForeColor = System.Drawing.Color.White;
            this.victoryLabel.Location = new System.Drawing.Point(553, 92);
            this.victoryLabel.Name = "victoryLabel";
            this.victoryLabel.Size = new System.Drawing.Size(0, 34);
            this.victoryLabel.TabIndex = 3;
            this.victoryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // resetButton
            // 
            this.resetButton.BackColor = System.Drawing.Color.White;
            this.resetButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.resetButton.Font = new System.Drawing.Font("標楷體", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.resetButton.Location = new System.Drawing.Point(521, 12);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(146, 54);
            this.resetButton.TabIndex = 4;
            this.resetButton.Text = "再來一局";
            this.resetButton.UseVisualStyleBackColor = false;
            // 
            // player2ColorLabel
            // 
            this.player2ColorLabel.AutoSize = true;
            this.player2ColorLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.player2ColorLabel.Font = new System.Drawing.Font("標楷體", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.player2ColorLabel.ForeColor = System.Drawing.Color.White;
            this.player2ColorLabel.Location = new System.Drawing.Point(817, 33);
            this.player2ColorLabel.Name = "player2ColorLabel";
            this.player2ColorLabel.Size = new System.Drawing.Size(202, 33);
            this.player2ColorLabel.TabIndex = 5;
            this.player2ColorLabel.Text = "玩家2顏色: ";
            // 
            // player1Picture
            // 
            this.player1Picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.player1Picture.Location = new System.Drawing.Point(165, 69);
            this.player1Picture.Name = "player1Picture";
            this.player1Picture.Size = new System.Drawing.Size(71, 70);
            this.player1Picture.TabIndex = 6;
            this.player1Picture.TabStop = false;
            // 
            // player2Picture
            // 
            this.player2Picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.player2Picture.Location = new System.Drawing.Point(922, 69);
            this.player2Picture.Name = "player2Picture";
            this.player2Picture.Size = new System.Drawing.Size(71, 70);
            this.player2Picture.TabIndex = 7;
            this.player2Picture.TabStop = false;
            // 
            // localPlayButton
            // 
            this.localPlayButton.Font = new System.Drawing.Font("微軟正黑體", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.localPlayButton.Location = new System.Drawing.Point(489, 275);
            this.localPlayButton.Name = "localPlayButton";
            this.localPlayButton.Size = new System.Drawing.Size(221, 75);
            this.localPlayButton.TabIndex = 8;
            this.localPlayButton.Text = "本地遊玩";
            this.localPlayButton.UseVisualStyleBackColor = true;
            this.localPlayButton.Click += new System.EventHandler(this.localPlayButton_Click);
            // 
            // multiplayerButton
            // 
            this.multiplayerButton.Font = new System.Drawing.Font("微軟正黑體", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.multiplayerButton.Location = new System.Drawing.Point(489, 513);
            this.multiplayerButton.Name = "multiplayerButton";
            this.multiplayerButton.Size = new System.Drawing.Size(221, 75);
            this.multiplayerButton.TabIndex = 9;
            this.multiplayerButton.Text = "連線模式";
            this.multiplayerButton.UseVisualStyleBackColor = true;
            this.multiplayerButton.Click += new System.EventHandler(this.multiplayerButton_Click);
            // 
            // leaveGameButton
            // 
            this.leaveGameButton.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.leaveGameButton.Location = new System.Drawing.Point(12, 12);
            this.leaveGameButton.Name = "leaveGameButton";
            this.leaveGameButton.Size = new System.Drawing.Size(106, 54);
            this.leaveGameButton.TabIndex = 10;
            this.leaveGameButton.Text = "返回目錄";
            this.leaveGameButton.UseVisualStyleBackColor = true;
            // 
            // ipTextBox
            // 
            this.ipTextBox.Font = new System.Drawing.Font("新細明體", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ipTextBox.Location = new System.Drawing.Point(382, 315);
            this.ipTextBox.Name = "ipTextBox";
            this.ipTextBox.Size = new System.Drawing.Size(429, 35);
            this.ipTextBox.TabIndex = 11;
            this.ipTextBox.Visible = false;
            // 
            // connectConfirmButton
            // 
            this.connectConfirmButton.Location = new System.Drawing.Point(521, 372);
            this.connectConfirmButton.Name = "connectConfirmButton";
            this.connectConfirmButton.Size = new System.Drawing.Size(148, 59);
            this.connectConfirmButton.TabIndex = 12;
            this.connectConfirmButton.Text = "連線至伺服器";
            this.connectConfirmButton.UseVisualStyleBackColor = true;
            this.connectConfirmButton.Visible = false;
            this.connectConfirmButton.Click += new System.EventHandler(this.connectConfirmButton_Click);
            // 
            // ipTextLabel
            // 
            this.ipTextLabel.AutoSize = true;
            this.ipTextLabel.BackColor = System.Drawing.Color.Transparent;
            this.ipTextLabel.Font = new System.Drawing.Font("新細明體", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ipTextLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ipTextLabel.Location = new System.Drawing.Point(506, 281);
            this.ipTextLabel.Name = "ipTextLabel";
            this.ipTextLabel.Size = new System.Drawing.Size(163, 24);
            this.ipTextLabel.TabIndex = 13;
            this.ipTextLabel.Text = "伺服器 IP 位址";
            this.ipTextLabel.Visible = false;
            // 
            // gameIcon
            // 
            this.gameIcon.BackColor = System.Drawing.Color.Transparent;
            this.gameIcon.BackgroundImage = global::ChineseDarkChess.Properties.Resources.redKing;
            this.gameIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gameIcon.Location = new System.Drawing.Point(416, 59);
            this.gameIcon.Name = "gameIcon";
            this.gameIcon.Size = new System.Drawing.Size(131, 123);
            this.gameIcon.TabIndex = 14;
            this.gameIcon.TabStop = false;
            // 
            // gameTitle
            // 
            this.gameTitle.AutoSize = true;
            this.gameTitle.BackColor = System.Drawing.Color.Transparent;
            this.gameTitle.Font = new System.Drawing.Font("新細明體", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.gameTitle.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.gameTitle.Location = new System.Drawing.Point(578, 103);
            this.gameTitle.Name = "gameTitle";
            this.gameTitle.Size = new System.Drawing.Size(118, 48);
            this.gameTitle.TabIndex = 15;
            this.gameTitle.Text = "暗棋";
            // 
            // surrenderButton
            // 
            this.surrenderButton.Font = new System.Drawing.Font("新細明體", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.surrenderButton.Location = new System.Drawing.Point(521, 14);
            this.surrenderButton.Name = "surrenderButton";
            this.surrenderButton.Size = new System.Drawing.Size(146, 52);
            this.surrenderButton.TabIndex = 16;
            this.surrenderButton.Text = "投降";
            this.surrenderButton.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1182, 753);
            this.Controls.Add(this.surrenderButton);
            this.Controls.Add(this.ipTextLabel);
            this.Controls.Add(this.connectConfirmButton);
            this.Controls.Add(this.ipTextBox);
            this.Controls.Add(this.leaveGameButton);
            this.Controls.Add(this.multiplayerButton);
            this.Controls.Add(this.localPlayButton);
            this.Controls.Add(this.player2Picture);
            this.Controls.Add(this.player1Picture);
            this.Controls.Add(this.player2ColorLabel);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.victoryLabel);
            this.Controls.Add(this.player1ColorLabel);
            this.Controls.Add(this.background);
            this.Controls.Add(this.gameIcon);
            this.Controls.Add(this.gameTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "暗棋";
            ((System.ComponentModel.ISupportInitialize)(this.background)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.player1Picture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.player2Picture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gameIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox background;
        private System.Windows.Forms.Label player1ColorLabel;
        private System.Windows.Forms.Label victoryLabel;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Label player2ColorLabel;
        private System.Windows.Forms.PictureBox player1Picture;
        private System.Windows.Forms.PictureBox player2Picture;
        private System.Windows.Forms.Button localPlayButton;
        private System.Windows.Forms.Button multiplayerButton;
        private System.Windows.Forms.Button leaveGameButton;
        private System.Windows.Forms.TextBox ipTextBox;
        private System.Windows.Forms.Button connectConfirmButton;
        private System.Windows.Forms.Label ipTextLabel;
        private System.Windows.Forms.PictureBox gameIcon;
        private System.Windows.Forms.Label gameTitle;
        private System.Windows.Forms.Button surrenderButton;
    }
}

