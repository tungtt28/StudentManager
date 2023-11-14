using DataServices.Logics;
using DataServices.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FAP.View
{
    public partial class SubjectCrud : Form
    {
        List<Subject> subjects;
        public SubjectCrud()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            HomePage form1 = new HomePage();
            this.Hide();
            form1.ShowDialog();
            this.Close();
        }

        private void Subjects_Load(object sender, EventArgs e)
        {
            LoadDataForDGV();
        }

        private void LoadDataForDGV()
        {
            SubjectManager subjectManager = new SubjectManager();
            subjects = subjectManager.GetSubjects();
            
                dataGridView1.DataSource = subjects
                    .Select(x => new
                    {
                        x.SubjectId,
                        x.SubjectCode,
                        x.SubjectName,
                        x.Department.DepartmentName,
                    }).ToList();
            
        }

            private void button1_Click(object sender, EventArgs e)
        {
            string code = textBox1.Text;
             
            using (APContext context = new APContext())
            {
                dataGridView1.DataSource = context.Subjects
                    
                    .Where(x => x.SubjectCode.ToLower().Contains(code) || x.SubjectName.ToLower().Contains(code))
                    .Include(x => x.Courses)
                    .Include(x => x.Department)
                    .Select(x => new
                    {

                        x.SubjectId,
                        x.SubjectCode,
                        x.SubjectName,
                        x.Department.DepartmentName,


                    })
                    .ToList();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            Subject currentSubject = subjects[e.RowIndex];
            textBox2.Text = currentSubject.SubjectId.ToString();
            textBox3.Text = currentSubject.SubjectCode.ToString();
            textBox4.Text = currentSubject.SubjectName.ToString();
            textBox5.Text = currentSubject.DepartmentId.ToString();
        }

        private Subject GetSubjectInfo()
        {
            Subject subject = new Subject();
            subject.SubjectCode = textBox3.Text;
            subject.SubjectName = textBox4.Text;
            subject.DepartmentId = Convert.ToInt32(textBox5.Text);

            return subject;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SubjectManager subjectManager = new SubjectManager();
            subjectManager.AddSubject(GetSubjectInfo());
            LoadDataForDGV();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Subject subject = GetSubjectInfo();
            if(textBox2.Text != String.Empty)
            {
                subject.SubjectId = Convert.ToInt32(textBox2.Text);
                
            }
            SubjectManager subjectManager = new SubjectManager();
            subjectManager.Edit(subject);
            LoadDataForDGV();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int subjectId = 0;
            if(textBox2.Text != String.Empty)
            {
                subjectId= Convert.ToInt32(textBox2.Text);
            }
            SubjectManager subjectManager = new SubjectManager();
            subjectManager.Delete(subjectId);
            LoadDataForDGV();
        }
    }
}

