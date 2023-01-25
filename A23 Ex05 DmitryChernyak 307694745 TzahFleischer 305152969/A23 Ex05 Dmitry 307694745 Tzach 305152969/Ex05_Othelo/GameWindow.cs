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
            PlayGame();
        }

        private void PlayGame()
        {
            _gameLogic.CheckHasAnyMove();

        }

        private void GameLogic_BoardChanged(int row, int col, eCellState newState)
        {
            //this.Text = string.Format("Othello {0}'s Turn",_gameLogic.m_CurrentPlayer.Name);
            _buttons[row, col].ButtonState = newState;
        }

        public void InitializeButtons(int boardSize)
        {
            var size = boardSize * 60;
            this.Size = new Size(size, size);

            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    _buttons[i, j] = new BoardButton(new Coordinate(i,j));
                    _buttons[i, j].Size = new Size(50, 50);
                    _buttons[i, j].Location = new Point(j * 50, i * 50);
                    _buttons[i, j].Click += new EventHandler(button_Click);
                    container.Controls.Add(_buttons[i, j]);
                }
            }
            //this.Size = new Size(_buttons[boardSize - 1, boardSize - 1].Location.X, _buttons[boardSize - 1, boardSize - 1].Location.Y);
           
            container.Dock = DockStyle.Fill;
            this.Controls.Add(container);
            //this.Size = new Size(container.Size.Width,container.Size.Height);
        }

        private void button_Click(object sender, EventArgs e)
        {
            
            BoardButton button = (BoardButton)sender;
            _gameLogic.MakeMove(button.Coordinate);
            //button.ButtonState = eCellState.Black;
            _gameLogic.CheckHasAnyMove();

        }

    }

   
}
