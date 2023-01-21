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
    public partial class GameSettingsWindow : Form
    {
        private GameWindow _gameWindow;
        private static int _boardSize = 6;
        private static string _boardSizeStringFormat { get => "Board Size: {0}x{0} (Click to increase)"; }
        private static string _boardSizeMsg { get; set; }
        private static string _boardSizeText
        {
            set
            {
                _boardSizeMsg = string.Format(_boardSizeStringFormat, value);
            }
            get 
            {
                return _boardSizeMsg;
            }
        }

        public GameSettingsWindow()
        {
            InitializeComponent();
        }

        private void GameSettingsWindow_Load(object sender, EventArgs e)
        {

        }

        private void boardSizeButton_Click(object sender, EventArgs e)
        {
            Button boardSizeBtn = sender as Button;
            _boardSize += 2;
            _boardSize = _boardSize > 12 ? 6 : _boardSize;
            _boardSizeText = _boardSize.ToString();
            boardSizeBtn.Text = _boardSizeText; 
        }

        private void playComputerButton_Click(object sender, EventArgs e)
        {
            StartNewGame();
        }

        private void playHumanButton_Click(object sender, EventArgs e)
        {
            StartNewGame();
        }

        private void StartNewGame()
        {
            this.Hide();
            _gameWindow = new GameWindow(_boardSize);
            _gameWindow.ShowDialog();
            this.Close();
            
        }
    }
}
