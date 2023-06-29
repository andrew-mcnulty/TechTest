using System.Linq;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Users;

namespace UserManagement.WebMS.Controllers;

[Route("users")]
public class UsersController : Controller
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService) => _userService = userService;

    [HttpGet]
    public ViewResult List()
    {
        var items = _userService.GetAll().Select(p => new UserListItemViewModel
        {
            Id = p.Id,
            Forename = p.Forename,
            Surname = p.Surname,
            Email = p.Email,
            IsActive = p.IsActive,
            DateOfBirth = p.DateOfBirth
        });

        var model = new UserListViewModel
        {
            Items = items.ToList()
        };

        return View(model);
    }

    [HttpGet]
    [Route("{active}")]
    public ViewResult List(bool active)
    {
        var items = _userService.FilterByActive(active).Select(UserToUserListItemViewModel);

        var model = new UserListViewModel
        {
            Items = items.ToList()
        };

        return View(model);
    }

    [HttpGet]
    [Route("/{id}")]
    public ViewResult View(long id, ViewMode viewMode)
    {
        var user = _userService.GetById(id);

        if(user is null)
        {
            return View("Error");
        }

        var model = new UserViewModel()
        {
            User = UserToUserListItemViewModel(user),
            ViewMode = viewMode
        };

        return View(model);
    }

    [HttpPost]
    public ViewResult SaveChange(UserViewModel model)
    {
        if(model is null)
        {
            return View("Error");
        }

        if(model.User.Id is null)
        {
            _userService.AddUser(
                model.User.IsActive,
                model.User.Forename ?? "",
                model.User.Surname ?? "",
                model.User.Email ?? "",
                model.User.DateOfBirth);
        }
        else
        {
            _userService.EditUser(
                model.User.Id.Value,
                model.User.IsActive,
                model.User.Forename ?? "",
                model.User.Surname ?? "",
                model.User.Email ?? "",
                model.User.DateOfBirth);
        }


        return View("View", model);
    }

    private UserListItemViewModel UserToUserListItemViewModel(User user)
    {
        return new UserListItemViewModel
        {
            Id = user.Id,
            Forename = user.Forename,
            Surname = user.Surname,
            Email = user.Email,
            IsActive = user.IsActive,
            DateOfBirth = user.DateOfBirth
        };
    }

}
