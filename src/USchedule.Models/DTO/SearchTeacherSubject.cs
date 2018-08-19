using System;

namespace USchedule.Models.DTO
{
    public class SearchTeacherSubject
    {
        public Guid SubjectId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}