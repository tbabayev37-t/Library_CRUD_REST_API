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
            // Arrange
            int bookId = 1;
            var fakeBook = new Book { Id = bookId, Title = "Test Kitab", Genre = "Badii", PublishedYear = 2020 };
            var fakeDto = new BookGetDto { Id = bookId, Title = "Test Kitab", Genre = "Badii", PublishedYear = 2020 };

            _mockBookRepo.Setup(repo => repo.GetByIdAsync(bookId)).ReturnsAsync(fakeBook);
            _mockMapper.Setup(m => m.Map<BookGetDto>(fakeBook)).Returns(fakeDto);

            // Act
            var result = await _bookService.GetByIdAsync(bookId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(bookId, result.Id);
            Assert.Equal("Test Kitab", result.Title);
        }

        // EXCEPTION SSENARİSİ
        [Fact]
        public async Task GetByIdAsync_WhenBookDoesNotExist_ThrowsKeyNotFoundException()
        {
            // Arrange
            int bookId = 99; // Baza-da olmayan ID
            _mockBookRepo.Setup(repo => repo.GetByIdAsync(bookId)).ReturnsAsync((Book)null!);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _bookService.GetByIdAsync(bookId));
        }

        //  EXCEPTION SSENARİSİ: Yeni kitab yaradılarkən Author tapılmadıqda
        [Fact]
        public async Task CreateAsync_WhenAuthorDoesNotExist_ThrowsKeyNotFoundException()
        {
            // Arrange
            var createDto = new BookCreateDto { Title = "Yeni Kitab", AuthorId = 99 };
            _mockAuthorRepo.Setup(repo => repo.GetByIdAsync(createDto.AuthorId)).ReturnsAsync((Author)null!);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _bookService.CreateAsync(createDto));
        }

        //  EXCEPTION SSENARİSİ: Yenilənəcək kitab tapılmadıqda 
        [Fact]
        public async Task UpdateAsync_WhenBookDoesNotExist_ThrowsKeyNotFoundException()
        {
            // Arrange
            var updateDto = new BookUpdateDto { Id = 99, Title = "Yenilənmiş Kitab" };
            _mockBookRepo.Setup(repo => repo.GetByIdAsync(updateDto.Id)).ReturnsAsync((Book)null!);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _bookService.UpdateAsync(updateDto));
        }
    }
}
