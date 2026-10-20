using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyMonAn_BIT240257.Data;
using QuanLyMonAn_BIT240257.Models;

namespace QuanLyMonAn_BIT240257.Controllers
{
    public class DishCategory_BIT240257Controller : Controller
    {
        private readonly ApplicationDbContext _context;

        public DishCategory_BIT240257Controller(
            ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DishCategory_BIT240257
        public async Task<IActionResult> Index()
        {
            var categories = await _context.DishCategories
                .Include(c => c.Dishes)
                .ToListAsync();

            return View(categories);
        }

        // GET: DishCategory_BIT240257/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.DishCategories
                .Include(c => c.Dishes)
                .FirstOrDefaultAsync(
                    c => c.BIT240257_CategoryId == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: DishCategory_BIT240257/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DishCategory_BIT240257/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("BIT240257_CategoryId,BIT240257_CategoryName,BIT240257_Description")]
            DishCategory_BIT240257 category)
        {
            if (ModelState.IsValid)
            {
                _context.DishCategories.Add(category);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        // GET: DishCategory_BIT240257/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.DishCategories
                .FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: DishCategory_BIT240257/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("BIT240257_CategoryId,BIT240257_CategoryName,BIT240257_Description")]
            DishCategory_BIT240257 category)
        {
            if (id != category.BIT240257_CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.DishCategories.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.BIT240257_CategoryId))
                    {
                        return NotFound();
                    }

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        // GET: DishCategory_BIT240257/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.DishCategories
                .FirstOrDefaultAsync(
                    c => c.BIT240257_CategoryId == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: DishCategory_BIT240257/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.DishCategories
                .Include(c => c.Dishes)
                .FirstOrDefaultAsync(
                    c => c.BIT240257_CategoryId == id);

            if (category == null)
            {
                return NotFound();
            }

            if (category.Dishes != null &&
                category.Dishes.Any())
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Không thể xóa danh mục đang có món ăn. " +
                    "Hãy chuyển các món ăn sang danh mục khác trước.");

                return View("Delete", category);
            }

            _context.DishCategories.Remove(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.DishCategories.Any(
                c => c.BIT240257_CategoryId == id);
        }
    }
}