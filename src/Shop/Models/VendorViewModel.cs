using System.ComponentModel;
using FluentValidation;
using FluentValidation.Attributes;
using Shop.Domain.Model;

namespace Shop.Models
{
    [Validator(typeof(VendorViewModelValidator))]
    public class VendorViewModel
    {
        public int Id { get; set; }
        [DisplayName("Имя")]
        public string Name { get; set; }
        [DisplayName("ИНН")]
        public string INN { get; set; }
    }

    public class VendorViewModelValidator : AbstractValidator<VendorViewModel>
    {
        public VendorViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(5, 150);
            RuleFor(x => x.INN).Must(x => x != null && (x.Length == 10 || x.Length == 12))
                               .WithMessage("Длина ИНН должна быть 10 или 12");
        }
    }

    public static class VendorViewModelMapingHelpers
    {
        public static Vendor MapToDomainModel(this VendorViewModel model)
        {
            return new Vendor
            {
                Id   = model.Id,
                INN  = model.INN,
                Name = model.Name
            };
        }

        public static VendorViewModel MapToViewModel(this Vendor vendor)
        {
            return new VendorViewModel
            {
                Id   = vendor.Id,
                INN  = vendor.INN,
                Name = vendor.Name
            };
        }
    }
}