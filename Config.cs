using System.Configuration;

namespace EmployeeDetails
{
    public class Config
    {
        public readonly static string ConnectionString;
        static Config()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnectionString"].ConnectionString;
        }
    }
}
