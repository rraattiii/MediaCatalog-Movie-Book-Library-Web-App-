using MediaCatalogApp.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http.Headers;

public class MovieService
{
    private readonly HttpClient _httpClient;
    private readonly string _bearerToken = "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiIwNTg2ZmFkYzEzMjdhZDdmMDQ5YjhjZmVlZjg3NjFjZSIsIm5iZiI6MTc2OTAwNjM5Ny4zNTMsInN1YiI6IjY5NzBlNTNkMzQ0NjMxNDFkYjI2ZjMzNyIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.cirgq8dQuJXOJkkG6Kh4Xc7wS4vLxlX-tDNdx_x-5WA"; 

    public MovieService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<List<MovieResult>> SearchMoviesAsync(string searchTerm)
    {
        var url = $"https://api.themoviedb.org/3/search/movie?query={Uri.EscapeDataString(searchTerm)}";
        
        var response = await _httpClient.GetAsync(url);
        
        if (!response.IsSuccessStatusCode) return new List<MovieResult>();

        var content = await response.Content.ReadAsStringAsync();
        
       
        var data = JsonSerializer.Deserialize<TmdbResponseInternal>(content);

        
        return data?.Results?.Select(m => new MovieResult
        {
            Id = m.Id,
            Title = m.Title,
            PosterUrl = string.IsNullOrEmpty(m.PosterPath) 
                ? "https://via.placeholder.com/500x750?text=No+Image" 
                : $"https://image.tmdb.org/t/p/w500{m.PosterPath}",
            ReleaseDate = m.ReleaseDate
        }).ToList() ?? new List<MovieResult>();
    }
}


public class TmdbResponseInternal
{
    [JsonPropertyName("results")]
    public List<TmdbItemDto> Results { get; set; } = new();
}

public class TmdbItemDto
{
    [JsonPropertyName("id")] 
    public int Id { get; set; }

    [JsonPropertyName("title")] 
    public string? Title { get; set; }

    [JsonPropertyName("poster_path")] 
    public string? PosterPath { get; set; }

    [JsonPropertyName("release_date")] 
    public string? ReleaseDate { get; set; }
}