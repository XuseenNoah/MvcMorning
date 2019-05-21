using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MvcMorning.Models
{
    public class Login
    {
        public string connection = System.Configuration.ConfigurationManager.ConnectionStrings["Db"].ConnectionString;
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }

        internal Login LoginEnter(Login login)
        {
            using (var conn = new SqlConnection(connection))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"SELECT *FROM Users WHERE Username=@User AND Password=@Pass";
                cmd.Parameters.AddWithValue("@User", login.Username);
                cmd.Parameters.AddWithValue("@Pass", login.Password);
                conn.Open();
                var reader = cmd.ExecuteReader();
                Login loginEnter = null;
                if (reader.Read())
                {
                    loginEnter = new Login();
                    loginEnter.Id = (int)reader["Id"];
                    loginEnter.Username = reader["Username"] as string;
                    loginEnter.Password = reader["Password"] as string;
                }
                return loginEnter;
            }
        }
    }
}