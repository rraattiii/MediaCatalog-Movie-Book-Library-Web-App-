using Microsoft.AspNetCore.Mvc;
using MediaCatalogApp.Data;
using MediaCatalogApp.Models;

public class BookController : Controller
{
    private readonly BookService _bookService;
    private readonly ApplicationDbContext _context;

    public BookController(BookService bookService, ApplicationDbContext context)
    {
        _bookService = bookService;
        _context = context;
    }

    public async Task<IActionResult> Index(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm)) 
            return View(new List<BookResult>());
        
        var results = await _bookService.SearchBooksAsync(searchTerm);
        return View(results);
    }

    [HttpPost]
    public async Task<IActionResult> AddToLibrary(string Title, string ImageUrl, string ExternalId, string Author)
    {
        var item = new MediaItem
        {
            Title = Title,
            ImageUrl = ImageUrl,
            ExternalId = ExternalId,
            Author = Author,
            MediaType = "Book",
            UserId = "DefaultUser"
        };

        _context.MediaItems.Add(item);
        await _context.SaveChangesAsync();
        

        return RedirectToAction("MyLibrary", "Movie");
    }
}