using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcMorning.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Addres { get; set; }


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
    }
}