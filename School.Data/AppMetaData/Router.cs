namespace School.Data.AppMetaData
{
    public static class Router
    {
        public const string root = "Api/";
        public const string version = "v1/";
        public const string Rule = root + version;

        public static class StudentRouting
        {
            public const string StudentRule = Rule + "Student/";

            public const string GetAllStudnets = StudentRule + "GetAllStudnets";
            public const string GetAllStudnetsUsingPagination = StudentRule + "GetAllStudnetsUsingPagination";
            public const string GetStudentById = StudentRule + "GetStudentById/{studentId}";
            public const string DeleteStudent = StudentRule + "DeleteStudent/{studentId}";
            public const string CreateStudnet = StudentRule + "CreateStudnet";

            public const string GetStudentListWithDepartmentDetails = StudentRule + "ListStudentWithDepartmentDetails";
            public const string GetStudentListInDepartmentByDeptID = StudentRule + "GetStudentListInDepartmentByDeptID";
        }


        public static class DepartmentRouting
        {
            public const string DepartmentRule = Rule + "Department/";
            public const string GetAllDepartments = DepartmentRule + "GetAllDepartments";
            public const string GetAllDepartmentUsingPagination = DepartmentRule + "GetAllDepartmentsUsingPagination";
            public const string GetDepartmentById = DepartmentRule + "GetDepartmentById/{departmentId}";
            public const string DeleteDepartment = DepartmentRule + "DeleteDepartment/{departmentId}";
            public const string CreateDepartment = DepartmentRule + "CreateDepartment";
        }

        public static class UserRouting
        {
            public const string UserRule = Rule + "User/";
            public const string CreateUser = UserRule + "CreateUser";
            public const string EditeUser = UserRule + "EditeUser";
            public const string DeleteUser = UserRule + "DeleteUser";
            public const string ChangeUserPassword = UserRule + "ChangeUserPassword";
            public const string GetUsersList = UserRule + "GetUsersList";
            public const string GetSingleUserByEmail = UserRule + "GetSingleUserByEmail";

            public const string test = UserRule + "test";


        }

        public static class AuthenticationRouting
        {
            public const string AuthenticationRule = Rule + "Authentication/";
            public const string SignIn = AuthenticationRule + "SignIn";
            public const string RefreshToken = AuthenticationRule + "RefreshToken";
            public const string ConfirmEmail = AuthenticationRule + "ConfirmEmail";

            public const string SendResetPassword = AuthenticationRule + "SendResetPassword";
            public const string ConfirmResetPassword = AuthenticationRule + "ConfirmResetPassword";
            public const string ResetNewPassword = AuthenticationRule + "ResetNewPassword";

        }

        public static class AuthorizationRouting
        {
            public const string AuthorizationRule = Rule + "Authorization/";
            public const string AddRole = AuthorizationRule + "AddRole";
            public const string EditRole = AuthorizationRule + "EditRole";
            public const string DeleteRole = AuthorizationRule + "DeleteRole";
            public const string GetSingleRole = AuthorizationRule + "GetSingleRole";
            public const string GetUserRolesByUserId = AuthorizationRule + "GetUserRolesByUserId";
            public const string GetPaginatedRoleList = AuthorizationRule + "GetPaginatedRoleList";
            public const string UpdateUserRolesByUserId = AuthorizationRule + "UpdateUserRolesByUserId";
        }

        public static class EmailRouting
        {
            public const string EmailRule = Rule + "Email/";
            public const string SendEmail = EmailRule + "SendEmail";
        }



    }

}

