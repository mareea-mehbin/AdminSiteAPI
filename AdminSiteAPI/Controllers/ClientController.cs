using AdminSiteAPI.Model;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace AdminSiteAPI.Controllers
{
    [ApiController]
    public class ClientController : Controller
    {
        [HttpGet]
        [Route("api/getClientList")]
        public List<Client> getClientList()
        {
            List<Client> clients = new List<Client>();
            SqlConnection con = new SqlConnection(@"Data Source=MAREEAME-LT\MSSQLSERVER_1;Initial Catalog=AdminSiteDB;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Clients", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                clients.Add(new Client
                {
                    ClientID = dr["cid"].ToString(),
                    ClientName = dr["cname"].ToString(),
                    ClientPermission = dr["cpermission"].ToString(),
                    ClientType = dr["cType"].ToString(),
                });
            }
            return clients;
        }

        [HttpGet]
        [Route("api/getClientList/{id}")]
        public Client getClientById(int id)
        {
            Client client = new Client();
            SqlConnection con = new SqlConnection(@"Data Source=MAREEAME-LT\MSSQLSERVER_1;Initial Catalog=AdminSiteDB;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Clients where cid=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                client.ClientID = dr["cid"].ToString();
                client.ClientName = dr["cname"].ToString();
                client.ClientPermission = dr["cpermission"].ToString();
                client.ClientType = dr["cType"].ToString();
            }
            return client;
        }

        [HttpDelete]
        [Route("api/deleteClient/{id}")]
        public int DeleteClient(int id)
        {
            SqlConnection con = new SqlConnection(@"Data Source=MAREEAME-LT\MSSQLSERVER_1;Initial Catalog=AdminSiteDB;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from Clients where cid=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            int result = cmd.ExecuteNonQuery();

            return result;
        }
    }
}