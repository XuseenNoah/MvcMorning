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
        [Required,StringLength(10)]
        public string Name { get; set; }
        [Required,EmailAddress,Display(Name ="Email Addres")]
        public string Addres { get; set; }
        [Required,MaxLength(10),MinLength(5)]
        public string Phone { get; set; }
        [Range(15,40)]
        public int Age { get; set; }
        public HttpPostedFileBase Image { get; set; }

        public DateTime Date { get; set; }

        public static List<Person> GetListPerson(string personName,string phone)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"SELECT *FROM Persons WHERE Name LIKE @Name AND Phone LIKE @Phone";
                cmd.Parameters.AddWithValue("@Name", personName +"%%");
                cmd.Parameters.AddWithValue("@Phone", phone +"%%");




                conn.Open();
                var reader = cmd.ExecuteReader();
                var listPerson = new List<Person>();
                while (reader.Read())
                {
                    var person = new Person();
                    person.Id =(int) reader["Id"];
                    person.Name = reader["Name"]as string;
                    person.Addres =reader["Addres"]as string;
                    person.Phone = reader["Phone"]as string;
                    person.Date = (DateTime)reader["DAte"];
                    listPerson.Add(person);
                }
                return listPerson;
            }

        }


        public static void CreatePerson(Person person)
        {
            using (var conn = new SqlConnection(connectionString))
                using(var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "Insert into Persons Values (@Name,@Addres,@Phone,getdate(),@Image)";
                cmd.Parameters.AddWithValue("@Name", person.Name);
                cmd.Parameters.AddWithValue("@Addres", person.Addres);
                cmd.Parameters.AddWithValue("@Phone", person.Phone);
                var bytes = new byte[person.Image.ContentLength];
                person.Image.InputStream.Read(bytes, 0, person.Image.ContentLength);
                cmd.Parameters.AddWithValue("@Image", bytes);
                conn.Open();
                cmd.ExecuteNonQuery();


            }


        }

        internal static byte[] GetImage(string id)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"SELECT Image FROM Persons WHERE Id=@Id";
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                var reader = cmd.ExecuteScalar() as byte[];
                return reader;
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

        internal static void DeletePerson(string id)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd =conn.CreateCommand())
            {
                cmd.CommandText = @"DELETE FROM Persons WHERE Id=@Id ";
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        //internal static void DeletePerson(string id)
        //{
        //    using (var conn = new SqlConnection(connectionString))
        //    using (var cmd = conn.CreateCommand())
        //    {
        //        cmd.CommandText = @"DELETE FROM Persons WHERE Id=@Id";
        //        cmd.Parameters.AddWithValue("@Id", id);
        //        conn.Open();
        //        cmd.ExecuteNonQuery();

        //    }
        //}

        internal static object GetPersonDetail(string id)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"SELECT *FROM Persons WHERE Id=@id";
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                var reader = cmd.ExecuteReader();
                Person person=null;
                if (reader.Read())
                {
                    person = new Person();
                    person.Id = (int)reader["Id"];
                    person.Name = reader["Name"] as string;
                    person.Addres = reader["Addres"] as string;
                    person.Phone = reader["Phone"] as string;
                    
                }
                return person;

            }
        }

        //public static void DeletePerson(int id)
        //{
        //    using (var conn = new SqlConnection(connectionString))
        //    using (var cmd = conn.CreateCommand())
        //    {
        //        cmd.CommandText = @"DELETE FROM Persons WHERE Id=@Id";
        //        cmd.Parameters.AddWithValue("@Id", id);
        //        conn.Open();
        //        cmd.ExecuteNonQuery();
        //    }
        //}

        //internal static void DeletePerson(int id)
        //{
        //    using (var conn = new SqlConnection(connectionString))
        //    using (var cmd = conn.CreateCommand())
        //    {
        //        cmd.CommandText = @"DELETE FROM Persons WHERE Id=@id";
        //        cmd.Parameters.AddWithValue("@id", id);
        //        conn.Open();
        //        cmd.ExecuteNonQuery();

        //    }
        //}

        internal static void UpdatePerson(Person person)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"UPDATE Persons SET Name=@Name,Addres=@Addres,Phone=@Phone WHERE Id=@Id";
                cmd.Parameters.AddWithValue("@Id", person.Id);
                cmd.Parameters.AddWithValue("@Name", person.Name);
                cmd.Parameters.AddWithValue("@Addres", person.Addres);
                cmd.Parameters.AddWithValue("@Phone", person.Phone);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

       


        



    }
}