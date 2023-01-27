using Logic;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
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
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            InitializeButtons(boardSize);
            gameSettings.Players[0] = new Player("Red", false);
            gameSettings.Players[1] = new Player("Yellow", isComputer);
            _gameLogic = new GameLogic(gameSettings);
            _gameLogic.BoardChanged += GameLogic_BoardChanged;
            _gameLogic.InitGame();
            PlayGame();
        }

        private void PlayGame()
        {
            this.Text = string.Format("Othello - {0}'s Turn", _gameLogic.m_CurrentPlayer.Name);
            GameReport gameStatus = _gameLogic.CheckHasAnyMove();
            if (_gameLogic.m_CurrentPlayer.IsComputer)
            {
                Update();
                Thread.Sleep(2000);
                _gameLogic.MakeMove(new Coordinate(0, 0));
                PlayGame();
            }
            if (gameStatus.GameStatus == eGameStatuses.GameOver)
            {
                DialogResult dialogResult = MessageBox.Show(string.Format("{0} Won!! ({2}/{3})({4}/{5}){1}Would you like another round?",
                    gameStatus.Winner.Name, Environment.NewLine, gameStatus.WinnerPoints, gameStatus.LoserPoints, GameReport.PlayerOneWinGames, GameReport.PlayerTwoWinGames), "Othello", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.No)
                {
                    this.Close();
                    return;
                }

                _gameLogic.InitGame();
                PlayGame();

            }
            else if (gameStatus.MoveStatus == eMoveStatuses.MoveSkipped)
            {
                MessageBox.Show(string.Format("No moves for {0}, turn skipped", _gameLogic.m_CurrentPlayer.Name));
                PlayGame();
            }
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
                    _buttons[i, j] = new BoardButton(new Coordinate(i, j));
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

            BoardButton button = (BoardButton)sender;
            _gameLogic.MakeMove(button.Coordinate);
            PlayGame();
            //button.ButtonState = eCellState.Black;
            //PlayGame();
            //Task.Factory.StartNew(() => PlayGame());
        }

    }


}
