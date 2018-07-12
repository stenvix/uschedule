using System;
using USchedule.Core.Entities.Implementations.Base;

namespace USchedule.Core.Entities.Implementations
{
    public class Teacher: EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        
        public Guid DepartmentId { get; set; }
        
        public Department Department { get; set; }
    }
}