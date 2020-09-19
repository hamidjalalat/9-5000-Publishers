using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Main.Controllers
{
    public class ShowContantBookController : Controller
    {
        private readonly DatabaseContext _context;

        public ShowContantBookController(DatabaseContext context)
        {
            _context = context;
        }
        public IActionResult Index(int Id)
        {
            TempData["Id"] = Id;
   
            return View();
        }
        [HttpPost]
        public JsonResult PageBaseFullList()
        {
            var Id = TempData["Id"] as int? ;
            TempData.Keep();
            var listPageBase = _context.PageBases.Where(C=>C.BookId== Id).ToList();
            List<PageBaseFullList> pageBaseFullLists = new List<PageBaseFullList>();

            foreach (var item in listPageBase)
            {
                PageBaseFullList pBFL = new PageBaseFullList();
                pBFL.Id = item.Id;
                pBFL.BookId = item.BookId;
                pBFL.Content = item.Content;
                pBFL.Enable = item.Enable;
                pBFL.PageNumber = item.PageNumber;
                pBFL.SessionNumber = item.SessionNumber;
                pBFL.ServerPath = item.ServerPath;
                pBFL.GoldenTips = _context.GoldenTips.Where(C => C.PageBaseId == item.Id).OrderBy(C => C.PageNumber).ToList();
                pBFL.ConceptualPoints = _context.ConceptualPoints.Where(C => C.PageBaseId == item.Id).OrderBy(C => C.PageNumber).ToList();
                pBFL.LetterCharts = _context.LetterCharts.Where(C => C.PageBaseId == item.Id).OrderBy(C => C.PageNumber).ToList();
                pBFL.LetterTables = _context.LetterTables.Where(C => C.PageBaseId == item.Id).OrderBy(C => C.PageNumber).ToList();
                pBFL.Movies = _context.Movies.Where(C => C.PageBaseId == item.Id).OrderBy(C => C.PageNumber).ToList();
                pageBaseFullLists.Add(pBFL);
            }

            var result = new { pageBaseFullLists = pageBaseFullLists.OrderBy(C=>C.PageNumber).ToList() };

            return Json(result);
        }

        public IActionResult PageBaseEpub(string Path)
        {
            ViewData["Path"] = Path; 
            return View();
        }
        public IActionResult TipsEpub(int pageNumber, int idTips)
        {
            var bookId = TempData["Id"] as int?;
            TempData.Keep();
            var id = _context.PageBases.Where(C => C.PageNumber == pageNumber && C.BookId== bookId).FirstOrDefault().Id;
            switch (idTips)
            {
                case 1:
                    {
                        var result = _context.GoldenTips.Where(C => C.PageBaseId == id).FirstOrDefault();
                        if (result!=null)
                        {
                            var path = result.ServerPath;
                            ViewData["Path"] = path;
                        }
                      

                        break;
                    }
                case 2:
                    {
                        var result = _context.ConceptualPoints.Where(C => C.PageBaseId == id).FirstOrDefault();
                        if (result != null)
                        {
                            var path = result.ServerPath;
                            ViewData["Path"] = path;
                        }
                        break;
                    }
                case 3:
                    {
                        var result = _context.LetterCharts.Where(C => C.PageBaseId == id).FirstOrDefault();
                        if (result != null)
                        {
                            var path = result.ServerPath;
                            ViewData["Path"] = path;
                        }
                        break;
                    }
                case 4:
                    {
                        var result = _context.LetterTables.Where(C => C.PageBaseId == id).FirstOrDefault();
                        if (result != null)
                        {
                            var path = result.ServerPath;
                            ViewData["Path"] = path;
                        }
                        break;
                    }
            }
            

            TempData["idTips"] = idTips;
            TempData["pageNumber"] = pageNumber;

            return View();
        }
        [HttpPost]
        public JsonResult GetListTips()
        {
            var pageNumber = TempData["pageNumber"] as int?;
            var idTips = TempData["idTips"] as int?;
            var bookId = TempData["Id"] as int?;
            TempData.Keep();

            var id = _context.PageBases.Where(C => C.PageNumber == pageNumber && C.BookId== bookId).FirstOrDefault().Id;

            switch (idTips)
            {
                case 1:
                    {
                        var pageBaseFullLists = _context.GoldenTips.Where(C => C.PageBaseId == id).ToList();
                        var result = new { pageBaseFullLists = pageBaseFullLists.OrderBy(C => C.PageNumber).ToList() };

                        return Json(result);

                    }
                case 2:
                    {
                        var pageBaseFullLists = _context.ConceptualPoints.Where(C => C.PageBaseId == id).ToList();
                        var result = new { pageBaseFullLists = pageBaseFullLists.OrderBy(C => C.PageNumber).ToList() };

                        return Json(result);
                    }
                case 3:
                    {
                        var pageBaseFullLists = _context.LetterCharts.Where(C => C.PageBaseId == id).ToList();
                        var result = new { pageBaseFullLists = pageBaseFullLists.OrderBy(C => C.PageNumber).ToList() };

                        return Json(result);
                    }
                case 4:
                    {
                        var pageBaseFullLists = _context.LetterTables.Where(C => C.PageBaseId == id).ToList();
                        var result = new { pageBaseFullLists = pageBaseFullLists.OrderBy(C => C.PageNumber).ToList() };

                        return Json(result);
                    }
                default:
                    {
                        var pageBaseFullLists = _context.GoldenTips.Where(C => C.PageBaseId == id).ToList();
                        var result = new { pageBaseFullLists = pageBaseFullLists.OrderBy(C => C.PageNumber).ToList() };

                        return Json(result);
                    }
            }


        }

    }
}