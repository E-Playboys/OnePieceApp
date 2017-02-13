namespace OnePiece.App.Models
{
    public class MangaImage
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public bool IsLoading { get; set; }

        public int Number { get; set; }
        public string Description { get; set; }
        public string AlternativeLink { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int ImageWidth { get; set; }

        public int MangaChapterId { get; set; }
    }
}
