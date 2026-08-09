using CRUD_REST_API.Business.DTOs.BookDto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CRUD_REST_API.Business.Validators.BookValidator
{
    public class BookCreateDtoValidator:AbstractValidator<BookCreateDto>
    {
        public BookCreateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Basliq yazilmalidir!")
                .MaximumLength(100).WithMessage("Kitab adi cox uzundur!");

            RuleFor(x => x.Genre)
                .NotEmpty().WithMessage("Janr mutleq olmalidir!");

            RuleFor(x => x.PublishedYear)
                .GreaterThan(0).WithMessage("Nesr ili duzgun qeyd olunmayib")
                .LessThanOrEqualTo(DateTime.UtcNow.Year)
                .WithMessage($"Nesr ili cari ilden ({DateTime.UtcNow.Year}) boyuk ola bilmez!");

            RuleFor(x => x.AuthorId)
                .GreaterThan(0).WithMessage("Muellif ID-si duzgun secilmelidir!");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Qiymet 0-dan boyuk olmalidir!")
                .When(x => x.Price.HasValue);

            RuleFor(x => x.ImageUrl)
                .MaximumLength(500).WithMessage("Sekil unvani cox uzundur!");
        }
    }
}
