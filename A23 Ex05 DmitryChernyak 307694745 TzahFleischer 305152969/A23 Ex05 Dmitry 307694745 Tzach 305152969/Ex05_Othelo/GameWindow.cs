using Logic;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace Ex05_Othelo
{
    public partial class GameWindow : Form
    {
        private readonly BoardButton[,] _buttons;
        private Panel container = new Panel();
        private GameLogic _gameLogic;
        

        public GameWindow(int boardSize, bool isComputer)
        {
            GameSettings gameSettings = new GameSettings(2)
            {
                MatrixSize = boardSize,

            };

            InitializeComponent();
            _buttons = new BoardButton[boardSize, boardSize];
            InitializeButtons(boardSize);
            gameSettings.Players[0] = new Player("Black", false);
            gameSettings.Players[1] = new Player("White", isComputer);
            _gameLogic = new GameLogic(gameSettings);
            _gameLogic.BoardChanged += GameLogic_BoardChanged;
            _gameLogic.InitGame();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void GameLogic_BoardChanged(int row, int col, eCellState newState)
        {
            _buttons[row, col].ButtonState = newState;
        }

        public void InitializeButtons(int boardSize)
        {
            var size = boardSize * 60;
            this.Size = new Size(size, size + 20);

            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    _buttons[i, j] = new BoardButton(new Point(i,j));
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
            BoardButton button = (BoardButton)sender;
            //button.Text = button.Coordinate.ToString();
            button.ButtonState = eCellState.Black;

        }

    }

   
}
