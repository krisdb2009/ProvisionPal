using System.Data.SqlClient;

namespace ProvisionPal.Services
{
    public class Database
    {
        public SqlConnection Connection = new();
        public Database()
        {
            Connection.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=ProvisionPal;Integrated Security=True;MultipleActiveResultSets=True";
        }
    }
}
