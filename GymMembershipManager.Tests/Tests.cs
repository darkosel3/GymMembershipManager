using GymMembershipManager.Models;
using GymMembershipManager.Services;
using GymMembershipManager.ViewModels;
using GymMembershipManager.Data.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace GymMembershipManager.Tests
{
    // Test 1: MembershipFactory
    public class MembershipFactoryTests
    {
        [Fact]
        public void Create_SetsCorrectExpiryDate()
        {
            var factory = new MembershipFactory();
            var type = new MembershipType
            {
                Id = 1,
                Name = "Mesečna",
                Price = 3000,
                DurationInDays = 30
            };
            var startDate = new DateTime(2026, 1, 1);

            var membership = factory.Create(1, type, startDate);

            Assert.Equal(new DateTime(2026, 1, 31), membership.ExpiryDate);
            Assert.Equal(1, membership.MemberId);
            Assert.Equal(1, membership.MembershipTypeId);
            Assert.Equal(startDate, membership.StartDate);
        }

        [Fact]
        public void Create_YearMembership_ExpiresAfter365Days()
        {
            var factory = new MembershipFactory();
            var type = new MembershipType
            {
                Id = 2,
                Name = "Godišnja",
                Price = 30000,
                DurationInDays = 365
            };
            var startDate = new DateTime(2026, 6, 1);

            var membership = factory.Create(5, type, startDate);

            Assert.Equal(new DateTime(2027, 6, 1), membership.ExpiryDate);
        }
    }

    // Test 2: JSON Strategy roundtrip
    public class JsonSerializeStrategyTests
    {
        [Fact]
        public void ExportImport_Roundtrip_PreservesData()
        {
            var strategy = new JsonSerializeStrategy();
            var tempFile = Path.GetTempFileName() + ".json";

            try
            {
                var original = new List<MemberExportDto>
                {
                    new MemberExportDto
                    {
                        FirstName = "Marko",
                        LastName = "Marković",
                        PhoneNumber = "0641234567",
                        BirthDate = new DateTime(1995, 3, 12),
                        DateJoined = new DateTime(2024, 1, 10),
                        Memberships = new List<MembershipExportDto>
                        {
                            new MembershipExportDto
                            {
                                MembershipTypeName = "Mesečna",
                                StartDate = new DateTime(2024, 1, 1),
                                ExpiryDate = new DateTime(2024, 1, 31)
                            }
                        }
                    }
                };

                strategy.Export(original, tempFile);
                var imported = strategy.Import(tempFile);

                Assert.Single(imported);
                Assert.Equal("Marko", imported[0].FirstName);
                Assert.Equal("Marković", imported[0].LastName);
                Assert.Single(imported[0].Memberships);
                Assert.Equal("Mesečna", imported[0].Memberships[0].MembershipTypeName);
            }
            finally
            {
                File.Delete(tempFile);
            }
        }
    }

    // Test 3: Password hash
    public class LoginHashTests
    {
        [Fact]
        public void HashPassword_AdminPassword_MatchesSeedHash()
        {
            var expectedHash = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918";

            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes("admin"));
            var actualHash = Convert.ToHexString(bytes).ToLowerInvariant();

            Assert.Equal(expectedHash, actualHash);
        }

        [Fact]
        public void HashPassword_DifferentPasswords_ProduceDifferentHashes()
        {
            var hash1 = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes("admin"))).ToLowerInvariant();
            var hash2 = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes("password"))).ToLowerInvariant();

            Assert.NotEqual(hash1, hash2);
        }
    }
}