using GymMembershipManager.Models;

namespace GymMembershipManager.Services
{
    public interface IPdfReportService
    {
        void GenerateReport(List<Member> members, string filePath);
    }
}