using dotnet_example_clean_arch_with_entity_framework.DOTs;
using FluentValidation;

namespace dotnet_example_clean_arch_with_entity_framework.Validators
{
    public class ProductValidator : AbstractValidator<ProductDto>
    {
        public ProductValidator()
        {
            RuleFor(x=> x.Name).NotEmpty();
            RuleFor(x => x.Price).GreaterThan(0);
        }
    }
}
