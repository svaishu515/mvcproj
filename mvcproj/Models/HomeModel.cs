using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvcproj.Models
{
    public class HomeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public List<string> Projects { get; set; }
        public int Age { get; set; }
    }
}
