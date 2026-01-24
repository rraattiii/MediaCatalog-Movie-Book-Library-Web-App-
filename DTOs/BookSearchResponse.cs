namespace MediaCatalogApp.DTOs
{
    public class BookSearchResponse
    {

        public List<BookDoc>? Docs { get; set; }
    }

    public class BookDoc
    {
        public string Title { get; set; } = string.Empty;
        public List<string>? Author_Name { get; set; }
        public int? First_Publish_Year { get; set; }
        public long? Cover_I { get; set; } 
        public string? Key { get; set; } 
    }
}