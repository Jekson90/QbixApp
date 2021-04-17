using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace QbixApp
{
    [Table(Name = "Employee")]
    class Employee
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column(Name ="Name")]
        public string Name { get; set; }
        [Column]
        public int Age { get; set; }
        [Column]
        public int PostId { get; set; }

        public Skill[] skill;
        public Post post;
    }
}
