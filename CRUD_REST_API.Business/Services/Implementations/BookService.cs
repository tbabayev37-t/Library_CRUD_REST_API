using AutoMapper;
using CRUD_REST_API.Business.DTOs.BookDto;
using CRUD_REST_API.Business.DTOs.QueryDto;
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
        private readonly AppDbContext _context;
        public BookService(IBookRepository bookRepository, IMapper mapper, IAuthorRepository authorRepository, AppDbContext context)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _authorRepository = authorRepository;
            _context = context;
        }

        public async Task CreateAsync(BookCreateDto CreateBookDto)
        {

            var authorExists = await _authorRepository.GetByIdAsync(CreateBookDto.AuthorId);

            if (authorExists == null)
            {
                throw new KeyNotFoundException($"Gönderilen ID-li ({CreateBookDto.AuthorId}) yaziçi sistemdə movcud deyil.");
            }
            if (CreateBookDto.CategoryIds != null && CreateBookDto.CategoryIds.Any())
            {
                var existingCategoriesCount = await _context.Categories
                    .Where(c => CreateBookDto.CategoryIds.Contains(c.Id))
                    .CountAsync();

                if (existingCategoriesCount != CreateBookDto.CategoryIds.Distinct().Count())
                {
                    throw new KeyNotFoundException("Daxil edilən kateqoriyalardan biri və ya bir neçəsi bazada tapılmadı!");
                }
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

        public async Task<PagedResultDto<BookGetDto>> GetAllAsync(BookQueryParameters queryParams)
        {
            var (books, totalCount) = await _bookRepository.GetAllBooksWithAuthorsAsync(
                queryParams.PageNumber,
                queryParams.PageSize,
                queryParams.SortBy,
                queryParams.IsDescending,
                queryParams.SearchTerm,
                queryParams.MinPrice,
                queryParams.MaxPrice
            );

            var bookDtos = _mapper.Map<IEnumerable<BookGetDto>>(books);

            return new PagedResultDto<BookGetDto>
            {
                Items = bookDtos,
                TotalCount = totalCount,
                PageNumber = queryParams.PageNumber,
                PageSize = queryParams.PageSize
            };
        }

        public async Task<BookGetDto> GetByIdAsync(int id)
        {
            var book = await _bookRepository.GetBookWithDetailsAsync(id);

            if (book is null) throw new KeyNotFoundException("Kitab tapilmadi!");

            return _mapper.Map<BookGetDto>(book);
        }

        public async Task UpdateAsync(BookUpdateDto UpdateBookDto)
        {
            var existBook = await _bookRepository.GetByIdAsync(UpdateBookDto.Id);
            if (existBook is null) throw new KeyNotFoundException("Kitab tapilmadi!");
            if (UpdateBookDto.CategoryIds != null && UpdateBookDto.CategoryIds.Any())
            {
                var existingCategoriesCount = await _context.Categories
                    .Where(c => UpdateBookDto.CategoryIds.Contains(c.Id))
                    .CountAsync();

                if (existingCategoriesCount != UpdateBookDto.CategoryIds.Distinct().Count())
                {
                    throw new KeyNotFoundException("Daxil edilən kateqoriyalardan biri və ya bir neçəsi bazada tapılmadı!");
                }
            }
            _mapper.Map(UpdateBookDto, existBook);
            _bookRepository.Update(existBook);
            await _bookRepository.SaveAsync();
        }
        public async Task<bool> CreateBookWithAuthorLogAsync(BookCreateDto dto)
        {
            var author = await _context.Authors.FindAsync(dto.AuthorId);
            if (author == null)
            {
                throw new KeyNotFoundException($"Göndərilən ID-li ({dto.AuthorId}) müəllif sistemdə mövcud deyil.");
            }
            // Transaction-i basladiriq
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // 1-ci cedvele yazma: Yeni Book yaradilir
                var book = new Book
                {
                    Title = dto.Title,
                    PublishedYear = dto.PublishedYear,
                    Price = dto.Price,
                    AuthorId = dto.AuthorId
                };
                await _context.Books.AddAsync(book);
                await _context.SaveChangesAsync();

                // 2-ci cedvele yazma: Author məlumati yenilenir

                author.Name = author.Name.Trim();
                _context.Authors.Update(author);
                await _context.SaveChangesAsync();

                // Her iki cedvele yazma ugurludursa transaction tesdiqlenir
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                // Iki emeliyatdan birinde xeta olarsa, butun desyishiklikler geri alinir
                await transaction.RollbackAsync();
                throw;
            }
        }

    }
}
