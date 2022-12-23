namespace OtheloLogic
{
    public class Player
    {
        public Player(string name, bool isComputer)
        {
            Name = name;
            IsComputer = isComputer;
        }

        public string Name { get; set; }
        public bool IsComputer { get; set; }
    }
}
