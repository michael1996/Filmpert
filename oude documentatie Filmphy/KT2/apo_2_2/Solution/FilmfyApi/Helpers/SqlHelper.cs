using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace FilmfyApi.Helpers
{
	public static class SqlHelper
	{
		const string UserPassConnectionString = @"Server={0};Database={1};User Id={2};Password={3};";
		const string WindowsAuthConnectionString = @"Server={0};Database{1};Trusted_Connection=True;";

		/// <summary>
		/// Gets the SQL Connection using the right connectionstring
		/// </summary>
		/// <returns></returns>
		public static SqlConnection GetConnection()
		{
			// Load the variables
			string sqlServer = WebConfigurationManager.AppSettings["Server"];
			string sqlDatabase = WebConfigurationManager.AppSettings["Database"];
			string sqlUserName = WebConfigurationManager.AppSettings["Username"];
			string sqlPassword = WebConfigurationManager.AppSettings["Password"];
			string sqlWindowsAuth = WebConfigurationManager.AppSettings["WindowsAuth"];

			bool windowsAuth = false;
			bool.TryParse(sqlWindowsAuth, out windowsAuth);

			string fullConnectionString;
			// Uses the WindowsAuth connection string
			if (windowsAuth)
			{
				fullConnectionString = string.Format(WindowsAuthConnectionString, sqlServer, sqlDatabase);
			}
			else
			{
				// Uses the UserPass connection string
				fullConnectionString = string.Format(UserPassConnectionString, sqlServer, sqlDatabase, sqlUserName, sqlPassword);
			}

			return new SqlConnection(fullConnectionString);
		}
	}
}