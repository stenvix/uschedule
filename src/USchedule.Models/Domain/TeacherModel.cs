namespace USchedule.Models.Domain
{
    public class TeacherModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        
        public DepartmentModel Department { get; set; }
    }
}