using HospitalManagementSystem.Entity.Common;

namespace HospitalManagementSystem.Entity
{
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Position { get; set; }
        public decimal Salary { get; set; }
        public DateTime DateOfJoining { get; set; }
        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; } // Navigation property to Hospital entity

        public string UserId { get; set; }
        public AppUser User { get; set; }

    }
}
