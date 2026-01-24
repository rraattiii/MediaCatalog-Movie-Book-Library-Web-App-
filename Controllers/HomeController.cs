using Microsoft.AspNetCore.Mvc;
using MediaCatalogApp.Data;
using MediaCatalogApp.Models;
using System.Linq;

namespace MediaCatalogApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            ViewBag.MovieCount = _context.MediaItems.Count(m => m.MediaType == "Movie");
            ViewBag.BookCount = _context.MediaItems.Count(m => m.MediaType == "Book");


            ViewBag.TopRated = _context.MediaItems
                .Where(m => m.UserRating >= 4)
                .OrderByDescending(m => m.UserRating)
                .Take(4)
                .ToList();

            return View();
        }
    }
}