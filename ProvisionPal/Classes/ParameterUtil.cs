using System.Data.SqlClient;
using ProvisionPal.Services;
using System.Text.RegularExpressions;

namespace ProvisionPal.Classes
{
    public static class ParameterUtil
    {
        public static bool Validate(Database DB, Guid Parameter, string ValueToValidate)
        {
            DB.Connection.Open();
            SqlCommand cmd = DB.Connection.CreateCommand();
            cmd.Parameters.AddWithValue("@PARAMID@", Parameter);
            cmd.CommandText = "SELECT RegularExpression FROM Parameters WHERE ParameterID = @PARAMID@";
            object regex = cmd.ExecuteScalar();
            DB.Connection.Close();
            if (regex.GetType() == typeof(DBNull)) return true;
            return Regex.Match(ValueToValidate, (string)regex).Value == ValueToValidate;
        }
    }
}
