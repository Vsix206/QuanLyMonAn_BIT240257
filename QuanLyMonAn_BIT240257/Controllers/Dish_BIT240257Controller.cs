
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyMonAn_BIT240257.Data;
using QuanLyMonAn_BIT240257.Models;

namespace QuanLyMonAn_BIT240257.Controllers
{
    public class Dish_BIT240257Controller : Controller
    {
        private readonly ApplicationDbContext _context;

        public Dish_BIT240257Controller(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dish_BIT240257
        // Hiển thị, tìm kiếm và lọc món ăn
        public async Task<IActionResult> Index(
            string? searchString,
            int? categoryId,
            decimal? minPrice,
            decimal? maxPrice)
        {
            var dishes = _context.Dishes
                .Include(d => d.Category)
                .AsQueryable();

            // Tìm kiếm theo tên món ăn
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                dishes = dishes.Where(d =>
                    d.BIT240257_DishName.Contains(searchString));
            }

            // Lọc theo danh mục
            if (categoryId.HasValue)
            {
                dishes = dishes.Where(d =>
                    d.BIT240257_CategoryId == categoryId.Value);
            }

            // Lọc theo giá tối thiểu
            if (minPrice.HasValue)
            {
                dishes = dishes.Where(d =>
                    d.BIT240257_Price >= minPrice.Value);
            }

            // Lọc theo giá tối đa
            if (maxPrice.HasValue)
            {
                dishes = dishes.Where(d =>
                    d.BIT240257_Price <= maxPrice.Value);
            }

            // Giữ lại các điều kiện lọc trên giao diện
            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentCategory = categoryId;
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;

            // Nạp danh mục cho ô lựa chọn
            ViewBag.Categories = await _context.DishCategories
                .OrderBy(c => c.BIT240257_CategoryName)
                .ToListAsync();

            return View(await dishes.ToListAsync());
        }

        // GET: Dish_BIT240257/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dishes
                .Include(d => d.Category)
                .Include(d => d.Images)
                .FirstOrDefaultAsync(d =>
                    d.BIT240257_DishId == id.Value);

            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }

        // GET: Dish_BIT240257/Create
        public IActionResult Create()
        {
            LoadCategories();
            return View();
        }

        // POST: Dish_BIT240257/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("BIT240257_DishId,BIT240257_DishName,BIT240257_Price,BIT240257_Description,BIT240257_ImageUrl,BIT240257_CategoryId")]
            Dish_BIT240257 dish)
        {
            if (ModelState.IsValid)
            {
                _context.Dishes.Add(dish);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            LoadCategories(dish.BIT240257_CategoryId);
            return View(dish);
        }

        // GET: Dish_BIT240257/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dishes.FindAsync(id.Value);

            if (dish == null)
            {
                return NotFound();
            }

            LoadCategories(dish.BIT240257_CategoryId);
            return View(dish);
        }

        // POST: Dish_BIT240257/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("BIT240257_DishId,BIT240257_DishName,BIT240257_Price,BIT240257_Description,BIT240257_ImageUrl,BIT240257_CategoryId")]
            Dish_BIT240257 dish)
        {
            if (id != dish.BIT240257_DishId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dish);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DishExists(dish.BIT240257_DishId))
                    {
                        return NotFound();
                    }

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            LoadCategories(dish.BIT240257_CategoryId);
            return View(dish);
        }

        // GET: Dish_BIT240257/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dishes
                .Include(d => d.Category)
                .FirstOrDefaultAsync(d =>
                    d.BIT240257_DishId == id.Value);

            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }

        // POST: Dish_BIT240257/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dish = await _context.Dishes
                .Include(d => d.Images)
                .FirstOrDefaultAsync(d =>
                    d.BIT240257_DishId == id);

            if (dish != null)
            {
                // Xóa các bản ghi hình ảnh liên quan
                if (dish.Images != null && dish.Images.Any())
                {
                    _context.DishImages.RemoveRange(dish.Images);
                }

                _context.Dishes.Remove(dish);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // Nạp danh mục vào dropdown
        private void LoadCategories(int? selectedCategory = null)
        {
            ViewData["BIT240257_CategoryId"] =
                new SelectList(
                    _context.DishCategories.ToList(),
                    "BIT240257_CategoryId",
                    "BIT240257_CategoryName",
                    selectedCategory);
        }

        // Kiểm tra món ăn có tồn tại hay không
        private bool DishExists(int id)
        {
            return _context.Dishes.Any(e =>
                e.BIT240257_DishId == id);
        }
    }
}
