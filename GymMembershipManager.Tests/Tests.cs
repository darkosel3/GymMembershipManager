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
    public class FakeUserRepository : IUserRepository
    {
        private readonly List<User> _users = new();
        public void Add(User user) => _users.Add(user);
        public void Remove(int id) => _users.RemoveAll(u => u.Id == id);
        public void Update(User user) { }
        public List<User> GetAll() => _users;
        public User? GetByUsername(string username) => _users.FirstOrDefault(u => u.Username == username);
    }

    public class LoginViewModelTests
    {
        private static string Hash(string password)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToHexString(bytes).ToLowerInvariant();
        }

        [Fact]
        public void Login_CorrectCredentials_SucceedsAndSetsSession()
        {
            var repo = new FakeUserRepository();
            repo.Add(new User { Id = 1, Username = "admin", PasswordHash = Hash("admin123"), Role = "Manager" });
            var session = new UserSession();
            var vm = new LoginViewModel(repo, session) { Username = "admin", Password = "admin123" };

            vm.LoginCommand.Execute(null);

            Assert.True(vm.IsLoginSuccessful);
            Assert.Equal("admin", session.Username);
            Assert.Equal("Manager", session.Role);
        }

        [Fact]
        public void Login_WrongPassword_FailsAndSetsErrorMessage()
        {
            var repo = new FakeUserRepository();
            repo.Add(new User { Id = 1, Username = "admin", PasswordHash = Hash("admin123"), Role = "Manager" });
            var session = new UserSession();
            var vm = new LoginViewModel(repo, session) { Username = "admin", Password = "wrongpass" };

            vm.LoginCommand.Execute(null);

            Assert.False(vm.IsLoginSuccessful);
            Assert.False(string.IsNullOrEmpty(vm.ErrorMessage));
        }

        [Fact]
        public void Login_UnknownUsername_Fails()
        {
            var repo = new FakeUserRepository();
            var session = new UserSession();
            var vm = new LoginViewModel(repo, session) { Username = "nobody", Password = "whatever" };

            vm.LoginCommand.Execute(null);

            Assert.False(vm.IsLoginSuccessful);
        }
    }

}