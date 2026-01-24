using Microsoft.AspNetCore.Mvc;
using MediaCatalogApp.Data;
using MediaCatalogApp.Models;
using Microsoft.EntityFrameworkCore;

public class MovieController : Controller
{
    private readonly MovieService _movieService;
    private readonly ApplicationDbContext _context;

    public MovieController(MovieService movieService, ApplicationDbContext context)
    {
        _movieService = movieService;
        _context = context;
    }


    public async Task<IActionResult> Index(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm)) return View(new List<MovieResult>());
        var results = await _movieService.SearchMoviesAsync(searchTerm);
        return View(results);
    }


    public IActionResult MyLibrary(string searchTerm, string mediaType)
    {
        var query = _context.MediaItems.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
            query = query.Where(m => m.Title.Contains(searchTerm));

        if (!string.IsNullOrWhiteSpace(mediaType))
            query = query.Where(m => m.MediaType == mediaType);

        var library = query
            .OrderByDescending(m => m.UserRating)
            .ThenBy(m => m.Title)
            .ToList();

        return View(library);
    }

    [HttpPost]
    public async Task<IActionResult> AddToLibrary(MediaItem item)
    {
        var exists = await _context.MediaItems.AnyAsync(m => m.ExternalId == item.ExternalId);
        if (!exists)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(MyLibrary));
    }

    [HttpPost]
    public async Task<IActionResult> UpdateRating(int id, int rating)
    {
        var item = await _context.MediaItems.FindAsync(id);
        if (item != null)
        {
            item.UserRating = rating;
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(MyLibrary));
    }

    [HttpPost]
    public async Task<IActionResult> DeleteFromLibrary(int id)
    {
        var item = await _context.MediaItems.FindAsync(id);
        if (item != null)
        {
            _context.MediaItems.Remove(item);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(MyLibrary));
    }
}