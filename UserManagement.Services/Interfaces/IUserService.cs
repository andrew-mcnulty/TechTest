using System;
using System.Collections.Generic;
using UserManagement.Models;

namespace UserManagement.Services.Domain.Interfaces;

public interface IUserService 
{
    /// <summary>
    /// Return users by active state
    /// </summary>
    /// <param name="isActive"></param>
    /// <returns></returns>
    IEnumerable<User> FilterByActive(bool isActive);
    IEnumerable<User> GetAll();
    User? GetById(long id);
    bool EditUser(long id, bool isActive, string forename, string surname, string email, DateTime dob);
    bool AddUser(bool isActive, string forename, string surname, string email, DateTime dob);
}
