using CRUD_REST_API.Business.DTOs.AuthorDto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_REST_API.Business.Validators.AuthorValidator
{
    public class AuthorCreateValidator:AbstractValidator<AuthorCreateDto>
    {
        public AuthorCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad bos ola  bilmez!")
                .MaximumLength(50);

            RuleFor(x => x.Biography).NotEmpty().WithMessage("Bos ola bilmez!")
                .MaximumLength(1000).WithMessage("Bioqrafiya 1000 simvoldan cox ola bilmez!");

        }
    }
}
