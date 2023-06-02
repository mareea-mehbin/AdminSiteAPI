using AdminSiteAPI.Model;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace AdminSiteAPI.Controllers
{
    [ApiController]
    public class LoginController : Controller
    {
        [HttpPost]
        [Route("api/login")]
        public bool Login([FromBody] Login data)
        {
            SqlConnection con = new SqlConnection(@"Data Source=MAREEAME-LT\MSSQLSERVER_1;Initial Catalog=AdminSiteDB;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Users where username=@username AND pwd=@pwd", con);
            cmd.Parameters.AddWithValue("@username", data.UserName);
            cmd.Parameters.AddWithValue("@pwd", data.Password);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}