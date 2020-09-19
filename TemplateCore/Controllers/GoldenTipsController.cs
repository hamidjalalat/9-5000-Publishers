using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Main.Controllers
{
    public class GoldenTipsController : Controller
    {
        private readonly DatabaseContext _context;

        public GoldenTipsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: GoldenTips
        public async Task<IActionResult> Index()
        {
            return View(await _context.GoldenTips.ToListAsync());
        }

        // GET: GoldenTips/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goldenTip = await _context.GoldenTips
                .FirstOrDefaultAsync(m => m.Id == id);
            if (goldenTip == null)
            {
                return NotFound();
            }

            return View(goldenTip);
        }

        // GET: GoldenTips/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GoldenTips/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PageNumber,ServerPath,PageBaseId,Id")] GoldenTip goldenTip)
        {
            if (ModelState.IsValid)
            {
                _context.Add(goldenTip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(goldenTip);
        }

        // GET: GoldenTips/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goldenTip = await _context.GoldenTips.FindAsync(id);
            if (goldenTip == null)
            {
                return NotFound();
            }
            return View(goldenTip);
        }

        // POST: GoldenTips/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PageNumber,ServerPath,PageBaseId,Id")] GoldenTip goldenTip)
        {
            if (id != goldenTip.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(goldenTip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GoldenTipExists(goldenTip.Id))
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
            return View(goldenTip);
        }

        // GET: GoldenTips/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goldenTip = await _context.GoldenTips
                .FirstOrDefaultAsync(m => m.Id == id);
            if (goldenTip == null)
            {
                return NotFound();
            }

            return View(goldenTip);
        }

        // POST: GoldenTips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var goldenTip = await _context.GoldenTips.FindAsync(id);
            _context.GoldenTips.Remove(goldenTip);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GoldenTipExists(int id)
        {
            return _context.GoldenTips.Any(e => e.Id == id);
        }
    }
}
