using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
using larp_poc_api.Models;

namespace larp_poc_api.Controllers
{
    public class CharactersController : ApiController
    {
        private readonly string _settings = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;

        private readonly Character[] _characters = new Character[]
        {
            new Character { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            new Character { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
            new Character { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
        };

        public IEnumerable<Character> GetAllCharacters()
        {
            SqlDataReader rdr = null;
            SqlConnection conn = new SqlConnection(_settings);
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from tblCharacter", conn);
            rdr = cmd.ExecuteReader();
            conn.Close();
            conn.Dispose();

            return _characters;
        }

        public IHttpActionResult GetCharacter(int id)
        {
            SqlDataReader rdr = null;
            SqlConnection conn = new SqlConnection(_settings);
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from tblCharacter", conn);
            rdr = cmd.ExecuteReader();
            conn.Close();
            conn.Dispose();

            var character = _characters.FirstOrDefault((p) => p.Id == id);
            if (character == null)
            {
                return NotFound();
            }
            return Ok(character);
        }
    }
}