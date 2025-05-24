namespace HospitalManagementSystem.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }
        public DateTime DateOfJoining { get; set; }
        public int HospitalId { get; set; }
        public string UserId { get; set; }
        public List<HospitalViewModel> Hospitals { get; set; } = new List<HospitalViewModel>();

    }
}
