using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ProvisionPal.Pages.Admin
{
    public class RequestsModel : PageModel
    {
        public List<RequestDetails> Requests = new();
        public List<SearchTerm> SearchTerms = new()
        {
            new() { CustomParameter = false, Name = "Name", DisplayName = "Type" },
            new() { CustomParameter = false, Name = "RequestedTime", DisplayName = "At" },
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
            public string Created;
            public string Modified;
            public Dictionary<string, string> CaptionParameters = new();
        }
        public class SearchTerm
        {
            public bool CustomParameter;
            public Order OrderBy;
            public string Name;
            public string DisplayName;
            public string Value;
        }
        public enum Order
        {
            None,
            Ascending,
            Descending
        }
        public void OnGet(Services.Database DB)
        {
            DB.Connection.Open();
            SqlCommand cmd = DB.Connection.CreateCommand();

            cmd.CommandText = 
            "SELECT DISTINCT " +
            "[Parameters].ParameterID, " +
            "[Parameters].Name " +
            "FROM FormXRefParameters " +
            "INNER JOIN [Parameters] " +
            "ON ([Parameters].ParameterID = [FormXRefParameters].ParameterID) " +
            "WHERE Caption = 1 " +
            "ORDER BY Name ASC";

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read()) SearchTerms.Add(new() {
                CustomParameter = true,
                Name = reader.GetGuid(0).ToString(),
                DisplayName = reader.GetString(1)
            });

            reader.Close();
            
            foreach (SearchTerm term in SearchTerms)
            {
                if (!Request.Query.ContainsKey(term.Name)) continue;
                term.Value = Request.Query[term.Name];
            }
            
            cmd.CommandText = 
            "SELECT * FROM (SELECT DISTINCT " +
            "Requests.RequestID, " +
            "Forms.Name, " +
            "Requests.RequestedBy, " +
            "CONVERT(varchar, Requests.RequestedTime, 22) AS RequestedTime, " +
            "Requests.LastModifiedBy, " +
            "CONVERT(varchar, Requests.LastModifiedTime, 22) AS LastModifiedTime, " +
            "[Status]" +
            SearchTermBuildSelectClause(cmd) +
            "FROM Requests " +
            "INNER JOIN Forms " +
            "ON (Requests.FormID = Forms.FormID)) AS REQS " +
            SearchTermBuildWhereClause(cmd) +
            "ORDER BY REQS.RequestedTime DESC";

            reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                RequestDetails rd = new()
                {
                    RequestID = reader.GetGuid(0),
                    SourceFormName = reader.GetString(1),
                    CreatedBy = reader.GetString(2),
                    Created = reader.GetString(3),
                    ModifiedBy = reader.GetString(4),
                    Modified = reader.GetString(5),
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
        private string SearchTermBuildWhereClause(SqlCommand cmd)
        {
            int index = 0;
            List<string> conditions = new();
            foreach (SearchTerm term in SearchTerms)
            {
                if (term.Value == null) continue;
                string value = "@VALUE" + index + "@";
                cmd.Parameters.AddWithValue(value, "%" + term.Value + "%");
                if (term.Name == "RequestedTime")
                {
                    conditions.Add("(REQS.[" + term.Name + "] LIKE " + value + ")");
                }
                else
                {
                    conditions.Add("(REQS.[" + term.Name + "] LIKE " + value + ")");
                }
                index++;
            }
            if (conditions.Count > 0)
            {
                return "WHERE " + string.Join(" AND ", conditions) + " ";
            }
            else
            {
                return "";
            }
        }
        private string SearchTermBuildSelectClause(SqlCommand cmd)
        {
            List<string> cols = new();
            foreach (SearchTerm term in SearchTerms)
            {
                if (term.Value == null || !term.CustomParameter) continue;
                cols.Add("(SELECT Value FROM RequestXRefParameters WHERE RequestID LIKE Requests.RequestID AND ParameterID = '" + term.Name + "') AS '" + term.Name + "'");
            }
            if (cols.Count > 0)
            {
                return ", " + string.Join(", ", cols) + " ";
            }
            else
            {
                return " ";
            }
        }
    }
}
