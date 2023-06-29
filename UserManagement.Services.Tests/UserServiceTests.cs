using System;
using System.Linq;
using UserManagement.Models;
using UserManagement.Services.Domain.Implementations;

namespace UserManagement.Data.Tests;

public class UserServiceTests
{
    [Fact]
    public void GetAll_WhenContextReturnsEntities_MustReturnSameEntities()
    {
        // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
        var service = CreateService();
        var users = SetupUsers();

        // Act: Invokes the method under test with the arranged parameters.
        var result = service.GetAll();

        // Assert: Verifies that the action of the method under test behaves as expected.
        result.Should().BeEquivalentTo(users);
    }

    [Fact]
    public void GetActive_WhenContextReturnsEntities_MustReturnSameEntities()
    {
        // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
        var service = CreateService();
        var users = SetupUsers();

        // Act: Invokes the method under test with the arranged parameters.
        var result = service.FilterByActive(true).ToArray().AsQueryable();

        // Assert: Verifies that the action of the method under test behaves as expected.
        result.Should().BeEquivalentTo(users.Where(p => p.IsActive));
    }

    [Fact]
    public void GetInactive_WhenContextReturnsEntities_MustReturnSameEntities()
    {
        // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
        var service = CreateService();
        var users = SetupUsers();

        // Act: Invokes the method under test with the arranged parameters.
        var result = service.FilterByActive(false);

        // Assert: Verifies that the action of the method under test behaves as expected.
        result.Should().BeEquivalentTo(users.Where(p => !p.IsActive));
    }

    [Fact]
    public void AddUser_WhenContextReturnsEntities_MustReturnSameEntities()
    {
        // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
        var service = CreateService();
        var users = SetupUsers();

        // Act: Invokes the method under test with the arranged parameters.
        var result = service.AddUser(false, "John", "Doe", "tester@test.com", DateTime.Today);
        var result2 = service.AddUser(true, "Jane", "Doe", "tester@test2.com", DateTime.Today);

        // Assert: Verifies that the action of the method under test behaves as expected.
        result.Should().BeTrue();
        result2.Should().BeTrue();
        service.GetAll().Should().ContainEquivalentOf(new User { Id = 1L, IsActive=false, Forename = "John", Surname = "Doe", Email = "tester@test.com", DateOfBirth = DateTime.Today });
        service.GetAll().Should().ContainEquivalentOf(new User { Id = 2L, IsActive=true, Forename = "Jane", Surname = "Doe", Email = "tester@test2.com", DateOfBirth = DateTime.Today });

    }

    private IQueryable<User> SetupUsers(string forename = "Johnny", string surname = "User", string email = "juser@example.com", bool isActive = true)
    {
        var users = new[]
        {
            new User
            {
                Id = 0L,
                Forename = forename,
                Surname = surname,
                Email = email,
                IsActive = isActive,
                DateOfBirth = DateTime.Now
            }
        }.AsQueryable();

        _dataContext
            .Setup(s => s.GetAll<User>())
            .Returns(users);

        return users;
    }

    private readonly Mock<IDataContext> _dataContext = new();
    private UserService CreateService() => new(_dataContext.Object);
}
