using GymMembershipManager.Models;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;

namespace GymMembershipManager.Services
{
    public class SerializationService : ISerializationService
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true
        };

        public void ExportJson(List<MemberExportDto> data, string filePath)
        {
            var json = JsonSerializer.Serialize(data, JsonOptions);
            File.WriteAllText(filePath, json);
        }

        public List<MemberExportDto> ImportJson(string filePath)
        {
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<MemberExportDto>>(json) ?? new();
        }

        public void ExportXml(List<MemberExportDto> data, string filePath)
        {
            var serializer = new XmlSerializer(typeof(List<MemberExportDto>));
            using var stream = File.Create(filePath);
            serializer.Serialize(stream, data);
        }

        public List<MemberExportDto> ImportXml(string filePath)
        {
            var serializer = new XmlSerializer(typeof(List<MemberExportDto>));
            using var stream = File.OpenRead(filePath);
            return serializer.Deserialize(stream) as List<MemberExportDto> ?? new();
        }
    }
}