using GymMembershipManager.Models;
using System.IO;
using System.Text.Json;

namespace GymMembershipManager.Services
{
    public class JsonSerializeStrategy : ISerializeStrategy
    {
        public string FileFilter => "JSON fajl (*.json)|*.json";
        public string DefaultFileName => "clanovi_export";

        public void Export(List<MemberExportDto> data, string filePath)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(filePath, JsonSerializer.Serialize(data, options));
        }

        public List<MemberExportDto> Import(string filePath)
        {
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<MemberExportDto>>(json) ?? new();
        }
    }
}