namespace OtheloLogic
{
    public class Cell
    {
        //0 white, 1 black
        //0 black, 1 white
        public bool IsTaken => Value != null;
        public int? Value { get; internal set; }
    }


}
