using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using System.IO.Compression;

namespace Main.Controllers
{
    public class PageBasesController:Controller
    {
        private readonly DatabaseContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PageBasesController(DatabaseContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: PageBases
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.PageBases.Include(p => p.Book);
            return View(await databaseContext.ToListAsync());
        }

        // GET: PageBases/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageBase = await _context.PageBases
                .Include(p => p.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pageBase == null)
            {
                return NotFound();
            }

            return View(pageBase);
        }

        // GET: PageBases/Create
        public IActionResult Create()
        {

            PageBase pb = new PageBase();
            
            var firstBook = _context.Books.FirstOrDefault();

            var countBookPage = _context.PageBases.Where(C => C.BookId == firstBook.Id).ToList();
            int lastPage = 0;

            if (countBookPage.Count > 0)
            {
                lastPage = (countBookPage.Max(C => C.PageNumber)) + 1;
            }
            else
            {
                lastPage++;
            }
        
            pb.PageNumber = lastPage;
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Name");

            if (TempData["PageNumber"]!=null && TempData["BookId"] != null)
            {
                int bookId =(int) TempData["BookId"];
                ViewData["BookId"]= new SelectList(_context.Books.Where(C=>C.Id==bookId), "Id", "Name");
                pb.PageNumber = ((int)TempData["PageNumber"]) + 1;
            }

            return View(pb);
        }
        
        // POST: PageBases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( PageBase pageBase, List<IFormFile> file)
        {
           
            string webRootPath = _webHostEnvironment.WebRootPath;
            long size = file.Sum(f => f.Length);

            var filePath = Path.Combine(webRootPath + "\\pagebase");
            if (file.Count == 0)
            {
                ModelState.AddModelError(key: "ServerPath", errorMessage: "انتخاب فایل محتوا الزامی می باشد");
            }
            foreach (var f in file)
            {
                string comparePath = "/pagebase/" + f.FileName;
                var listNameFile = _context.PageBases.Where(C => C.ServerPath == comparePath.Replace("zip","htm") && C.BookId==pageBase.BookId).FirstOrDefault();
                //if (listNameFile !=null)
                //{
                //    ModelState.AddModelError(key: "ServerPath", errorMessage: " اسم فایل محتوا تکراری می باشد");
                //}

                var ext = Path.GetExtension(f.FileName);
                if (ext != ".zip")
                {
                    ModelState.AddModelError(key: "ServerPath", errorMessage: " فایل محتوا حتما باید زیپ باشد");
                }
            
                else
                {
                    string startPath = filePath;
                    string zipPath = filePath + "\\" + f.FileName;
                    string extractPath = filePath;

                    pageBase.ServerPath = "/pagebase/" + f.FileName.Replace("zip", "htm");

                    if (f.Length > 0)
                    {
                        //Directory.CreateDirectory(webRootPath + "/pagemovie");

                        using (var stream = new FileStream(zipPath, FileMode.Create))
                        {
                            f.CopyTo(stream);
                        }
                    }
                    //ZipFile.CreateFromDirectory(startPath, zipPath);

                    ZipFile.ExtractToDirectory(zipPath, extractPath, true);

                }

                //var filePath = Path.Combine(webRootPath + "/pagebase", f.FileName);
                //pageBase.ServerPath = "/pagebase/" + f.FileName;
                //if (f.Length > 0)
                //{
                //    //Directory.CreateDirectory(webRootPath + "/pagemovie");

                //    using (var stream = new FileStream(filePath, FileMode.Create))
                //    {
                //        f.CopyTo(stream);
                //    }
                //}
            }

            if (ModelState.IsValid)
            {
                _context.Add(pageBase);
                await _context.SaveChangesAsync();
                ViewData["BookId"] = new SelectList(_context.Books, "Id", "Name", pageBase.BookId);
                TempData["BookId"] = pageBase.BookId;
                TempData["PageNumber"] = pageBase.PageNumber;
                var BookId = _context.Books.Where(C => C.Id == pageBase.BookId).Select(C => C.LessonId).FirstOrDefault();
                var lessonTips = _context.Lessons.Where(C => C.Id == BookId).FirstOrDefault();

                ViewData["ListTips"] = lessonTips;
                return View(pageBase);
            }

            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Name", pageBase.BookId);
            return View(pageBase);
        }

