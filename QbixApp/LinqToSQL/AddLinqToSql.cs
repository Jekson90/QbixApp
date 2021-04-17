using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QbixApp
{
    class AddLinqToSql
    {
        public static bool AddSkill(string name, int postId)
        {
            Skill skill = new Skill();
            skill.Name = name;
            skill.PostId = postId;
            try
            {
                ConnectionDB.AddSkill(skill);
            } catch
            {
                return false;
            }
            
            return true;
        }

        public static bool AddEmployee(Employee employee)
        {
            try
            {
                ConnectionDB.AddEmployee(employee);
                ConnectionDB.AddSkillEmployee(employee);
            } catch 
            {
                return false;
            }
            return true;
        }        
    }
}
