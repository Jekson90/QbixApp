using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.Linq;
using System.Data.SqlClient;

namespace QbixApp
{
    class SearchLinqToSql
    {
        public static Employee Search(string name)
        {
            var emp = ConnectionDB.GetEmployee(name);
            if (emp != null)
                foreach (var v in emp)
                {
                    v.skill = GetSkills(v.Id);
                    v.post = GetPost(v.PostId);
                    return v;
                }                    
            return null;
        }

        static Skill[] GetSkills(int id)
        {
            return ConnectionDB.GetSkillsOfEmployee(id);
        }

        public static Skill GetSkill(string skill)
        {
            return ConnectionDB.GetSkill(skill);
        }

        static Post GetPost(int id)
        {
            return ConnectionDB.GetPost(id);
        }

        public static Post GetPost(string post)
        {
            return ConnectionDB.GetPost(post);
        }

        public static string[] GetAllPost()
        {
            Post[] post = ConnectionDB.GetAllPost();
            string[] p = new string[post.Length];
            for (int i = 0; i < post.Length; i++)
                p[i] = post[i].Name;
            return p;
        }
    }
}
