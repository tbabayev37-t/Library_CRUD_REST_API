using AutoMapper;
using CRUD_REST_API.Business.DTOs.BookDto;
using CRUD_REST_API.Business.DTOs.QueryDto;
using CRUD_REST_API.Business.Services.Abstractions;
using CRUD_REST_API.Contexts;
using CRUD_REST_API.DataAccess.Repositories.Abstractions;
using CRUD_REST_API.DataAccess.Repositories.Implementations;
using CRUD_REST_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly IMemoryCache _cache;
        public BookService(IBookRepository bookRepository, IMapper mapper, IAuthorRepository authorRepository, AppDbContext context,IMemoryCache cache)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _authorRepository = authorRepository;
            _context = context;
            _cache = cache;
        }

        public async Task CreateAsync(BookCreateDto CreateBookDto)
        {
            var authorExists = await _authorRepository.GetByIdAsync(CreateBookDto.AuthorId);

            if (authorExists == null)
            {
                throw new KeyNotFoundException($"Gönderilen ID-li ({CreateBookDto.AuthorId}) yaziçi sistemdə movcud deyil.");
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
            string cachedKey = $"book_{id}";
            if(_cache.TryGetValue(cachedKey, out BookGetDto bookGetDto))
            {
                return bookGetDto;
            }
            var book = await _bookRepository.GetByIdAsync(id);
            if (book is null) throw new KeyNotFoundException("Kitab tapilmadi!");

            var bookDto = _mapper.Map<BookGetDto>(book);

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
                .SetSlidingExpiration(TimeSpan.FromMinutes(2));

            _cache.Set(cachedKey,bookDto, cacheOptions);
            return bookDto;
        }

        public async Task UpdateAsync(BookUpdateDto UpdateBookDto)
        {
            var existBook = await _bookRepository.GetByIdAsync(UpdateBookDto.Id);
            if (existBook is null) throw new KeyNotFoundException("Kitab tapilmadi!");
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
                    Genre = dto.Genre,
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
