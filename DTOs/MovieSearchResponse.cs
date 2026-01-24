public class MovieSearchResponse
{
    public List<TmdbMovieDto> Results { get; set; }
}

public class TmdbMovieDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Release_Date { get; set; }
    public string Poster_Path { get; set; } 
    public string Overview { get; set; }
}