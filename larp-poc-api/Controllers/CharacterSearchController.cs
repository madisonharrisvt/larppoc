using System.Collections.Generic;
using System.Web.Http;

namespace larp_poc_api.Controllers
{
    public class CharacterSearchController : ApiController
    {
        [HttpGet]
        public List<Dictionary<string, string>> GetByPartialName(string id)
        {
            var query = $"SELECT * FROM tblCharacter WHERE name LIKE '%{id}%' ";

            var results = MSSQL.ExecuteMsSqlQuery(query);

            return results;
        }
    }
}