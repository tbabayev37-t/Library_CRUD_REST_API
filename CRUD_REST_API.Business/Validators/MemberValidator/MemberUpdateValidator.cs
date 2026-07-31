using CRUD_REST_API.Business.DTOs.MemberDto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_REST_API.Business.Validators.MemberValidator
{
    public class MemberUpdateValidator:AbstractValidator<MemberUpdateDto>
    {
        public MemberUpdateValidator()
        {
            RuleFor(x=>x.Id).NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Ad mutleq yazilmalidir!");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Soyad mutleq yazilmalidir!");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email mutleq yazilmalidir!")
                .EmailAddress().WithMessage("Duzgun email unvani daxil edin!");
        }
    }
}
