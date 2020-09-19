using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using ViewModels;

namespace Main.Controllers
{


    public class BooksController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly DatabaseContext _databaseContext;

        public BooksController(IUnitOfWork UnitOfWork, IWebHostEnvironment webHostEnvironment, DatabaseContext databaseContext)
        {
            _unitOfWork = UnitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _databaseContext = databaseContext;
        }

        // GET: Books
     
        public IActionResult Index()
        {
            var listBooks =  _unitOfWork.BookRepository.Get().ToList();
            List<BookViewModel> listVm = new List<BookViewModel>();
            var hh = _unitOfWork.LessonRepository.Get().ToList();

            ViewData["Lesson"] = _unitOfWork.LessonRepository.Get().ToList();
            foreach (var item in listBooks)
            {
                BookViewModel Vm = new BookViewModel();
                Vm.Id = item.Id;
                Vm.Author = item.Author;
                Vm.BookTypeId = item.BookTypeId;
                Vm.Content = item.Content;
                Vm.ContPage = _databaseContext.PageBases.Where(C => C.BookId == item.Id).Count();
                Vm.Cover = item.Cover;
                Vm.CreateDate = item.CreateDate;
                Vm.CreateDateShamsi = item.CreateDateShamsi;
                Vm.Enable = item.Enable;
                Vm.EndCreateDate = item.EndCreateDate;
                Vm.EndCreateDateShamsi = item.EndCreateDateShamsi;
                Vm.FieldOfStudyId = item.FieldOfStudyId;
                Vm.LessonId = item.LessonId;
                Vm.Name = item.Name;
                if(item.ProductTypeId == 1)
                {
                    Vm.ProductName = " آموزش ";
                }
                if(item.ProductTypeId == 2)
                {
                    Vm.ProductName = "تست ";
                }
                if(item.ProductTypeId == 3)
                {
                    Vm.ProductName = "امتحان ";
                }
                Vm.Publisher = item.Publisher;
                listVm.Add(Vm);
            }
            return View(listVm);
        }

        [HttpPost]
        public IActionResult Index(int lesson,int product,int section)
        {
            ViewData["Lesson"] = _unitOfWork.LessonRepository.Get().ToList();

            var listFull = _unitOfWork.BookRepository.Get().AsQueryable();
            List<BookViewModel> listVm = new List<BookViewModel>();

            if (lesson != 0)
            {
                listFull = listFull.Where(C => C.LessonId == lesson);
            }
            if (product != 0)
            {
                listFull = listFull.Where(C => C.ProductTypeId == product);
            }
            if (section != 0)
            {
                listFull = listFull.Where(C => C.SectionId == section);
            }
            var listBooks = listFull.ToList();
            foreach (var item in listBooks)
            {
                BookViewModel Vm = new BookViewModel();
                Vm.Id = item.Id;
                Vm.Author = item.Author;
                Vm.BookTypeId = item.BookTypeId;
                Vm.Content = item.Content;
                Vm.ContPage = _databaseContext.PageBases.Where(C => C.BookId == item.Id).Count();
                Vm.Cover = item.Cover;
                Vm.CreateDate = item.CreateDate;
                Vm.CreateDateShamsi = item.CreateDateShamsi;
                Vm.Enable = item.Enable;
                Vm.EndCreateDate = item.EndCreateDate;
                Vm.EndCreateDateShamsi = item.EndCreateDateShamsi;
                Vm.FieldOfStudyId = item.FieldOfStudyId;
                Vm.LessonId = item.LessonId;
                Vm.Name = item.Name;
                if (item.ProductTypeId == 1)
                {
                    Vm.ProductName = " آموزش ";
                }
                if (item.ProductTypeId == 2)
                {
                    Vm.ProductName = "تست ";
                }
                if (item.ProductTypeId == 3)
                {
                    Vm.ProductName = "امتحان ";
                }
                Vm.Publisher = item.Publisher;
                listVm.Add(Vm);
            }
            return View(listVm);
        }


