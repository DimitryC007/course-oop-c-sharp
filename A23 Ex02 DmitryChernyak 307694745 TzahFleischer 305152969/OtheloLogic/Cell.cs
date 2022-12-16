namespace OtheloLogic
{
    public class Cell
    {
        public bool IsTaken => Value != null;
        public int? Value { get; internal set; }
    }
}
