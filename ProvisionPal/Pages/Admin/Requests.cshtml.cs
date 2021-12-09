using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ProvisionPal.Pages.Admin
{
    public class RequestsModel : PageModel
    {
        public List<RequestDetails> Requests = new();
        public List<SearchTerm> SearchTerms = new()
        {
            new() { CustomParameter = false, Name = "RequestedBy", DisplayName = "From" },
            new() { CustomParameter = false, Name = "Status", DisplayName = "Status" }
        };
        public class RequestDetails
        {
            public Guid RequestID;
            public string SourceFormName;
            public string CreatedBy;
            public string ModifiedBy;
            public string Status;
            public DateTime Created;
            public DateTime Modified;
            public Dictionary<string, string> CaptionParameters = new();
        }
        public class SearchTerm
        {
            public bool CustomParameter;
            public string Name;
            public string DisplayName;
            public string Value;
        }
        public void OnGet(Services.Database DB)
        {
            DB.Connection.Open();
            SqlCommand cmd = DB.Connection.CreateCommand();
            
            
            cmd.CommandText = 
            "SELECT " +
            "RequestID, " +
            "(SELECT [Name] FROM Forms WHERE Forms.FormID = Requests.FormID) AS SourceFormName, " +
            "RequestedBy, " +
            "RequestedTime, " +
            "LastModifiedBy, " +
            "LastModifiedTime, " +
            "[Status] " +
            "FROM Requests";
            SqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                RequestDetails rd = new()
                {
                    RequestID = reader.GetGuid(0),
                    SourceFormName = reader.GetString(1),
                    CreatedBy = reader.GetString(2),
                    Created = reader.GetDateTime(3),
                    ModifiedBy = reader.GetString(4),
                    Modified = reader.GetDateTime(5),
                    Status = reader.GetString(6)
                };
                Requests.Add(rd);
            }
            reader.Close();
            cmd.Parameters.Add("@REQID@", System.Data.SqlDbType.UniqueIdentifier);
            cmd.CommandText =
            "SELECT DISTINCT [Name], [Value], [Order] " +
            "FROM RequestXRefParameters " +
            "INNER JOIN Parameters ON RequestXRefParameters.ParameterID = Parameters.ParameterID " +
            "INNER JOIN Requests ON RequestXRefParameters.RequestID = Requests.RequestID " +
            "INNER JOIN FormXRefParameters ON Requests.FormID = FormXRefParameters.FormID " +
            "AND FormXRefParameters.ParameterID = RequestXRefParameters.ParameterID " +
            "WHERE RequestXRefParameters.RequestID = @REQID@ " +
            "AND Caption = 1 " +
            "ORDER BY [Order]";
            foreach (RequestDetails rd in Requests)
            {
                cmd.Parameters["@REQID@"].Value = rd.RequestID;
                reader = cmd.ExecuteReader();
                while (reader.Read()) rd.CaptionParameters.Add(reader.GetString(0), reader.GetString(1));
                reader.Close();
            }
            DB.Connection.Close();
        }
    }
}
