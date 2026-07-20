using AutoMapper;
using CRUD_REST_API.Business.DTOs.BookDto;
using CRUD_REST_API.Business.Services.Abstractions;
using CRUD_REST_API.Contexts;
using CRUD_REST_API.DataAccess.Repositories.Abstractions;
using CRUD_REST_API.DataAccess.Repositories.Implementations;
using CRUD_REST_API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_REST_API.Business.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        public BookService(IBookRepository bookRepository, IMapper mapper, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _authorRepository = authorRepository;
        }
        public async Task CreateAsync(BookCreateDto CreateBookDto)
        {
            var authorExists = await _authorRepository.GetByIdAsync(CreateBookDto.AuthorId);

            if (authorExists == null)
            {
                throw new Exception($"Gönderilen ID-li ({CreateBookDto.AuthorId}) yaziçi sistemdə movcud deyil.");
            }
            var book = _mapper.Map<Book>(CreateBookDto);
            await _bookRepository.AddAsync(book);
            await _bookRepository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var deletedBook = await _bookRepository.GetByIdAsync(id);
            if (deletedBook == null) return;
            _bookRepository.Delete(deletedBook);
            await _bookRepository.SaveAsync();
        }

        public async Task<IEnumerable<BookGetDto>> GetAllAsync()
        {
            //var books = await _bookRepository.GetAllAsync();
            var books = await _bookRepository.GetAllBooksWithAuthorsAsync();
            return _mapper.Map<IEnumerable<BookGetDto>>(books);
        }

        public async Task<BookGetDto> GetByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book is null) throw new Exception("Kitab tapilmadi!");
            return _mapper.Map<BookGetDto>(book);
        }

        public async Task UpdateAsync(BookUpdateDto UpdateBookDto)
        {
            var existBook = await _bookRepository.GetByIdAsync(UpdateBookDto.Id);
            if(existBook is null) throw new Exception("Movcud kitab tapilmadi!");
            _mapper.Map(UpdateBookDto, existBook);
            _bookRepository.Update(existBook);
            await _bookRepository.SaveAsync();

        }
    }
}
