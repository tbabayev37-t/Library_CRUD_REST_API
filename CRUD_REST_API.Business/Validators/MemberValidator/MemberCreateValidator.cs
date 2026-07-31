using CRUD_REST_API.Business.DTOs.MemberDto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_REST_API.Business.Validators.MemberValidator
{
    public class MemberCreateValidator:AbstractValidator<MemberCreateDto>
    {
        public MemberCreateValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Ad mutleq yazilmalidir!");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Soyad mutleq yazilmalidir!");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email mutleq yazilmalidir!")
                .EmailAddress().WithMessage("Duzgun email unvani daxil edin!");

        }
    }
}
