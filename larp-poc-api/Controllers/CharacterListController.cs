using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json.Linq;

namespace larp_poc_api.Controllers
{
    public class CharacterListController : ApiController
    {
        [HttpGet]
        public List<Dictionary<string, string>> GetAllCharacters()
        {
            var query = "select * from tblCharacter";

            var results = MSSQL.ExecuteMsSqlQuery(query);

            return results;
        }

        /*
        [HttpPost]
        public IHttpActionResult AddCharacter([FromBody] string name)
        {
            var query = $"INSERT INTO tblCharacter (id, name) VALUES(NEWID (), '{name}')";

            MSSQL.ExecuteNonQuery(query);

            return Ok(GetAllCharacters());
        }

        [HttpDelete]
        public IHttpActionResult DeleteCharacter([FromBody] string id)
        {
            var sqlId = new SqlParameter("id", SqlDbType.UniqueIdentifier) {Value = new Guid(id)};
            var query = $"DELETE FROM tblCharacter WHERE id = @id";

            MSSQL.ExecuteNonQuery(query, sqlId);

            return Ok(GetAllCharacters());
        }*/

        [HttpPost]
        public Dictionary<string, string> AddCharacter([FromBody] JObject body)
        {
            var name = body["name"];
            var query = $@" DECLARE @newId varchar(255) = NEWID ()  INSERT INTO tblCharacter (id, name) VALUES(@newId, @name)  SELECT * from tblCharacter WHERE id = @newId";

            var listParams = new SqlParameter[] { new SqlParameter("name", SqlDbType.NVarChar) { Value = name } };

            var results = MSSQL.ExecuteMsSqlQuery(query, listParams);

            return results.FirstOrDefault();
        }
    }
}