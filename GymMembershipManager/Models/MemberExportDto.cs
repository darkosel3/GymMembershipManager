namespace GymMembershipManager.Models
{
    public class MemberExportDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public DateTime DateJoined { get; set; }
        public List<MembershipExportDto> Memberships { get; set; } = new();
    }

    public class MembershipExportDto
    {
        public string MembershipTypeName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}