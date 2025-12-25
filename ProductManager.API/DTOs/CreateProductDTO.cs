using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.ComponentModel.DataAnnotations;

namespace ProductManager.API.DTOs
{
    public class CreateProductDTO
    {
        [Required(ErrorMessage ="Tên sản phẩm là bắt buộc")]
        [MaxLength(200)]
        public string Name { get; set; }
        [Range(0, double.MaxValue, ErrorMessage ="Giá sản phẩm phải lớn hơn 0")]
        public decimal Price { get; set; }
        [Range(0, int.MaxValue, ErrorMessage ="Số lượng tòn kho không dược nhỏ hơn 0")]
        public int Stock { get; set; }
    }
}
