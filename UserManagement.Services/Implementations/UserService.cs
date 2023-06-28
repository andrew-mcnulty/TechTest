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
}
