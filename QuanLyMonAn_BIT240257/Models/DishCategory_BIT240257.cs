
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyMonAn_BIT240257.Models
{
    [Table("BIT240257_DishCategories")]
    public class DishCategory_BIT240257
    {
        [Key]
        [Column("BIT240257_CategoryId")]
        public int BIT240257_CategoryId { get; set; }

        [Required]
        [StringLength(100)]
        [Column("BIT240257_CategoryName")]
        public string BIT240257_CategoryName { get; set; } = string.Empty;

        [Column("BIT240257_Description")]
        public string? BIT240257_Description { get; set; }

        public ICollection<Dish_BIT240257> Dishes { get; set; }
            = new List<Dish_BIT240257>();
    }
}

