using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_REST_API.Business.DTOs.MemberDto
{
    public class MemberGetDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime MembershipDate { get; set; }
    }
}
