namespace OtheloLogic
{
    public class GameReport
    {
        public GameStatus GameStatus { get; }
        public MoveStatus MoveStatus { get; set; }
        public Player Winner { get; }
        public Player Loser { get; }
        public int WinnerPoints { get; }
        public int LoserPoints { get; }
    }
}
