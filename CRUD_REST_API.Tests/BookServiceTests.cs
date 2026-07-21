using AutoMapper;
using CRUD_REST_API.Business.DTOs.BookDto;
using CRUD_REST_API.Business.Services.Implementations;
using CRUD_REST_API.DataAccess.Repositories.Abstractions;
using CRUD_REST_API.Models;
using Moq;

namespace CRUD_REST_API.Tests
{
    public class BookServiceTests
    {
        private readonly Mock<IBookRepository> _mockBookRepo;
        private readonly Mock<IAuthorRepository> _mockAuthorRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly BookService _bookService;

        public BookServiceTests()
        {
            _mockBookRepo = new Mock<IBookRepository>();
            _mockAuthorRepo = new Mock<IAuthorRepository>();
            _mockMapper = new Mock<IMapper>();

            _bookService = new BookService(_mockBookRepo.Object, _mockMapper.Object, _mockAuthorRepo.Object);
        }

        [Fact]
        public async Task GetByIdAsync_WhenBookExists_ReturnsBookGetDto()
        {
            int bookId = 1;
            var fakeBook = new Book { Id = bookId, Title = "Test Kitab", Genre = "Badii", PublishedYear = 2020 };
            var fakeDto = new BookGetDto { Id = bookId, Title = "Test Kitab", Genre = "Badii", PublishedYear = 2020 };

            _mockBookRepo.Setup(repo => repo.GetByIdAsync(bookId)).ReturnsAsync(fakeBook);
            _mockMapper.Setup(m => m.Map<BookGetDto>(fakeBook)).Returns(fakeDto);

            var result = await _bookService.GetByIdAsync(bookId);

            Assert.NotNull(result);
            Assert.Equal(bookId, result.Id);
            Assert.Equal("Test Kitab", result.Title);
        }
    }
}
