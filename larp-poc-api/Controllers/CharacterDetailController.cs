using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json.Linq;

namespace larp_poc_api.Controllers
{
    public class CharacterDetailController : ApiController
    {

        [HttpGet]
        public Dictionary<string, string> GetCharacterById(string id)
        {
            var query = $"SELECT * FROM tblCharacter WHERE id = '{id}' ";

            var results = MSSQL.ExecuteMsSqlQuery(query);

            return results.FirstOrDefault();
        }

        [HttpPut]
        public Dictionary<string, string> UpdateCharacterNameById([FromBody] JObject body)
        {
            var query = $"UPDATE tblCharacter SET name = '{body["name"]}' WHERE id = '{body["id"]}'";

            var results = MSSQL.ExecuteMsSqlQuery(query);

            return results.FirstOrDefault();
        }

        [HttpDelete]
        public IHttpActionResult DeleteCharacterById(Guid id)
        {
            var characterId = new SqlParameter("CharacterId", SqlDbType.UniqueIdentifier) { Value = id };
            var query = $"DELETE FROM tblCharacter WHERE id = @CharacterId";

            MSSQL.ExecuteNonQuery(query, characterId);

            return Ok();
        }
    }
}