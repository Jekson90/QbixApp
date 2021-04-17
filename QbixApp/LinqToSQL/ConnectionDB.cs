using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;

namespace QbixApp
{ 
    class ConnectionDB
    {
        const string connectionString = @"Data Source=JEKSON;Initial Catalog=qbixdb;Integrated Security=True";
        static DataContext data = new DataContext(connectionString);

        public static IQueryable<Employee> GetEmployee(string name)
        {            
            return data.GetTable<Employee>().Where(e => e.Name == name);
        }
                
        public static Skill[] GetSkillsOfEmployee(int employeeId)
        {
            var skillemployee = data.GetTable<SkillEmployee>().Where(se => se.EmpId == employeeId);
            int count = 0;
            foreach (var se in skillemployee) count++;

            Skill[] skills = new Skill[count];
            count = 0;
            foreach (var se in skillemployee)
            {
                foreach (var s in data.GetTable<Skill>().Where(i => i.Id == se.SkillId))
                    skills[count] = s;
                count++;
            }

            return skills;
        }

        public static Skill GetSkill(string skill)
        {
            var skills = data.GetTable<Skill>().Where(s => s.Name == skill);
            foreach (var v in skills)
                return v;
            return null;
        }

        public static Post GetPost(int id)
        {
            var post = data.GetTable<Post>().Where(e => e.Id == id);
            foreach (var v in post)
                return v;
            return null;
        }

        public static Post GetPost(string post)
        {
            var posts = data.GetTable<Post>().Where(p => p.Name == post);
            foreach (var v in posts)
                return v;
            return null;
        }

        public static Post[] GetAllPost()
        {
            var allPosts = data.GetTable<Post>();
            int postCount = 0;
            foreach (var v in allPosts) postCount++;
            Post[] post = new Post[postCount];
            int i = 0;
            foreach (var v in allPosts)
            {
                post[i] = v;
                i++;
            }
            return post;
        }

        public static void AddSkill(Skill skill)
        {
            data.GetTable<Skill>().InsertOnSubmit(skill);
            data.SubmitChanges();
        }

        public static void AddEmployee(Employee employee)
        {
            data.GetTable<Employee>().InsertOnSubmit(employee);
            data.SubmitChanges();
        }

        public static void AddSkillEmployee(Employee employee)
        {
            SkillEmployee[] se = new SkillEmployee[employee.skill.Length];
            for (int i = 0; i < employee.skill.Length; i++)
            {
                se[i] = new SkillEmployee();
                se[i].EmpId = employee.Id;
                se[i].SkillId = employee.skill[i].Id;
            }
            data.GetTable<SkillEmployee>().InsertAllOnSubmit<SkillEmployee>(se);
            data.SubmitChanges();
        }

        public static void DeleteEmployee(Employee employee)
        {
            data.GetTable<Employee>().DeleteOnSubmit(employee);
            data.SubmitChanges();
        }
    }
}
