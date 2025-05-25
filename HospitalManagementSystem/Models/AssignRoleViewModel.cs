namespace HospitalManagementSystem.Models
{
    public class AssignRoleViewModel
    {
        public string UserId { get; set; }
        public string RoleName { get; set; }
        public List<UserViewModel> Users { get; set; } = new List<UserViewModel>();
        public List<RoleViewModel> Roles { get; set; } = new List<RoleViewModel>();
    }
}
