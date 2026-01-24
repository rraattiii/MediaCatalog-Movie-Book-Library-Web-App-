#  MediaCatalogApp (2025)

A modern, full-stack web application built with **ASP.NET Core 10** for managing a personal library of movies and books.

## Features
- **Dual-Source Search:** Integrates with TMDB API for movies and OpenLibrary API for books.
- **Glass-morphism UI:** A custom, dark-themed interface with backdrop-blur effects and fluid animations.
- **Personal Library:** Save titles to a persistent SQL database via Entity Framework Core.
- **Responsive Grid:** Optimized for mobile and desktop viewing.

##  Tech Stack
- **Backend:** C#, ASP.NET Core MVC, Entity Framework Core
- **Frontend:** HTML5, CSS3 (Custom Scrollbars/Animations), Bootstrap 5, FontAwesome
- **Database:** MS SQL Server / SQLite
- **APIs:** The Movie Database (TMDB), Open Library API

##  How to Run
1. Clone the repo: `git clone [Your-Link]`
2. Update `appsettings.json` with your TMDB API Key.
3. Run `dotnet ef database update` to sync the schema.
4. Run `dotnet run` and visit `http://localhost:5236`.
