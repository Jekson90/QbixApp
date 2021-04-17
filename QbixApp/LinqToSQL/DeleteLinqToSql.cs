using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QbixApp
{
    class DeleteLinqToSql
    {
        public static bool DeleteEmployee(string name)
        {
            try
            {
                Employee employee = SearchLinqToSql.Search(name);
                if (employee != null)
                    ConnectionDB.DeleteEmployee(employee);
                else throw new ArgumentException();
            } catch
            {
                return false;
            }            
            return true;
        }
    }
}
