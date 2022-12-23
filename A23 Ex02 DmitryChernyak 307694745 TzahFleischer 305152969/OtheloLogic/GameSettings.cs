namespace OtheloLogic
{
    public class GameSettings
    {
        public GameSettings(int i_NumOfPlayers)
        {
            Players = new Player[i_NumOfPlayers];
        }

        public int MatrixSize { get; set; }
        public Player[] Players { get; set; }
    }
}
