using GymMembershipManager.Models;

namespace GymMembershipManager.Services
{
    public interface ISerializeStrategy
    {
        string FileFilter { get; }
        string DefaultFileName { get; }
        void Export(List<MemberExportDto> data, string filePath);
        List<MemberExportDto> Import(string filePath);
    }
}