        // GET: PageBases/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageBase = await _context.PageBases.FindAsync(id);
            if (pageBase == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Name", pageBase.BookId);
            return View(pageBase);
        }

        // POST: PageBases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PageBase pageBase, List<IFormFile> file)
        {
            if (file.Count==0)
            {
                ModelState.AddModelError(key: "ServerPath", errorMessage: "انتخاب فایل محتوا الزامی می باشد");
            }
            string webRootPath = _webHostEnvironment.WebRootPath;
            long size = file.Sum(f => f.Length);


            foreach (var f in file)
            {
                var filePath = Path.Combine(webRootPath + "/pagebase", f.FileName);
                pageBase.Content = "/pagebase/" + f.FileName;
                if (f.Length > 0)
                {

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        f.CopyTo(stream);
                    }
                }
            }
            if (id != pageBase.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pageBase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PageBaseExists(pageBase.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(SercheEditPage));
            }
            return RedirectToAction(nameof(SercheEditPage));
        }

        // GET: PageBases/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageBase = await _context.PageBases
                .Include(p => p.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pageBase == null)
            {
                return NotFound();
            }

            return View(pageBase);
        }

        // POST: PageBases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var pageBase = await _context.PageBases.FindAsync(id);
            _context.PageBases.Remove(pageBase);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PageBaseExists(Guid id)
        {
            return _context.PageBases.Any(e => e.Id == id);
        }
        [HttpPost]
        public IActionResult GetSaveGoldenTip( List<IFormFile> fileGoldenTips)
        {
            return Json(true) ;
        }


        [HttpPost]
        public IActionResult UploadFilessGoldenTips(string id,int goldenTipsPageNumber)
        {
            Guid PageBaseId = Guid.Parse(id);
            var HasRow = _context.GoldenTips.Where(C => C.PageBaseId == PageBaseId && C.PageNumber == goldenTipsPageNumber).FirstOrDefault();
            string message = "";
            bool result = false;
            if (HasRow!=null)
            {
                message = "شماره صفحه تکراری می باشد";
                result = false;
                return Json(new { result = result, message = message });
            }
            
            try
            {
                long size = 0;
                var files = Request.Form.Files;
                string ServerPath = "";

                foreach (var file in files)
                {
                
                    var filename = ContentDispositionHeaderValue
                    .Parse(file.ContentDisposition)
                    .FileName
                    .Trim('"');
                    var editfilename = filename.Replace("zip", "htm");
                    ServerPath = "/pagegoldentips/" + editfilename;

                    filename = _webHostEnvironment.WebRootPath + "\\pagegoldentips" + $@"\{filename}";

                    var filePath = _webHostEnvironment.WebRootPath + "\\pagegoldentips";
                    size += file.Length;

                    string startPath = filePath;
                    string zipPath = filePath + "\\" + file.FileName;
                    string extractPath = filePath;



                    using (FileStream fs = System.IO.File.Create(filename))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                    ZipFile.ExtractToDirectory(zipPath, extractPath, true);
                }

                GoldenTip gT = new GoldenTip();

                gT.PageBaseId = PageBaseId;
                gT.PageNumber = goldenTipsPageNumber;
                gT.ServerPath = ServerPath;

                _context.GoldenTips.Add(gT);

                _context.SaveChanges();
                result = true;
            }
            catch (Exception)
            {
                message = "عملیات ذخیره با شکست روبرو شد";
                result = false;
            }

            return Json(new {result=result,message= message });
        }

        [HttpPost]
        public IActionResult UploadFilessConceptualPoints(string id, int conceptualPointsPageNumber)
        {
            Guid PageBaseId = Guid.Parse(id);
            var HasRow = _context.ConceptualPoints.Where(C => C.PageBaseId == PageBaseId && C.PageNumber == conceptualPointsPageNumber).FirstOrDefault();
            string message = "";
            bool result = false;
            if (HasRow != null)
            {
                message = "شماره صفحه تکراری می باشد";
                result = false;
                return Json(new { result = result, message = message });
            }
            try
            {
                long size = 0;
                var files = Request.Form.Files;
                string ServerPath = "";
                foreach (var file in files)
                {
                    var filename = ContentDispositionHeaderValue
                    .Parse(file.ContentDisposition)
                    .FileName
                    .Trim('"');
                    var editfilename = filename.Replace("zip", "htm");
                    ServerPath = "/pageconceptualpoints/" + editfilename;

                    filename = _webHostEnvironment.WebRootPath + "\\pageconceptualpoints" + $@"\{filename}";

                    var filePath = _webHostEnvironment.WebRootPath + "\\pageconceptualpoints";
                    size += file.Length;

                    string startPath = filePath;
                    string zipPath = filePath + "\\" + file.FileName;
                    string extractPath = filePath;



                    using (FileStream fs = System.IO.File.Create(filename))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                    ZipFile.ExtractToDirectory(zipPath, extractPath, true);
                }
                ConceptualPoint cP = new ConceptualPoint();

                cP.PageBaseId = Guid.Parse(id);
                cP.PageNumber = conceptualPointsPageNumber;
                cP.ServerPath = ServerPath;

                _context.ConceptualPoints.Add(cP);

                _context.SaveChanges();

                result = true;
            }
            catch (Exception)
            {
                message = "عملیات ذخیره با شکست روبرو شد";
                result = false;
            }

            return Json(new { result = result, message = message });
        }

        [HttpPost]
        public IActionResult UploadFilessLetterChart(string id, int letterChartPageNumber)
        {
            Guid PageBaseId = Guid.Parse(id);
            var HasRow = _context.LetterCharts.Where(C => C.PageBaseId == PageBaseId && C.PageNumber == letterChartPageNumber).FirstOrDefault();
            string message = "";
            bool result = false;

            if (HasRow != null)
            {
                message = "شماره صفحه تکراری می باشد";
                result = false;
                return Json(new { result = result, message = message });
            }

            try
            {
                long size = 0;
                var files = Request.Form.Files;
                string ServerPath = "";

                foreach (var file in files)
                {
                    var filename = ContentDispositionHeaderValue
                    .Parse(file.ContentDisposition)
                    .FileName
                    .Trim('"');
                    var editfilename = filename.Replace("zip", "htm");
                    ServerPath = "/pageletterchart/" + editfilename;

                    filename = _webHostEnvironment.WebRootPath + "\\pageletterchart" + $@"\{filename}";

                    var filePath = _webHostEnvironment.WebRootPath + "\\pageletterchart";
                    size += file.Length;

                    string startPath = filePath;
                    string zipPath = filePath + "\\" + file.FileName;
                    string extractPath = filePath;



                    using (FileStream fs = System.IO.File.Create(filename))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                    ZipFile.ExtractToDirectory(zipPath, extractPath, true);
                }

                LetterChart lC = new LetterChart();

                lC.PageBaseId = PageBaseId;
                lC.PageNumber = letterChartPageNumber;
                lC.ServerPath = ServerPath;

                _context.LetterCharts.Add(lC);

                _context.SaveChanges();
                result = true;
            }
            catch (Exception)
            {
                message = "عملیات ذخیره با شکست روبرو شد";
                result = false;
            }

            return Json(new { result = result, message = message });
        }

        [HttpPost]
        public IActionResult UploadFilessLetterTable(string id, int letterTablePageNumber)
        {
            Guid PageBaseId = Guid.Parse(id);
            var HasRow = _context.LetterTables.Where(C => C.PageBaseId == PageBaseId && C.PageNumber == letterTablePageNumber).FirstOrDefault();
            string message = "";
            bool result = false;
            if (HasRow != null)
            {
                message = "شماره صفحه تکراری می باشد";
                result = false;
                return Json(new { result = result, message = message });
            }

            try
            {
                long size = 0;
                var files = Request.Form.Files;
                string ServerPath = "";

                foreach (var file in files)
                {
                    var filename = ContentDispositionHeaderValue
                    .Parse(file.ContentDisposition)
                    .FileName
                    .Trim('"');

                    var editfilename = filename.Replace("zip", "htm");
                    ServerPath = "/pagelettertable/" + editfilename;

                    filename = _webHostEnvironment.WebRootPath + "\\pagelettertable" + $@"\{filename}";

                    var filePath = _webHostEnvironment.WebRootPath + "\\pagelettertable";
                    size += file.Length;

                    string startPath = filePath;
                    string zipPath = filePath + "\\" + file.FileName;
                    string extractPath = filePath;



                    using (FileStream fs = System.IO.File.Create(filename))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                    ZipFile.ExtractToDirectory(zipPath, extractPath, true);
                }

                LetterTable lT = new LetterTable();

                lT.PageBaseId = PageBaseId;
                lT.PageNumber = letterTablePageNumber;
                lT.ServerPath = ServerPath;

                _context.LetterTables.Add(lT);

                _context.SaveChanges();
                result = true;
            }
            catch (Exception)
            {
                message = "عملیات ذخیره با شکست روبرو شد";
                result = false;
            }

            return Json(new { result = result, message = message });
        }


        [HttpPost]
        public IActionResult UploadFilesMovie(string id, int moviePageNumber)
        {
            Guid PageBaseId = Guid.Parse(id);
            var HasRow = _context.Movies.Where(C => C.PageBaseId == PageBaseId && C.PageNumber == moviePageNumber).FirstOrDefault();
            string message = "";
            bool result = false;
            if (HasRow != null)
            {
                message = "شماره صفحه تکراری می باشد";
                result = false;
                return Json(new { result = result, message = message });
            }

            try
            {
                long size = 0;
                var files = Request.Form.Files;
                string ServerPath = "";

                foreach (var file in files)
                {
                    var filename = ContentDispositionHeaderValue
                    .Parse(file.ContentDisposition)
                    .FileName
                    .Trim('"');

                    var editfilename = filename.Replace("zip", "mp4");
                    ServerPath = "/pagemovie/" + editfilename;

                    filename = _webHostEnvironment.WebRootPath + "\\pagemovie" + $@"\{filename}";

                    var filePath = _webHostEnvironment.WebRootPath + "\\pagemovie";
                    size += file.Length;

                    string startPath = filePath;
                    string zipPath = filePath + "\\" + file.FileName;
                    string extractPath = filePath;



                    using (FileStream fs = System.IO.File.Create(filename))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                    ZipFile.ExtractToDirectory(zipPath, extractPath, true);
                }

                Movie m = new Movie();

                m.PageBaseId = PageBaseId;
                m.PageNumber = moviePageNumber;
                m.ServerPath = ServerPath;

                _context.Movies.Add(m);

                _context.SaveChanges();
                result = true;
            }
            catch (Exception)
            {
                message = "عملیات ذخیره با شکست روبرو شد";
                result = false;
            }

            return Json(new { result = result, message = message });
        }
        public JsonResult CountPage(int id)
        {
            var countBookPage = _context.PageBases.Where(C => C.BookId == id).ToList();
            int lastPage = 0;

            if (countBookPage.Count>0)
            {
                 lastPage = (countBookPage.Max(C=>C.PageNumber)) + 1;
            }
            else
            {
                lastPage++;
            }

            return Json(new {lastPage= lastPage});
        }

        [HttpPost]
        public IActionResult EditPageBases(int bookId,int pageNumber)
        {
            var bookIdLesson = _context.Books.Where(C => C.Id == bookId).Select(C => C.LessonId).FirstOrDefault();
            var lessonTips = _context.Lessons.Where(C => C.Id == bookIdLesson).FirstOrDefault();

            ViewData["ListTipsCondition"] = lessonTips;

            var pageBase = _context.PageBases.Where(C => C.BookId == bookId && C.PageNumber == pageNumber).FirstOrDefault();
            if (pageBase ==null)
            {
                return Content("صفحه مورد نظر یافت نشد");
            }
           return PartialView("_PartialEditPageBase", pageBase);
        }

        public IActionResult SercheEditPage()
        {
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Name");
            return View();
        }

        [HttpPost]
        public JsonResult SercheEditPage(string id, int SessionNumber)
        {
            Guid PageBaseId = Guid.Parse(id);
            bool result = false;
            string message = "";
         
            PageBase pB = _context.PageBases.Where(C => C.Id == PageBaseId).FirstOrDefault();
            try
            {
                long size = 0;
                var files = Request.Form.Files;
                string ServerPath = "";
                if (files.Count==0)
                {
                    return Json(new { result = result, message = "انتخاب فایل محتوا اجباری می باشد", serverPath = pB.ServerPath });
                }
                foreach (var file in files)
                {

                    var filename = ContentDispositionHeaderValue
                    .Parse(file.ContentDisposition)
                    .FileName
                    .Trim('"');
                    var editfilename = filename.Replace("zip", "htm");
                    ServerPath = "/pagebase/" + editfilename;

                    filename = _webHostEnvironment.WebRootPath + "\\pagebase" + $@"\{filename}";

                    var filePath = _webHostEnvironment.WebRootPath + "\\pagebase";
                    size += file.Length;

                    string startPath = filePath;
                    string zipPath = filePath + "\\" + file.FileName;
                    string extractPath = filePath;



                    using (FileStream fs = System.IO.File.Create(filename))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                    ZipFile.ExtractToDirectory(zipPath, extractPath, true);
                }


                pB.SessionNumber = SessionNumber;
                pB.ServerPath = ServerPath;

                _context.PageBases.Update(pB);

                _context.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                message = "عملیات ذخیره با شکست روبرو شد";
                result = false;
            }

            return Json(new { result = result, message = message,serverPath=pB.ServerPath});
      
        }


        public async Task<IActionResult> ListGoldenTips(Guid id)
        {
            var list = await _context.GoldenTips.Where(C=>C.PageBaseId==id).OrderBy(P => P.PageNumber).ToListAsync();
            ViewBag.PageBaseId = id;
            return View(list);
        }

        [HttpPost]
        public IActionResult UploadEditGoldenTips(string pageBaseId,int id, int goldenTipsPageNumber)
        {
            Guid PageBaseId = Guid.Parse(pageBaseId);
            string message = "";
            bool result = false;
       

            try
            {
                long size = 0;
                var files = Request.Form.Files;
                string ServerPath = "";

                foreach (var file in files)
                {
                    var filename = ContentDispositionHeaderValue
                    .Parse(file.ContentDisposition)
                    .FileName
                    .Trim('"');
                    var editfilename = filename.Replace("zip", "htm");
                    ServerPath = "/pagegoldentips/" + editfilename;
                    filename = _webHostEnvironment.WebRootPath + "\\pagegoldentips" + $@"\{filename}";

                    var filePath = _webHostEnvironment.WebRootPath + "\\pagegoldentips";
                    size += file.Length;

                    string startPath = filePath;
                    string zipPath = filePath + "\\" + file.FileName;
                    string extractPath = filePath;

                    using (FileStream fs = System.IO.File.Create(filename))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                    ZipFile.ExtractToDirectory(zipPath, extractPath, true);
                }

                GoldenTip gT = _context.GoldenTips.Where(C=>C.Id==id).FirstOrDefault();

               
                gT.ServerPath = ServerPath;

                _context.GoldenTips.Update(gT);

                _context.SaveChanges();
                result = true;
            }
            catch (Exception)
            {
                message = "عملیات ذخیره با شکست روبرو شد";
                result = false;
            }

            return Json(new { result = result, message = message });
        }

        /// <summary>
        public async Task<IActionResult> ListConceptualPoints(Guid id)
        {
            var list = await _context.ConceptualPoints.Where(C => C.PageBaseId == id).OrderBy(P => P.PageNumber).ToListAsync();
            ViewBag.PageBaseId = id;
            return View(list);
        }

        [HttpPost]
        public IActionResult UploadEditConceptualPoints(string pageBaseId, int id, int conceptualPointsPageNumber)
        {
            Guid PageBaseId = Guid.Parse(pageBaseId);
            string message = "";
            bool result = false;


            try
            {
                long size = 0;
                var files = Request.Form.Files;
                string ServerPath = "";

                foreach (var file in files)
                {
                    var filename = ContentDispositionHeaderValue
                    .Parse(file.ContentDisposition)
                    .FileName
                    .Trim('"');
                    var editfilename = filename.Replace("zip", "htm");
                    ServerPath = "/pageconceptualpoints/" + editfilename;
                    filename = _webHostEnvironment.WebRootPath + "\\pageconceptualpoints" + $@"\{filename}";

                    var filePath = _webHostEnvironment.WebRootPath + "\\pageconceptualpoints";
                    size += file.Length;

                    string startPath = filePath;
                    string zipPath = filePath + "\\" + file.FileName;
                    string extractPath = filePath;

                    using (FileStream fs = System.IO.File.Create(filename))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                    ZipFile.ExtractToDirectory(zipPath, extractPath, true);
                }

                ConceptualPoint cT = _context.ConceptualPoints.Where(C => C.Id == id).FirstOrDefault();


                cT.ServerPath = ServerPath;

                _context.ConceptualPoints.Update(cT);

                _context.SaveChanges();
                result = true;
            }
            catch (Exception)
            {
                message = "عملیات ذخیره با شکست روبرو شد";
                result = false;
            }

            return Json(new { result = result, message = message });
        }
        /// <returns></returns>


        /// <summary>
        public async Task<IActionResult> ListLetterCharts(Guid id)
        {
            var list = await _context.LetterCharts.Where(C => C.PageBaseId == id).OrderBy(P => P.PageNumber).ToListAsync();
            ViewBag.PageBaseId = id;
            return View(list);
        }

        [HttpPost]
        public IActionResult UploadEditLetterCharts(string pageBaseId, int id, int letterChartsPageNumber)
        {
            Guid PageBaseId = Guid.Parse(pageBaseId);
            string message = "";
            bool result = false;


            try
            {
                long size = 0;
                var files = Request.Form.Files;
                string ServerPath = "";

                foreach (var file in files)
                {
                    var filename = ContentDispositionHeaderValue
                    .Parse(file.ContentDisposition)
                    .FileName
                    .Trim('"');
                    var editfilename = filename.Replace("zip", "htm");
                    ServerPath = "/pageletterchart/" + editfilename;
                    filename = _webHostEnvironment.WebRootPath + "\\pageletterchart" + $@"\{filename}";

                    var filePath = _webHostEnvironment.WebRootPath + "\\pageletterchart";
                    size += file.Length;

                    string startPath = filePath;
                    string zipPath = filePath + "\\" + file.FileName;
                    string extractPath = filePath;

                    using (FileStream fs = System.IO.File.Create(filename))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                    ZipFile.ExtractToDirectory(zipPath, extractPath, true);
                }

                LetterChart lC = _context.LetterCharts.Where(C => C.Id == id).FirstOrDefault();


                lC.ServerPath = ServerPath;

                _context.LetterCharts.Update(lC);

                _context.SaveChanges();
                result = true;
            }
            catch (Exception)
            {
                message = "عملیات ذخیره با شکست روبرو شد";
                result = false;
            }

            return Json(new { result = result, message = message });
        }
        /// <returns></returns>


        /// <summary>
        public async Task<IActionResult> ListLetterTables(Guid id)
        {
            var list = await _context.LetterTables.Where(C => C.PageBaseId == id).OrderBy(P => P.PageNumber).ToListAsync();
            ViewBag.PageBaseId = id;
            return View(list);
        }

        [HttpPost]
        public IActionResult UploadEditLetterTables(string pageBaseId, int id, int letterTablesPageNumber)
        {
            Guid PageBaseId = Guid.Parse(pageBaseId);
            string message = "";
            bool result = false;


            try
            {
                long size = 0;
                var files = Request.Form.Files;
                string ServerPath = "";

                foreach (var file in files)
                {
                    var filename = ContentDispositionHeaderValue
                    .Parse(file.ContentDisposition)
                    .FileName
                    .Trim('"');
                    var editfilename = filename.Replace("zip", "htm");
                    ServerPath = "/pagelettertable/" + editfilename;
                    filename = _webHostEnvironment.WebRootPath + "\\pageLetterTable" + $@"\{filename}";

                    var filePath = _webHostEnvironment.WebRootPath + "\\pageLetterTable";
                    size += file.Length;

                    string startPath = filePath;
                    string zipPath = filePath + "\\" + file.FileName;
                    string extractPath = filePath;

                    using (FileStream fs = System.IO.File.Create(filename))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                    ZipFile.ExtractToDirectory(zipPath, extractPath, true);
                }

                LetterTable lT = _context.LetterTables.Where(C => C.Id == id).FirstOrDefault();


                lT.ServerPath = ServerPath;

                _context.LetterTables.Update(lT);

                _context.SaveChanges();
                result = true;
            }
            catch (Exception)
            {
                message = "عملیات ذخیره با شکست روبرو شد";
                result = false;
            }

            return Json(new { result = result, message = message });
        }
        /// <returns></returns>

        /// <summary>
        public async Task<IActionResult> ListMovies(Guid id)
        {
            var list = await _context.Movies.Where(C => C.PageBaseId == id).OrderBy(P => P.PageNumber).ToListAsync();
            ViewBag.PageBaseId = id;
            return View(list);
        }

        [HttpPost]
        public IActionResult UploadEditMovies(string pageBaseId, int id, int moviesPageNumber)
        {
            Guid PageBaseId = Guid.Parse(pageBaseId);
            string message = "";
            bool result = false;


            try
            {
                long size = 0;
                var files = Request.Form.Files;
                string ServerPath = "";

                foreach (var file in files)
                {
                    var filename = ContentDispositionHeaderValue
                    .Parse(file.ContentDisposition)
                    .FileName
                    .Trim('"');
                    var editfilename = filename.Replace("zip", "htm");
                    ServerPath = "/pagemovie/" + editfilename;
                    filename = _webHostEnvironment.WebRootPath + "\\pagemovie" + $@"\{filename}";

                    var filePath = _webHostEnvironment.WebRootPath + "\\pagemovie";
                    size += file.Length;

                    string startPath = filePath;
                    string zipPath = filePath + "\\" + file.FileName;
                    string extractPath = filePath;

                    using (FileStream fs = System.IO.File.Create(filename))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                    ZipFile.ExtractToDirectory(zipPath, extractPath, true);
                }

                Movie m = _context.Movies.Where(C => C.Id == id).FirstOrDefault();


                m.ServerPath = ServerPath;

                _context.Movies.Update(m);

                _context.SaveChanges();
                result = true;
            }
            catch (Exception)
            {
                message = "عملیات ذخیره با شکست روبرو شد";
                result = false;
            }

            return Json(new { result = result, message = message });
        }
        /// <returns></returns>

        public IActionResult FullUpdatePageBase()
        {
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult FullUpdatePageBase(PageBase pageBase, List<IFormFile> file)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            long size = file.Sum(f => f.Length);

            foreach (var f in file)
            {
                var filePath = Path.Combine(webRootPath + "\\pagebase");
                if (f.Length > 0)
                {
                    string startPath = filePath;
                    string zipPath = filePath +"\\"+f.FileName;
                    string extractPath = filePath;


                    using (var stream = new FileStream(zipPath, FileMode.Create))
                    {
                        f.CopyTo(stream);
                    }

                    //ZipFile.CreateFromDirectory(startPath, zipPath);

                    ZipFile.ExtractToDirectory(zipPath, extractPath,true);
                }
            }
            return View();
        }

        [HttpPost]
        public JsonResult GoldenTipsDisplay(string id)
        {
            Guid PageBaseId = Guid.Parse(id);
            var listGoldenTips = _context.GoldenTips.Where(C => C.PageBaseId == PageBaseId).OrderBy(P=>P.PageNumber).ToList();    

            return Json(listGoldenTips);
        }

        [HttpPost]
        public JsonResult ConceptualPointsDisplay(string id)
        {
            Guid PageBaseId = Guid.Parse(id);
            var listConceptualPoints = _context.ConceptualPoints.Where(C => C.PageBaseId == PageBaseId).OrderBy(P => P.PageNumber).ToList();

            return Json(listConceptualPoints);
        }
        [HttpPost]
        public JsonResult LetterChartDisplay(string id)
        {
            Guid PageBaseId = Guid.Parse(id);
            var listLetterChart = _context.LetterCharts.Where(C => C.PageBaseId == PageBaseId).OrderBy(P => P.PageNumber).ToList();

            return Json(listLetterChart);
        }
        [HttpPost]
        public JsonResult LetterTableDisplay(string id)
        {
            Guid PageBaseId = Guid.Parse(id);
            var listLetterTable = _context.LetterTables.Where(C => C.PageBaseId == PageBaseId).OrderBy(P => P.PageNumber).ToList();

            return Json(listLetterTable);
        }
        [HttpPost]
        public JsonResult MovieDisplay(string id)
        {
            Guid PageBaseId = Guid.Parse(id);
            var listMovie = _context.Movies.Where(C => C.PageBaseId == PageBaseId).OrderBy(P => P.PageNumber).ToList();

            return Json(listMovie);
        }

    }
}
