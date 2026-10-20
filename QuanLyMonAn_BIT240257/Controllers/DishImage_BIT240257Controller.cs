using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyMonAn_BIT240257.Data;
using QuanLyMonAn_BIT240257.Models;

namespace QuanLyMonAn_BIT240257.Controllers
{
    public class DishImage_BIT240257Controller : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public DishImage_BIT240257Controller(
            ApplicationDbContext context,
            IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetThumbnail(int imageId)
        {
            var selectedImage = await _context.DishImages
                .FirstOrDefaultAsync(i =>
                    i.BIT240257_ImageId == imageId);

            if (selectedImage == null)
            {
                return NotFound();
            }

            var dishImages = await _context.DishImages
                .Where(i => i.BIT240257_DishId ==
                            selectedImage.BIT240257_DishId)
                .ToListAsync();

            foreach (var image in dishImages)
            {
                image.IsThumbnail =
                    image.BIT240257_ImageId == imageId;
            }

            var dish = await _context.Dishes
                .FirstOrDefaultAsync(d =>
                    d.BIT240257_DishId ==
                    selectedImage.BIT240257_DishId);

            if (dish != null)
            {
                dish.BIT240257_ImageUrl =
                    selectedImage.BIT240257_ImageUrl;
            }

            await _context.SaveChangesAsync();

            TempData["Success"] =
                "Thumbnail updated successfully.";

            return RedirectToAction(
                "Index",
                new { dishId = selectedImage.BIT240257_DishId });
        }
        // Hiển thị danh sách ảnh của một món ăn
        public async Task<IActionResult> Index(int dishId)
        {
            var dish = await _context.Dishes
                .Include(d => d.Images)
                .FirstOrDefaultAsync(d => d.BIT240257_DishId == dishId);

            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }

        // Tải nhiều ảnh lên cùng lúc
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(
            int dishId,
            List<IFormFile> imageFiles)
        {
            var dish = await _context.Dishes
                .FirstOrDefaultAsync(d => d.BIT240257_DishId == dishId);

            if (dish == null)
            {
                return NotFound();
            }

            if (imageFiles == null || imageFiles.Count == 0)
            {
                TempData["Error"] = "Vui lòng chọn ít nhất một hình ảnh.";
                return RedirectToAction(nameof(Index), new { dishId });
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            var uploadFolder = Path.Combine(
                _environment.WebRootPath, "images", "dishes");

            Directory.CreateDirectory(uploadFolder);

            foreach (var file in imageFiles)
            {
                if (file.Length == 0 || file.Length > 5 * 1024 * 1024)
                {
                    TempData["Error"] =
                        "Ảnh rỗng hoặc vượt quá 5 MB đã bị bỏ qua.";
                    continue;
                }

                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

                if (!allowedExtensions.Contains(extension))
                {
                    TempData["Error"] =
                        "Chỉ chấp nhận ảnh JPG, JPEG, PNG hoặc WEBP.";
                    continue;
                }

                var fileName = $"{Guid.NewGuid():N}{extension}";
                var filePath = Path.Combine(uploadFolder, fileName);

                await using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var image = new DishImage_BIT240257
                {
                    BIT240257_DishId = dishId,
                    BIT240257_ImageUrl = "/images/dishes/" + fileName
                };

                _context.DishImages.Add(image);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new { dishId });
        }

        // Xóa một hình ảnh
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var image = await _context.DishImages
                .FirstOrDefaultAsync(i => i.BIT240257_ImageId == id);

            if (image == null)
            {
                return NotFound();
            }

            var dishId = image.BIT240257_DishId;
            var relativePath = image.BIT240257_ImageUrl.TrimStart('/')
                .Replace('/', Path.DirectorySeparatorChar);

            var fullPath = Path.Combine(
                _environment.WebRootPath, relativePath);

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            _context.DishImages.Remove(image);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new { dishId });
        }
    }
}