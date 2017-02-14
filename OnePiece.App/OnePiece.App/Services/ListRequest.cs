namespace OnePiece.App.Services
{
    public abstract class ListRequest
    {
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}