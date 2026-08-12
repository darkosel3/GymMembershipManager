using GymMembershipManager.Models;

namespace GymMembershipManager.Services
{
    public interface IExportStrategy
    {
        string FileFilter { get; }
        string DefaultFileName { get; }
        void Export(List<MemberExportDto> data, string filePath);
    }
}