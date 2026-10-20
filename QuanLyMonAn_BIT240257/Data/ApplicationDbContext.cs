
using Microsoft.EntityFrameworkCore;
using QuanLyMonAn_BIT240257.Models;

namespace QuanLyMonAn_BIT240257.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Dish_BIT240257> Dishes { get; set; }

        public DbSet<DishCategory_BIT240257> DishCategories { get; set; }

        public DbSet<DishImage_BIT240257> DishImages { get; set; }
    }
}
