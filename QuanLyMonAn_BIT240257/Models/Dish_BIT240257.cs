

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyMonAn_BIT240257.Models
{
    [Table("BIT240257_Dishes")]
    public class Dish_BIT240257
    {
        [Key]
        [Column("BIT240257_DishId")]
        public int BIT240257_DishId { get; set; }

        [Required(ErrorMessage = "Please enter the dish name.")]
        [StringLength(200)]
        [Column("BIT240257_DishName")]
        public string BIT240257_DishName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter the price.")]
        [Range(typeof(decimal), "0.01", "1000000000",
            ErrorMessage = "Price must be greater than 0.")]
        [Column("BIT240257_Price", TypeName = "decimal(18,2)")]
        public decimal BIT240257_Price { get; set; }

        [Column("BIT240257_Description")]
        public string? BIT240257_Description { get; set; }

        [Column("BIT240257_ImageUrl")]
        public string? BIT240257_ImageUrl { get; set; }

        [Required(ErrorMessage = "Please select a category.")]
        [Column("BIT240257_CategoryId")]
        public int BIT240257_CategoryId { get; set; }

        [Range(1, 1440,
            ErrorMessage = "Preparation time must be between 1 and 1440 minutes.")]
        [Column("BIT240257_PreparationTime")]
        public int PreparationTime { get; set; } = 15;

        [Column("BIT240257_IsAvailable")]
        public bool IsAvailable { get; set; } = true;

        [ForeignKey(nameof(BIT240257_CategoryId))]
        public DishCategory_BIT240257? Category { get; set; }

        public ICollection<DishImage_BIT240257> Images { get; set; }
            = new List<DishImage_BIT240257>();
    }
}
