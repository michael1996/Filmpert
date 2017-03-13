using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmpert
{
    class DatabaseHandler
    {
        public string DefaultConnectionString
        {
            get
            {
                return @"C:\USERS\TOMMY\DOCUMENTS\DATABASE.MDF";
            }
        }

        public string ConnectionString
        {
            get;
            private set;
        }

        public DatabaseHandler()
        {

        }
    }
}
