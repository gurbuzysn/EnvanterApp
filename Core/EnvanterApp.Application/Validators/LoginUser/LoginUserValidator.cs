using EnvanterApp.Application.Features.Queries.LoginUser;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvanterApp.Application.Validators.LoginUser
{
    public class LoginUserValidator : AbstractValidator<LoginUserQueryRequest>
    {
        public LoginUserValidator()
        {
            RuleFor(x => x.UserName)
                .NotNull()
                .NotEmpty()
                    .WithMessage("E-Mail alanı boş bırakılamaz.")
                .EmailAddress()
                    .WithMessage("Lütfen geçerli bir mail adresi giriniz");
            
            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                    .WithMessage("Şifre alanı boş bırakılamaz.");
        }
    }
}
