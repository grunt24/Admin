using BCASLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BCASLibrary.Controllers
{
    [Authorize]

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _db;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _db = context;
        }

        public IActionResult Index(string searchBy, string searchValue)
        {
            try
            {
                var bookList = _db.Book.Include(g => g.Genre).ToList();

                if (bookList.Count == 0)
                {
                    TempData["InfoMessage"] = "There is no Books available at the moment";
                }
                else
                {
                    if (string.IsNullOrEmpty(searchValue))
                    {
                        TempData["InfoMessage"] = "Please provide search value";
                        return View(bookList);
                    }
                    else
                    {
                        if(searchBy.ToLower() == "genrename")
                        {
                            var searchByGenreName = bookList.Where(g => g.Genre.GenreName.ToLower().Contains(searchValue.ToLower()));
                            return View(searchByGenreName);
                        }
                    }
                }

                var totalRecords = bookList.Count(); 
                var totalPages = Math.Ceiling

                return View(bookList);
            }
            catch (Exception ex)
            {
                TempData["ErroMessage"] = ex.Message;
                return View();
            }
        }
        public IActionResult Details(int? id)
        {
            Book book = _db.Book.Include(g => g.Genre).SingleOrDefault(u => u.Id == id);
            return View(book);
        }



        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}