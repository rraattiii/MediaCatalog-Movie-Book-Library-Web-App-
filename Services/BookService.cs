using System.Text.Json;
using System.Text.Json.Serialization;
using MediaCatalogApp.Models;

public class BookService
{
    private readonly HttpClient _httpClient;

    public BookService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<BookResult>> SearchBooksAsync(string searchTerm)
{
    var url = $"https://openlibrary.org/search.json?q={Uri.EscapeDataString(searchTerm)}";
    var response = await _httpClient.GetAsync(url);
    var content = await response.Content.ReadAsStringAsync();
    

    var data = JsonSerializer.Deserialize<MediaCatalogApp.DTOs.BookSearchResponse>(content, 
        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });


    return data?.Docs?.Select(d => new BookResult
    {
        Id = d.Key,
        Title = d.Title,
        Author = d.Author_Name != null ? string.Join(", ", d.Author_Name) : "Unknown",
        ImageUrl = d.Cover_I.HasValue 
            ? $"https://covers.openlibrary.org/b/id/{d.Cover_I}-M.jpg" 
            : "https://via.placeholder.com/128x192?text=No+Cover"
    }).ToList() ?? new List<BookResult>();
}
}


public class GoogleBooksResponse
{
    [JsonPropertyName("items")] public List<GoogleBookItem>? Items { get; set; }
}

public class GoogleBookItem
{
    [JsonPropertyName("id")] public string? Id { get; set; }
    [JsonPropertyName("volumeInfo")] public VolumeInfo VolumeInfo { get; set; } = new();
}

public class VolumeInfo
{
    [JsonPropertyName("title")] public string? Title { get; set; }
    [JsonPropertyName("authors")] public List<string>? Authors { get; set; }
    [JsonPropertyName("imageLinks")] public ImageLinks? ImageLinks { get; set; }
}

public class ImageLinks
{
    [JsonPropertyName("thumbnail")] public string? Thumbnail { get; set; }
}