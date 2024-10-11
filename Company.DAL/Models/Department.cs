using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Models
{
    public class Department: ModelBase
    {
        public int Department_Id { get; set; }
        public string Name { get; set; }
        public DateOnly Creation_Date { get; set; }
        public string ImageName { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
    }
}
