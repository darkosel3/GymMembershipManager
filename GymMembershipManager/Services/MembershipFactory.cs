namespace GymMembershipManager.Services
{
    public interface IMembershipFactory
    {
        Models.Membership Create(int memberId, Models.MembershipType type, DateTime startDate);
    }

    public class MembershipFactory : IMembershipFactory
    {
        public Models.Membership Create(int memberId, Models.MembershipType type, DateTime startDate)
        {
            return new Models.Membership
            {
                MemberId = memberId,
                MembershipTypeId = type.Id,
                StartDate = startDate,
                ExpiryDate = startDate.AddDays(type.DurationInDays)
            };
        }
    }
}