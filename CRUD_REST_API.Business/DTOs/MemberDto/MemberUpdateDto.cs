using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRUD_REST_API.Business.DTOs.MemberDto
{
    public class MemberUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Ad mutleq yazilmalidir!")]
        public string FirstName { get; set; } = null!;
        [Required(ErrorMessage = "Soyad mutleq yazilmalidir!")]
        public string LastName { get; set; } = null!;
        [Required(ErrorMessage = "Email mutleq yazilmalidir!")]
        [EmailAddress(ErrorMessage = "Duzgun email unvani daxil edin!")]
        public string Email { get; set; } = null!;
    }
}
