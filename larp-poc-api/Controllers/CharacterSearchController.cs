using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;

namespace larp_poc_api.Controllers
{
    public class CharacterSearchController : ApiController
    {
        [HttpGet]
        public List<Dictionary<string, string>> GetByPartialName(string id)
        {
            var query = $"SELECT * FROM tblCharacter WHERE name LIKE '%' + @id + '%' ";

            var listParams = new SqlParameter[] { new SqlParameter("id", SqlDbType.NVarChar) { Value = id } };

            var results = MSSQL.ExecuteMsSqlQuery(query, listParams);

            return results;
        }
    }
}