namespace CRUD_REST_API.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime MembershipDate { get; set; } = DateTime.UtcNow;
    }
}
