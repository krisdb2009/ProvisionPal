using ProvisionPal.Services;
using System.Data;
using System.Data.SqlClient;
using System.Security.Principal;

namespace ProvisionPal.Classes
{
    public static class FormUtil
    {
        public static void Submit(
            Database DB,
            Guid RequestID,
            Guid FormID,
            IPrincipal Author,
            Dictionary<Guid, string> Parameters,
            Dictionary<Guid, string> ScriptGroups
        ) {
            DB.Connection.Open();
            SqlCommand cmd = DB.Connection.CreateCommand();
            cmd.Parameters.AddWithValue("@REQID@", RequestID);
            cmd.Parameters.AddWithValue("@FRMID@", FormID);
            cmd.Parameters.AddWithValue("@AUTHOR@", Author.Identity?.Name);
            cmd.CommandText = "SELECT RequestID FROM Requests WHERE RequestID = @REQID@";
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Close();
                cmd.CommandText = "UPDATE Requests SET LastModifiedBy = @AUTHOR@, LastModifiedTime = SYSDATETIME(), Status = 'Modified' WHERE RequestID = @REQID@";
                cmd.ExecuteNonQuery();
            }
            else
            {
                reader.Close();
                cmd.CommandText = "INSERT INTO Requests (RequestID, FormID, RequestedBy, LastModifiedBy, RequestedTime) VALUES (@REQID@, @FRMID@, @AUTHOR@, @AUTHOR@, SYSDATETIME())";
                cmd.ExecuteNonQuery();
            }
            cmd.CommandText = "DELETE FROM RequestXRefParameters WHERE RequestID = @REQID@";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "DELETE FROM RequestXRefScriptGroups WHERE RequestID = @REQID@";
            cmd.ExecuteNonQuery();
            cmd.Parameters.Add("@KEY@", SqlDbType.UniqueIdentifier);
            cmd.Parameters.Add("@VALUE@", SqlDbType.VarChar);
            foreach (KeyValuePair<Guid, string> param in Parameters)
            {
                cmd.Parameters["@KEY@"].Value = param.Key;
                cmd.Parameters["@VALUE@"].Value = param.Value;
                cmd.CommandText = "INSERT INTO RequestXRefParameters (RequestID, ParameterID, Value) VALUES (@REQID@, @KEY@, @VALUE@)";
                cmd.ExecuteNonQuery();
            }
            foreach (KeyValuePair<Guid, string> sg in ScriptGroups)
            {
                if (sg.Value != "on") continue;
                cmd.Parameters["@KEY@"].Value = sg.Key;
                cmd.CommandText = "INSERT INTO RequestXRefScriptGroups (RequestID, ScriptGroupID) VALUES (@REQID@, @KEY@)";
                cmd.ExecuteNonQuery();
            }
            DB.Connection.Close();
        }
        public static void Read(Database DB, Guid RequestID, out Dictionary<Guid, string> Parameters, out Dictionary<Guid, string> ScriptGroups)
        {
            Parameters = new();
            ScriptGroups = new();
            DB.Connection.Open();
            SqlCommand cmd = DB.Connection.CreateCommand();
            cmd.Parameters.AddWithValue("@REQID@", RequestID);
            cmd.CommandText = "SELECT ParameterID, Value FROM RequestXRefParameters WHERE RequestID = @REQID@";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) Parameters.Add(reader.GetGuid(0), reader.GetString(1));
            reader.Close();
            cmd.CommandText = "SELECT ScriptGroupID FROM RequestXRefScriptGroups WHERE RequestID = @REQID@";
            reader = cmd.ExecuteReader();
            while (reader.Read()) ScriptGroups.Add(reader.GetGuid(0), "on");
            DB.Connection.Close();
        }
    }
}