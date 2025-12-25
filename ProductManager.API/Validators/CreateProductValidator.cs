using FluentValidation;
using ProductManager.API.DTOs;

namespace ProductManager.API.Validators
{
    public class CreateProductValidator : AbstractValidator<CreateProductDTO>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Tên sản phẩm không được để trống")
                .Length(3, 100).WithMessage("Tên sản phẩm phải từ 3 đến 100 ký tự");

            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Giá sản phẩm phải lớn hơn 0");

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("Số lượng tồn kho không được âm");

        }
    }
}
