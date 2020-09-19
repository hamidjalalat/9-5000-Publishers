using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Main.Controllers
{
    public class PagesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PagesController(IUnitOfWork UnitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = UnitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Pages
        public async Task<IActionResult> Index()
        {
            var databaseContext = _unitOfWork.PageRepository.Get().Include(p => p.Book);
            return View(await databaseContext.ToListAsync());
        }

        // GET: Pages/Details/5
        public  IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = _unitOfWork.PageRepository.GetById(id.Value);
             
            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }

        // GET: Pages/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_unitOfWork.BookRepository.Get(), "Id", "Name");
            return View();
        }

        // POST: Pages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("PageNumber,ServerPath,SessionNumber,Content,Enable,BookId,Id")] Page page)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.PageRepository.Insert(page);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_unitOfWork.BookRepository.Get(), "Id", "Name", page.BookId);
            return View(page);
        }

        // GET: Pages/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = _unitOfWork.PageRepository.GetById(id.Value);
            if (page == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_unitOfWork.BookRepository.Get(), "Id", "Name", page.BookId);
            return View(page);
        }

        // POST: Pages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("PageNumber,ServerPath,SessionNumber,Content,Enable,BookId,Id")] Page page)
        {
            if (id != page.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.PageRepository.Update(page);
                    _unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PageExists(page.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_unitOfWork.BookRepository.Get(), "Id", "Author", page.BookId);
            return View(page);
        }

        // GET: Pages/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = _unitOfWork.PageRepository.GetById(id.Value);
              
            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }

        // POST: Pages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _unitOfWork.PageRepository.DeleteById(id);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool PageExists(int id)
        {
            return _unitOfWork.PageRepository.Get().Any(e => e.Id == id);
        }
    }
}
