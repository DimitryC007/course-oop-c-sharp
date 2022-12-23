namespace OtheloLogic
{
    public class GameSettings
    {
        public GameSettings(int numOfPlayers)
        {
            Players = new Player[numOfPlayers];
        }

        public int MatrixSize { get; set; }
        public Player[] Players { get; set; }
    }
}
