using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmpert
{
    class DatabaseHandler
    {
        public string ConnectionString
        {
            get;
            private set;
        }

        public DatabaseHandler()
            : this(@"C:\USERS\TOMMY\DOCUMENTS\DATABASE.MDF")
        {
            
        }

        public DatabaseHandler(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string GetValue(string query)
        {
            string result = "";

            try
            {

            }
            catch (Exception)
            {
                
            }

            return result;
        }

        public object[] GetValues(string query)
        {
            object[] result = { };

            try
            {

            }
            catch (Exception)
            {
                
            }

            return result;
        }

        public object[] GetValues(string table, string[] columns, QueryStatements statements)
        {
            object[] result = { };

            try
            {
                string query = QueryBuilder.BuildQuery(table, columns, statements);
                result = GetValues(query);
            }
            catch (Exception)
            {
                
            }

            return result;
        }
    }
}