        // GET: Books/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book =  _unitOfWork.BookRepository.GetById(id.Value);
                
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            //ViewData["BookTypeId"] = new SelectList(_unitOfWork.BookTypeRepository.Get(), "Id", "Name");

            ViewData["FieldOfStudyId"] = new SelectList(_unitOfWork.FieldOfStudyRepository.Get(), "Id", "Name");
            ViewData["SectionId"] = new SelectList(_unitOfWork.SectionRepository.Get(), "Id", "Name");
            ViewData["ProductTypeId"] = new SelectList(_unitOfWork.ProductTypeRepository.Get(), "Id", "Name");

            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("UploadFiles")]
        [ValidateAntiForgeryToken]
        public  IActionResult Create(Book book , List<IFormFile> file)
        {
            book.BookTypeId = 1;
            book.Enable = false;
            var Book = _unitOfWork.BookRepository.Get().Where(C => C.Name == book.Name.Trim()).FirstOrDefault();
            if (Book!=null)
            {
                ModelState.AddModelError(key: "Name", errorMessage: "نام کتاب تکراری می باشد");
            }

            if (book.FieldOfStudyId == 0)
            {
                ModelState.AddModelError(key: "FieldOfStudyId", errorMessage: "انتخاب رشته تحصیلی الزامی می باشد");
            }
            if (book.SectionId == 0)
            {
                ModelState.AddModelError(key: "SectionId", errorMessage: "انتخاب مقطع تحصیلی الزامی می باشد");
            }
            if (book.CreateDateShamsi == null)
            {
                ModelState.AddModelError(key: "CreateDateShamsi", errorMessage: "انتخاب تاریخ تالیف الزامی می باشد");
            }
            if (book.EndCreateDateShamsi == null)
            {
                book.EndCreateDate = DateTime.Now;
            }
            else
            {
                DateTime EndDateMiladi = DateTime.Parse(book.EndCreateDateShamsi, new CultureInfo("fa-IR"));
                book.EndCreateDate = EndDateMiladi;
            }

            string webRootPath = _webHostEnvironment.WebRootPath;
            long size = file.Sum(f => f.Length);


            foreach (var f in file)
            {
                var filePath = Path.Combine(webRootPath + "/cover", f.FileName);
                book.Cover = "/cover/" + f.FileName;
                if (f.Length > 0)
                {
                    //Directory.CreateDirectory(webRootPath + "/cover");

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        f.CopyTo(stream);
                    }
                }
            }
          

          

            
            if (ModelState.IsValid)
            {
                DateTime StartDateMiladi = DateTime.Parse(book.CreateDateShamsi, new CultureInfo("fa-IR"));
                book.CreateDate = StartDateMiladi;
              
                _unitOfWork.BookRepository.Insert(book);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FieldOfStudyId"] = new SelectList(_unitOfWork.FieldOfStudyRepository.Get(), "Id", "Name");
            ViewData["SectionId"] = new SelectList(_unitOfWork.SectionRepository.Get(), "Id", "Name");
            ViewData["ProductTypeId"] = new SelectList(_unitOfWork.ProductTypeRepository.Get(), "Id", "Name");
            return View(book);
        }

        // GET: Books/Edit/5
        public IActionResult Edit(int? id)
        {
            var book = _unitOfWork.BookRepository.GetById(id.Value);
            ViewData["FieldOfStudyId"] = new SelectList(_unitOfWork.FieldOfStudyRepository.Get(), "Id", "Name");
            ViewData["SectionId"] = new SelectList(_unitOfWork.SectionRepository.Get(), "Id", "Name");
            ViewData["ProductTypeId"] = new SelectList(_unitOfWork.ProductTypeRepository.Get(), "Id", "Name");


            if (id == null)
            {
                return NotFound();
            }

            ViewData["LessonId"] = new SelectList(_unitOfWork.LessonRepository.Get().Where(C => C.SectionId == book.SectionId && C.FieldOfStudyId==book.FieldOfStudyId), "Id", "Name");

            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,Book book, List<IFormFile> file)
        {
          
            ViewData["FieldOfStudyId"] = new SelectList(_unitOfWork.FieldOfStudyRepository.Get(), "Id", "Name");
            ViewData["SectionId"] = new SelectList(_unitOfWork.SectionRepository.Get(), "Id", "Name");
            ViewData["ProductTypeId"] = new SelectList(_unitOfWork.ProductTypeRepository.Get(), "Id", "Name");
            ViewData["LessonId"] = new SelectList(_unitOfWork.LessonRepository.Get().Where(C => C.SectionId == book.SectionId && C.FieldOfStudyId == book.FieldOfStudyId), "Id", "Name");
            if (id != book.Id)
            {
                return NotFound();
            }

            Book Orginalbook = _unitOfWork.BookRepository.Get().Where(C => C.Id == book.Id).FirstOrDefault();

            DateTime StartDateMiladi = DateTime.Parse(book.CreateDateShamsi, new CultureInfo("fa-IR"));
            DateTime EndDateMiladi = DateTime.Parse(book.EndCreateDateShamsi, new CultureInfo("fa-IR"));


            book.CreateDate = StartDateMiladi;
            book.EndCreateDate = EndDateMiladi;
            if (book.CreateDate>book.EndCreateDate)
            {
                ModelState.AddModelError(key: "CreateDateShamsi", errorMessage: "تاریخ شروع تالیف  نمی تواند بزرگتر از پایان تاریخ تالیف باشد");
            }
            Orginalbook.Name = book.Name;
            Orginalbook.FieldOfStudyId = book.FieldOfStudyId;
            Orginalbook.Author = book.Author;
            Orginalbook.BookTypeId = book.BookTypeId;
            Orginalbook.Content = book.Content;
            Orginalbook.CreateDate = book.CreateDate;
            Orginalbook.LessonId = book.LessonId;
            Orginalbook.ProductTypeId = book.ProductTypeId;
            Orginalbook.Publisher = book.Publisher;
            Orginalbook.SectionId = book.SectionId;
            Orginalbook.CreateDateShamsi = book.CreateDateShamsi;
            Orginalbook.EndCreateDateShamsi = book.EndCreateDateShamsi;
            Orginalbook.Enable = book.Enable;
           
            if (file.Count != 0)
            {
                string webRootPath = _webHostEnvironment.WebRootPath;
                long size = file.Sum(f => f.Length);


                foreach (var f in file)
                {
                    var filePath = Path.Combine(webRootPath + "/cover", f.FileName);
                    Orginalbook.Cover = "/cover/" + f.FileName;
                    if (f.Length > 0)
                    {
                        //Directory.CreateDirectory(webRootPath + "/cover");

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            f.CopyTo(stream);
                        }
                    }
                }
            }
       

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.BookRepository.Update(Orginalbook);
                    _unitOfWork.Save();
                }
                catch (Exception)
                {
                    if (!BookExists(book.Id))
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
            return View(book);
        }

        // GET: Books/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _unitOfWork.BookRepository.GetById(id.Value);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var book = _unitOfWork.BookRepository.GetById(id);
            _unitOfWork.BookRepository.Delete(book);
            _unitOfWork.Save() ;
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _unitOfWork.BookRepository.Get().Any(e => e.Id == id);
        }
        [HttpPost]
        public JsonResult GetLesson(int idFileadOfStudy, int idSection)
        {
            var listlesson = _unitOfWork.LessonRepository.Get().Where(C => C.FieldOfStudyId == idFileadOfStudy && C.SectionId == idSection).ToList();
            return Json(listlesson);
        }
       public IActionResult CreatePaper()
        {
            return View();
        }




    }
}
