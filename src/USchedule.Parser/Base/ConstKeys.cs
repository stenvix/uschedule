namespace USchedule.Parser
{
    public static class ConstKeys
    {
        public static string InstituteName = "InstituteName";
        public static string InstituteId = "InstituteId";
        public static string SemesterId = "SemesterId";
        public static string SemesterPartId = "SemesterPartId";
        public static string GroupId = "GroupId";
        public static string GroupName = "GroupName";
        public static string DepartmentName = "DepartmentName";
        public static string DepartmentId = "DepartmentId";

        public static string JobTask = "JobTask";
        public static string JobScheduler = "JobScheduler";
        public static string JobBaseUrl = "JobBaseUrl";
        public static string JobLogger = "JobLogger";
        public static string TeacherFirstName => nameof(TeacherFirstName);
        public static string TeacherLastName => nameof(TeacherLastName);
    }
}