using AdminSiteAPI.Model;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace AdminSiteAPI.Controllers
{
    [ApiController]
    public class AdminController : Controller
    {
        [HttpGet]
        [Route("api/Admin/getuserList")]
        public List<User> getuserList()
        {
            List<User> users = new List<User>();
            SqlConnection con = new SqlConnection(@"Data Source=MAREEAME-LT\MSSQLSERVER_1;Initial Catalog=AdminSiteDB;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Users", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                users.Add(new User
                {
                    Id = dr["id"].ToString(),
                    UserName = dr["username"].ToString(),
                    CompanyID = dr["companyID"].ToString(),
                    CompanyName = dr["companyName"].ToString(),
                    UserType = dr["userType"].ToString()
                });
            }
            return users;

        }
        [HttpGet]
        [Route("api/Admin/getuserListbyID/{id}")]
        public User getuserListbyID(int id)
        {
            User usr = new User();
            SqlConnection con = new SqlConnection(@"Data Source=MAREEAME-LT\MSSQLSERVER_1;Initial Catalog=AdminSiteDB;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Users where id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                usr.Id = dr["id"].ToString();
                usr.UserName = dr["username"].ToString();
                usr.CompanyID = dr["companyID"].ToString();
                usr.CompanyName = dr["companyName"].ToString();
                usr.UserType = dr["userType"].ToString();
            }
            return usr;
        }
        [HttpPost]
        [Route("api/Admin/addUser")]
        public int addUser([FromBody] User usr)
        {
            SqlConnection con = new SqlConnection(@"Data Source=MAREEAME-LT\MSSQLSERVER_1;Initial Catalog=AdminSiteDB;Integrated Security=True");
            con.Open();
            string sqlStr = "insert into Users values(@id,@username,@companyID,@companyName,@userType)";
            SqlCommand cmd = new SqlCommand(sqlStr, con);
            cmd.Parameters.AddWithValue("@id", usr.Id);
            cmd.Parameters.AddWithValue("@username", usr.UserName);
            cmd.Parameters.AddWithValue("@companyID", usr.CompanyID);
            cmd.Parameters.AddWithValue("@companyName", usr.CompanyName);
            cmd.Parameters.AddWithValue("@userType", usr.UserType);
            int result = cmd.ExecuteNonQuery();
            return result;
        }
        [HttpDelete]
        [Route("api/Admin/deleteUser/{id}")]
        public int deleteUser(int id)
        {
            SqlConnection con = new SqlConnection(@"Data Source=MAREEAME-LT\MSSQLSERVER_1;Initial Catalog=AdminSiteDB;Integrated Security=True");
            con.Open();
            string sqlStr = "delete from Users where id=@id";
            SqlCommand cmd = new SqlCommand(sqlStr, con);
            var usr = getuserListbyID(id);
            cmd.Parameters.AddWithValue("@id", usr.Id);
            int result = cmd.ExecuteNonQuery();
            return result;
        }
    }
}
