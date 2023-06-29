namespace UserManagement.Web.Models.Users;

public class UserViewModel
{
    public UserListItemViewModel User { get; set; } = new();
    public ViewMode ViewMode { get; set; }
    
}

public enum ViewMode
{
    View,
    Edit,
    Add
}
