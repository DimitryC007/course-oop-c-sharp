namespace OtheloLogic
{
    public class GameReport
    {
        public eGameStatuses GameStatus { get; internal set; }
        public eMoveStatuses MoveStatus { get; internal set; }
        public Player Winner { get; internal set; }
        public Player Loser { get; internal set; }
        public int WinnerPoints { get; internal set; }
        public int LoserPoints { get; internal set; }
        public string LastMovePlayerName { get; internal set; }
    }
}
