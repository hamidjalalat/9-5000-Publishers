using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Main.Controllers
{
    public class DisplayBooksController : Controller
    {

        private readonly DatabaseContext _context;

        public DisplayBooksController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["ListTajrobi"] = _context.Books.Where(C => C.FieldOfStudyId == 1).ToList();
            ViewData["ListEnsani"] = _context.Books.Where(C => C.FieldOfStudyId == 2).ToList();
            ViewData["ListRiazi"] = _context.Books.Where(C => C.FieldOfStudyId == 3).ToList();

            return View();
        }
    }
}