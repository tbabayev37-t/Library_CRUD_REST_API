using AutoMapper;
using CRUD_REST_API.Business.DTOs.MemberDto;
using CRUD_REST_API.Business.Services.Abstractions;
using CRUD_REST_API.DataAccess.Repositories.Abstractions;
using CRUD_REST_API.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_REST_API.Business.Services.Implementations
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;
        public MemberService(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }
        public async Task CreateAsync(MemberCreateDto MemberCreateDto)
        {
            var member =  _mapper.Map<Member>(MemberCreateDto);
            await _memberRepository.AddAsync(member);
            await _memberRepository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var deletedMember = await _memberRepository.GetByIdAsync(id);
            if (deletedMember == null) throw new Exception("Member movcud deyil");
            _memberRepository.Delete(deletedMember);
            await _memberRepository.SaveAsync();
        }

        public async Task<IEnumerable<MemberGetDto>> GetAllAsync()
        {
            var members = await _memberRepository.GetAllAsync();
            return  _mapper.Map<IEnumerable<MemberGetDto>>(members);
        }

        public async Task<MemberGetDto> GetByIdAsync(int id)
        {
            var findingMember = await _memberRepository.GetByIdAsync(id);
            if (findingMember == null) throw new Exception("Member tapilmadi!");
            return _mapper.Map<MemberGetDto>(findingMember);
        }

        public async Task UpdateAsync(MemberUpdateDto UpdateMemberDto)
        {
            var updatedMember = await _memberRepository.GetByIdAsync(UpdateMemberDto.Id);
            if (updatedMember == null) throw new Exception("Member tapilmadi");
            _mapper.Map(UpdateMemberDto,updatedMember);
            _memberRepository.Update(updatedMember);
            await _memberRepository.SaveAsync();
        }
    }
}
