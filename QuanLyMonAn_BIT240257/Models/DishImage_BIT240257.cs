
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyMonAn_BIT240257.Models
{
    [Table("BIT240257_DishImages")]
    public class DishImage_BIT240257
    {
        [Key]
        [Column("BIT240257_ImageId")]
        public int BIT240257_ImageId { get; set; }

        [Required]
        [Column("BIT240257_DishId")]
        public int BIT240257_DishId { get; set; }

        [Required]
        [Column("BIT240257_ImageUrl")]
        public string BIT240257_ImageUrl { get; set; } = string.Empty;

        // Đánh dấu ảnh đại diện của món ăn
        [Column("BIT240257_IsThumbnail")]
        public bool IsThumbnail { get; set; } = false;

        [ForeignKey(nameof(BIT240257_DishId))]
        public virtual Dish_BIT240257? Dish { get; set; }
    }
}
