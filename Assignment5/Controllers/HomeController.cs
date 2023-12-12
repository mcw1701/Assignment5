using Assignment5.Data;
using Assignment5.Models;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Assignment5.Controllers
{
    public class HomeController : Controller
    {
        private readonly Assignment5Context _context;

        public HomeController(Assignment5Context context)
        {
            _context = context;
        }

        // GET: Index
        public async Task<IActionResult> Index(string genre, string performer)
        {
            if (_context.Song == null)
            {
                return Problem("Entity set 'Assignment5Context.Song' is null.");
            }

            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.Song
                                            orderby m.Genre
                                            select m.Genre;
            IQueryable<string> performerQuery = from m in _context.Song
                                                orderby m.Performer
                                                where m.Genre == genre
                                                select m.Performer;
            var songs = from m in _context.Song
                        select m;

            if (!string.IsNullOrEmpty(genre))
            {
                songs = songs.Where(x => x.Genre == genre);
            } else
            {
                songs = songs.Where(x => x.Genre == "");
            }

            if (!string.IsNullOrEmpty(performer))
            {
                songs = songs.Where(s => s.Performer == performer);
            } else
            {
                songs = songs.Where(x => x.Performer == "");
            }

            var songGenrePerformerVM = new SongGenrePerformerViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Performers = new SelectList(await performerQuery.Distinct().ToListAsync()),
                Songs = await songs.ToListAsync()
            };

            return View(songGenrePerformerVM);
        }

        public async Task<IActionResult> ShoppingCart(string[] selectedSongs)
        {
            if (_context.Song == null)
            {
                return Problem("Entity set 'Assignment5Context.Song' is null.");
            }

            List<Song> songsInCart = new List<Song>();
            foreach (string songId in selectedSongs)
            {
                int id = int.Parse(songId);
                var songs = from s in _context.Song
                            where s.Id == id
                            select s;
                songsInCart.AddRange(await songs.ToListAsync());
            }

            var Cart = new Cart
            {
                Songs = songsInCart
            };

            decimal total = 0;
            foreach (Song song in Cart.Songs)
            {
                total += song.Price;
            }
            Cart.total = total;
            return View(Cart);
        }

        // GET: Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            var users = from u in _context.NetUser
                        where u.UserName == username &&
                        u.UserPassword == password
                        select u;
            if (await users.CountAsync() != 0)
            {
                NetUser user = await users.FirstAsync();
                HttpContext.Session.SetString(IndexModel.SessionKeyUser, user.UserName);
                HttpContext.Session.SetString(IndexModel.SessionKeyType, user.UserType);
                return RedirectToAction(nameof(Index));
            } else
            {
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove(IndexModel.SessionKeyUser);
            HttpContext.Session.Remove(IndexModel.SessionKeyType);
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
