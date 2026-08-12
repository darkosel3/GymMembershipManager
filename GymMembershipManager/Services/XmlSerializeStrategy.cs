using GymMembershipManager.Models;
using System.IO;
using System.Xml.Serialization;

namespace GymMembershipManager.Services
{
    public class XmlSerializeStrategy : ISerializeStrategy
    {
        public string FileFilter => "XML fajl (*.xml)|*.xml";
        public string DefaultFileName => "clanovi_export";

        public void Export(List<MemberExportDto> data, string filePath)
        {
            var serializer = new XmlSerializer(typeof(List<MemberExportDto>));
            using var stream = File.Create(filePath);
            serializer.Serialize(stream, data);
        }

        public List<MemberExportDto> Import(string filePath)
        {
            var serializer = new XmlSerializer(typeof(List<MemberExportDto>));
            using var stream = File.OpenRead(filePath);
            return serializer.Deserialize(stream) as List<MemberExportDto> ?? new();
        }
    }
}