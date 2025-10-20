using EnvanterApp.Application.Features.Commands.Employees;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvanterApp.Application.Validators.Employees
{
    public class AddEmployeeValidator : AbstractValidator<AddEmployeeCommandRequest>
    {
        public AddEmployeeValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("İsim alanı boş bırakılamaz.")
                .MaximumLength(50).WithMessage("İsim alanı en fazla 50 karakter olabilir.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyisim alanı boş bırakılamaz.")
                .MaximumLength(50).WithMessage("Soyisim alanı en fazla 50 karakter olabilir.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email alanı boş bırakılamaz.")
                .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Telefon numarası alanı boş bırakılamaz.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Geçerli bir telefon numarası giriniz.");

            RuleFor(x => x.Department)
                .NotEmpty().WithMessage("Departman alanı boş bırakılamaz.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Ünvan alanı boş bırakılamaz.");

            When(x => x.ProfileImage != null, () =>
            {
                RuleFor(x => x.ProfileImage.Length)
                    .LessThanOrEqualTo(4 * 1024 * 1024)
                    .WithMessage("Profil resmi en fazla 4 MB olabilir.");
            });

        }
    }
}
