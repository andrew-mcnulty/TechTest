using System;
using System.Collections.Generic;
using System.Linq;
using UserManagement.Data;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Services.Domain.Implementations;

public class UserService : IUserService
{
    private readonly IDataContext _dataAccess;
    public UserService(IDataContext dataAccess) => _dataAccess = dataAccess;


    public IEnumerable<User> GetAll() => _dataAccess.GetAll<User>();
    public IEnumerable<User> FilterByActive(bool isActive) => _dataAccess.GetAll<User>().Where(p => p.IsActive == isActive);
    public User? GetById(long id) => _dataAccess.GetAll<User>().Where(u => u.Id == id).FirstOrDefault();
    public bool EditUser(long id, bool isActive, string forename, string surname, string email, DateTime dob)
    {
        var user = GetById(id);

        if(user == null)
        {
            return false;
        }

        //expand this to capture exception and return error
        try
        {
            user.IsActive = isActive;
            user.Forename = forename;
            user.Surname = surname;
            user.Email = email;
            user.DateOfBirth = dob;

            _dataAccess.Update(user);

            return true;
        }
        catch
        {
            return false;
        }   
    }

    public bool AddUser(bool isActive, string forename, string surname, string email, DateTime dob)
    {
        //expand this to capture exception and return error
        try
        {
            User user = new User
            {
                IsActive = isActive,
                Forename = forename,
                Surname = surname,
                Email = email,
                DateOfBirth = dob,
            };

            _dataAccess.Create(user);

            return true;
        }
        catch
        {
            return false;
        }
    }
}
