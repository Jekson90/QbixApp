using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace QbixApp
{
    [Table]
    class SkillEmployee
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column]
        public int EmpId { get; set; }
        [Column]
        public int SkillId { get; set; }
    }
}
