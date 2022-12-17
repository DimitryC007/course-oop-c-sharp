namespace OtheloLogic
{
    public class Cell
    {
        //0 white, 1 black
        public bool IsTaken => Value != null;
        public int? Value { get; internal set; }
    }
}
