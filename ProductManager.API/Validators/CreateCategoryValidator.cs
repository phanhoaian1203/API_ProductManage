using FluentValidation;
using ProductManager.API.DTOs;

namespace ProductManager.API.Validators
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryDTO>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Tên danh mục không được để trống")
                .Length(2, 100).WithMessage("Tên danh mục phải từ 2 đến 100 kí tự");
        }
    }
}
