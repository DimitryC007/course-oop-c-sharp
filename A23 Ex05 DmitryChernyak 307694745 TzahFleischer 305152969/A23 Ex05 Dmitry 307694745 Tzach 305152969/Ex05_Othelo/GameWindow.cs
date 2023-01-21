using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ex05_Othelo
{
    public partial class GameWindow : Form
    {
        private readonly Button[,] _buttons;
        private Panel container = new Panel();

        public GameWindow(int boardSize)
        {
            InitializeComponent();
            _buttons = new Button[boardSize, boardSize];
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            InitializeButtons(boardSize);
        }

        public void InitializeButtons(int boardSize)
        {
            var size = boardSize * 60;
            this.Size = new Size(size, size + 20);

            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    _buttons[i, j] = new Button();
                    _buttons[i, j].Size = new Size(50, 50);
                    _buttons[i, j].Location = new Point(j * 60, i * 60);
                    _buttons[i, j].Click += new EventHandler(button_Click);
                    container.Controls.Add(_buttons[i, j]);
                }
            }
           
            container.Dock = DockStyle.Fill;
            this.Controls.Add(container);
        }

        private void button_Click(object sender, EventArgs e)
        {
            // Your code here
            Button button = (Button)sender;
            button.Text = "Clicked";
        }
    }
}
