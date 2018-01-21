using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;

namespace larp_poc_api.Controllers
{
    public class CharactersController : ApiController
    {
        [HttpGet]
        public List<Dictionary<string, string>> GetAllCharacters()
        {
            var query = "select * from tblCharacter";

            var results = MSSQL.ExecuteMsSqlQuery(query);

            return results;
        }

        [HttpPost]
        public IHttpActionResult AddCharacter([FromBody] string name)
        {
            var query = $"INSERT INTO tblCharacter (CharacterID, CharacterName) VALUES(NEWID (), '{name}')";

            MSSQL.ExecuteNonQuery(query);

            return Ok(GetAllCharacters());
        }

        [HttpDelete]
        public IHttpActionResult DeleteCharacter([FromBody] string id)
        {
            var sqlId = new SqlParameter("id", SqlDbType.UniqueIdentifier) {Value = new Guid(id)};
            var query = $"DELETE FROM tblCharacter WHERE CharacterId = @id";

            MSSQL.ExecuteNonQuery(query, sqlId);

            return Ok(GetAllCharacters());
        }
    }
}