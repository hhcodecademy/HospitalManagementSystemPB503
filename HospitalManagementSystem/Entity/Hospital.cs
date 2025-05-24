using HospitalManagementSystem.Entity.Common;

namespace HospitalManagementSystem.Entity
{
    public class Hospital : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();


    }
}
