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

    public class LoginEnter
    {
        [Required(ErrorMessage = "Enter your Username")]

        public string Username { get; set; }
        [Required(ErrorMessage = "Enter Tour Password"), DataType(DataType.Password)]
        public string Password { get; set; }
        public string HashedPassword(string Password)
        {
#pragma warning disable 618
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(Password, "SHA1");
#pragma warning restore 618
        }
        public int Branch { get; set; }
        public Permissions CurrentPermissions { get; set; }
        public enum Permissions
        {
            None = 0,
            Update = 1 << 0,
            Delete = 1 << 1,
            Add = 1 << 2,
            View = 1 << 3 | Add,
            Super = (Update | View),
            Admin = (Update | Delete | Add | View)
        }
        public string UserType { get; set; }
    }
}