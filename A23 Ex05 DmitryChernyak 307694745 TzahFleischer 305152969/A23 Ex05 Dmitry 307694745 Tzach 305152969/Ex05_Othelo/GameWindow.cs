using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ex05_Othelo
{
    public partial class GameWindow : Form
    {
        private readonly Button[,] _buttons;

        public GameWindow(int boardSize)
        {
            InitializeComponent();
            _buttons = new Button[boardSize, boardSize];
            InitializeButtons(boardSize);
        }

        public void InitializeButtons(int boardSize)
        {
            this.Size = new Size(boardSize * 60 + 80, boardSize * 60 + 80);
            //this.Padding = new Padding(100);

            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    _buttons[i, j] = new Button();
                    
                    _buttons[i, j].Size = new Size(50, 50);
                    _buttons[i, j].Location = new Point(j * 60, i * 60);
                    _buttons[i, j].Left = j + 15;
                    _buttons[i, j].Click += new EventHandler(button_Click);
                    _buttons[i, j].Anchor = AnchorStyles.None;
                    this.Controls.Add(_buttons[i, j]);
                }
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            // Your code here
            Button button = (Button)sender;
            button.Text = "Clicked";
        }
    }
}
