namespace OtheloLogic
{
    public class GameReport
    {
        public GameStatus GameStatus { get; internal set; }
        public MoveStatus MoveStatus { get; internal set; }
        public Player Winner { get; internal set; }
        public Player Loser { get; internal set; }
        public int WinnerPoints { get; internal set; }
        public int LoserPoints { get; internal set; }
    }
}
