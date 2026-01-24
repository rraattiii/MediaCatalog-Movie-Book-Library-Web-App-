namespace MediaCatalogApp.Models
{
    public class MediaItem
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }
        public string? ExternalId { get; set; }
        public string? MediaType { get; set; } 
        public string? UserId { get; set; }
        
        public string? Author { get; set; } 
        public int? UserRating { get; set; } 
    }
}