using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_P2026.Data.MetaDataApp
{
	public static class Router
	{
		public const string root = "Api";
		public const string version = "V1";
		public const string Rule = root + "/" + version + "/";

		public static class Auth
		{
			public const string prefix = Rule + "Auth/";
			public const string Register = prefix + "Register";
			public const string Login = prefix + "Login";
			public const string Logout = prefix + "Logout";
			public const string ConfirmEmail = prefix + "Confirm-Email";
			public const string ResendConfirmEmail = prefix + "Resend-Confirm-Email";
			public const string ForgotPassword = prefix + "Forgot-Password";
			public const string ResetPassword = prefix + "Reset-Password";
			public const string checkEmailExists = prefix + "Check-Email/{email}";
			public const string checkUsernameExists = prefix + "Check-Username/{username}";
			public const string checkEmailVerified = prefix + "Check-Email-Verified/{email}";
		}

		public static class Users
		{
			public const string prefix = Rule + "Users/";

			public const string GetAllUsers = prefix + "Users/all-users";
			public const string GetUserById = prefix + "Users/Get-UserById/{id}";
			public const string UpdateUser = prefix + "Users/Update-User";
			public const string DeleteUser = prefix + "Users/Delete-User/{id}";
		}
	}
}
