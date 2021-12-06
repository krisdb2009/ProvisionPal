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
            cmd.CommandText = "SELECT RegularExpression, Required FROM Parameters WHERE ParameterID = @PARAMID@";
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            string regex = "";
            if (!reader.IsDBNull(0)) regex = reader.GetString(0);
            bool isRequired = reader.GetBoolean(1);
            reader.Close();

            cmd.CommandText = "SELECT Value FROM ParameterXRefMultiSelect WHERE ParameterID = @PARAMID@";
            reader = cmd.ExecuteReader();
            List<string> MultiSelectVals = new();
            while (reader.Read()) MultiSelectVals.Add(reader.GetString(0));
            DB.Connection.Close();
            if (isRequired && ValueToValidate == "") return false;
            if (MultiSelectVals.Count > 0)
            {
                if (MultiSelectVals.Contains(ValueToValidate))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (regex == "") return true;
            return Regex.Match(ValueToValidate, (string)regex).Value == ValueToValidate;
        }
    }
}
