using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SqlConnectionAdo.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Salary { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
    }
}