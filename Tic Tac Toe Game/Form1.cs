using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tic_Tac_Toe_Game.Properties;

namespace Tic_Tac_Toe_Game
{
    public partial class TicTacToe : Form
    {
        stGameStatus gameStatus;

        enPlayer playerTurn = enPlayer.Player1;

        struct stGameStatus
        {
            public enWinner winner;
            public bool gameOver;
            public short PlayCount;
        };

        enum enPlayer
        {
            Player1, Player2
        }

        enum enWinner
        {
            Player1, Player2, Draw, InProgres
        }

        public TicTacToe()
        {
            InitializeComponent();
        }

        void endGame()
        {
            lblPlayerTurn.Text = "Game over";
            switch (gameStatus.winner)
            {
                case enWinner.Player1:
                    lblWinner.Text = "Player 1";
                    break;
                    case enWinner.Player2:
                    lblWinner.Text = "Player 2";
                    break;
                default:
                    lblWinner.Text = "Draw";
                    break;
            }
            MessageBox.Show("GameOver", "GameOver", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        public bool checkValues(Button btn1, Button btn2, Button btn3)
        {
            if (btn1.Tag.ToString() != "?" && btn1.Tag.ToString() == btn2.Tag.ToString() && btn1.Tag.ToString() == btn3.Tag.ToString())
            {
                btn1.BackColor = Color.Green;
                btn2.BackColor = Color.Green;
                btn3.BackColor = Color.Green;

                if (btn1.Tag.ToString() == "X")
                {
                    gameStatus.winner = enWinner.Player1;
                    gameStatus.PlayCount = 0;
                    gameStatus.gameOver = true;
                    endGame();
                    return true;
                }
                else
                {
                    gameStatus.winner = enWinner.Player2;
                    gameStatus.PlayCount = 0;
                    gameStatus.gameOver = true;
                    endGame();
                    return true;
                }

            }
            gameStatus.gameOver = false;
            return false;
        }

        public void checkWinner()
        {
            if (checkValues(btn1, btn2, btn3))
                return;

            if (checkValues(btn4, btn5, btn6))
                return;

            if (checkValues(btn7, btn8, btn9))
                return;

            if (checkValues(btn1, btn4, btn7))
                return;

            if (checkValues(btn2, btn5, btn8))
                return;

            if (checkValues(btn3, btn6, btn9))
                return;

            if (checkValues(btn1, btn5, btn9))
                return;

            if (checkValues(btn3, btn5, btn7))
                return;
        }

        public void changeImage(Button btn)
        {
            if (gameStatus.gameOver) return;
            if (btn.Tag.ToString() == "?")
            {
                switch (playerTurn)
                {
                    case enPlayer.Player1:
                        btn.Image = Resources.X;
                        btn.Tag = "X";
                        lblPlayerTurn.Text = "Player 2";
                        playerTurn = enPlayer.Player2;
                        checkWinner();
                        break;
                    case enPlayer.Player2:
                        btn.Image = Resources.O;
                        btn.Tag = "O";
                        lblPlayerTurn.Text = "Player1";
                        playerTurn = enPlayer.Player1;
                        checkWinner();
                        break;
                }
            }

            else
            {
                MessageBox.Show("Wrong Choice", "Worng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if(gameStatus.PlayCount == 9)
            {
                gameStatus.gameOver = true;
                gameStatus.winner = enWinner.Draw;
                endGame();
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            changeImage((Button)sender);
        }

        private void restartButtons(Button btn)
        {
            btn.Image = Resources.question_mark_96;
            btn.Tag = "?";
            btn.BackColor = Color.Transparent;
        }

        private void restartGame()
        {
            restartButtons(btn1);
            restartButtons(btn2);
            restartButtons(btn3);
            restartButtons(btn4);
            restartButtons(btn5);
            restartButtons(btn6);
            restartButtons(btn7);
            restartButtons(btn8);
            restartButtons(btn9);

            playerTurn = enPlayer.Player1;
            lblPlayerTurn.Text = "Player 1";
            lblWinner.Text = "In Progress";
            gameStatus.winner = enWinner.InProgres;
            gameStatus.gameOver = false;
            gameStatus.PlayCount = 0;
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            restartGame();
        }

        private void TicTacToe_Paint(object sender, PaintEventArgs e)
        {
            Color white = Color.FromArgb(255, 255, 255, 255);
            Pen Pen = new Pen(white);
            Pen.Width = 10;
            Pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            Pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            e.Graphics.DrawLine(Pen, 400, 300, 1050, 300);
            e.Graphics.DrawLine(Pen, 400, 460, 1050, 460);
            e.Graphics.DrawLine(Pen, 610, 140, 610, 620);
            e.Graphics.DrawLine(Pen, 848, 148, 840, 620);
        }
    }
}

