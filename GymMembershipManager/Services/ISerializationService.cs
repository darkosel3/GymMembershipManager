using GymMembershipManager.Models;

namespace GymMembershipManager.Services
{
    public interface ISerializationService
    {
        void ExportJson(List<MemberExportDto> data, string filePath);
        List<MemberExportDto> ImportJson(string filePath);
        void ExportXml(List<MemberExportDto> data, string filePath);
        List<MemberExportDto> ImportXml(string filePath);
    }
}