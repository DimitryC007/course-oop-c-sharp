namespace OtheloLogic
{
    public class Player
    {
        private int _count = 0;
        public Player(string name, bool isComputer)
        {
            Name = name;
            IsComputer = isComputer;
        }
        public string Name { get; set; }
        public bool IsComputer { get; set; }

        public int Count { get; set; }

    }
}
