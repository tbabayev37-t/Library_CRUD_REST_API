using AutoMapper;
using CRUD_REST_API.Business.DTOs.AuthorDto;
using CRUD_REST_API.Business.Services.Abstractions;
using CRUD_REST_API.DataAccess.Repositories.Abstractions;
using CRUD_REST_API.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_REST_API.Business.Services.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }
        public async Task CreateAsync(AuthorCreateDto AuthorCreateDto)
        {
            var author = _mapper.Map<Author>(AuthorCreateDto);
            if(author == null)
            {
                throw new Exception("Invalid input");
            }
            await _authorRepository.AddAsync(author);
            await _authorRepository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var deletedAuthor = await _authorRepository.GetByIdAsync(id);
            if (deletedAuthor == null) throw new Exception("Muellif tapilmadi!");
            _authorRepository.Delete(deletedAuthor);
            await _authorRepository.SaveAsync();
        }

        public async Task<IEnumerable<AuhtorGetDto>> GetAllAsync()
        {
            var authors = await _authorRepository.GetAllAuthorsWithBooksAsync();
            return _mapper.Map<IEnumerable<AuhtorGetDto>>(authors);
        }

        public async Task<AuhtorGetDto> GetByIdAsync(int id)
        {
            var FindingAuthor = await _authorRepository.GetByIdAsync(id);
            if (FindingAuthor == null) throw new Exception("Bu muellif tapilmadi!");
            return _mapper.Map<AuhtorGetDto>(FindingAuthor);
        }

        public async Task UpdateAsync(AuthorUpdateDto AuthorUpdateDto)
        {
            var updatedAuthor = await _authorRepository.GetByIdAsync(AuthorUpdateDto.Id);
            if (updatedAuthor == null) throw new Exception("Muellif tapilmadi!");
            _mapper.Map(AuthorUpdateDto, updatedAuthor);
            _authorRepository.Update(updatedAuthor);
            await _authorRepository.SaveAsync();

        }
    }
}
