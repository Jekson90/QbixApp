using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QbixApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //this.comboBoxAdd.Items.AddRange(SearchLinqToSql.GetAllPost());
            this.comboBoxAdd.Items.AddRange(new string[]{ "Программист", "Бэкэндер", "Фронтендер", "Бухгалтер", "HR", "Верстальщик"});
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            this.outputBox.Text = "";
            string name = this.searchBox.Text;
            Employee employee = SearchLinqToSql.Search(name);
            string output = "";
            if (employee == null)
                output += "No match found.";
            else
            {
                output += $"Name:\t{employee.Name}";
                output += Environment.NewLine + $"Age:\t{employee.Age}";
                output += Environment.NewLine + $"Post:\t{employee.post.Name}";
                output += Environment.NewLine + Environment.NewLine + $"Skills:";
                for (int i = 0; i < employee.skill.Length; i++)
                {
                    output += $"\t{employee.skill[i].Name}" + Environment.NewLine;
                }
            }
            this.outputBox.Text = output;

        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (this.addBoxName.Text == "" || this.addBoxAge.Text == "" || this.comboBoxAdd.SelectedItem == null || this.addBoxSkill.Text == "")
            {
                MessageBox.Show("Please, fill all fields.");
                return;
            }

            //reading name
            string name = this.addBoxName.Text;

            //reading and analyzing Age
            int age = 0;
            try
            {
                age = int.Parse(this.addBoxAge.Text);
            } catch (FormatException ex)
            {
                MessageBox.Show($"Error: Age: {ex.Message}");
                this.addBoxAge.Text = "";
                return;
            }

            //reading and analyzing Post
            string p = this.comboBoxAdd.SelectedItem.ToString();
            Post post = SearchLinqToSql.GetPost(p);
            
            //reading and analyzing Skills
            Skill[] skills = null;
            try
            {
                skills = SkillsForEmployee();
            } catch (ArgumentException)
            {
                this.addBoxSkill.Text = "";
                return;
            }
            
            Employee employee = new Employee();
            employee.Name = name;
            employee.Age = age;
            employee.post = post;
            employee.PostId = post.Id;
            employee.skill = skills;

            bool status = AddLinqToSql.AddEmployee(employee);

            if (status) MessageBox.Show($"{employee.Name} add successfull.");
            else MessageBox.Show("Something goes wrong");
            this.addBoxName.Text = "";
            this.addBoxAge.Text = "";
            this.comboBoxAdd.SelectedItem = null;
            this.addBoxSkill.Text = "";
        }

        Skill[] SkillsForEmployee()
        {
            string[] skill = this.addBoxSkill.Text.Split(',');
            Skill[] skills = new Skill[skill.Length];
            int skillCount = 0;
            for (int i = 0; i < skill.Length; i++)
            {
                skills[i] = SearchLinqToSql.GetSkill(skill[i]);
                if (skills[i] != null)
                    skillCount++;
                else
                {
                    string message = $"Skill \"{skill[i]}\" is absend in skill list. Do you whant to add new skill?";
                    string caption = "Word Processor";
                    DialogResult result = MessageBox.Show(message, caption, MessageBoxButtons.OKCancel);
                    if (result == DialogResult.OK)
                    {
                        Post post = SearchLinqToSql.GetPost(this.comboBoxAdd.SelectedItem.ToString());
                        AddLinqToSql.AddSkill(skill[i],post.Id);
                        MessageBox.Show("Skill added successfull.");
                    }
                    else throw new ArgumentException();
                    skills[i] = SearchLinqToSql.GetSkill(skill[i]);
                    skillCount++;
                }
            }

            return skills;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            string name = this.deleteBox.Text;
            bool success = DeleteLinqToSql.DeleteEmployee(name);
            if (success) MessageBox.Show("Delete successful.");
            else MessageBox.Show("Match not found.");
            this.deleteBox.Text = "";
        }
    }
}
