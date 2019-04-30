using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MvcMorning.Models
{
    public class Person
    {

        public static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Db"].ConnectionString;
        public int Id { get; set; }
        [Required,StringLength(20)]
        public string Name { get; set; }
        [Required]
        public string Addres { get; set; }
        [Required]
        public string Phone { get; set; }


        public static List<Person> GetListPerson()
        {
            return new List<Person> {
                new Person{Id=1,Name="Ahmed",Addres="Goljano"},
                new Person{Id=1,Name="Ahmed",Addres="Goljano"},
                new Person{Id=1,Name="Ahmed",Addres="Goljano"},
                new Person{Id=1,Name="Ahmed",Addres="Goljano"},
                new Person{Id=1,Name="Ahmed",Addres="Goljano"},
                new Person{Id=1,Name="Ahmed",Addres="Goljano"},
                new Person{Id=1,Name="Ahmed",Addres="Goljano"},
                new Person{Id=1,Name="Ahmed",Addres="Goljano"},
                new Person{Id=1,Name="Ahmed",Addres="Goljano"},
            };
        }


        public static void CreatePerson(Person person)
        {
            using (var conn = new SqlConnection(connectionString))
                using(var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "Insert into Persons Values (@Name,@Addres,@Phone,getdate())";
                cmd.Parameters.AddWithValue("@Name", person.Name);
                cmd.Parameters.AddWithValue("@Addres", person.Addres);
                cmd.Parameters.AddWithValue("@Phone", person.Phone);
                conn.Open();
                cmd.ExecuteNonQuery();


            }


        }

        public static Person GetPerson(string name)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"SELECT *FROM Persons WHERE Name=@Name";
                cmd.Parameters.AddWithValue("@Name", name);
                conn.Open();
                var reader = cmd.ExecuteReader();
                Person person = null;
                if (reader.Read())
                {
                    person = new Person();
                    person.Name = reader["Name"] as string;
                }
                return person;


            }


        }



    }
}