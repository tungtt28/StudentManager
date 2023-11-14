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

namespace FAP.View
{
    public partial class StudentInfo : Form
    {
        List<Student> students;
        public StudentInfo()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            HomePage form1 = new HomePage();
            this.Hide();
            form1.ShowDialog();
            this.Close();
        }

        private void StudentInfo_Load(object sender, EventArgs e)
        {
            
            LoadDataForDGV();
            

        }

        private void LoadDataForDGV()
        {
            
            StudentManager studentManager = new StudentManager();
            students = studentManager.GetStudents();
            using (APContext context = new APContext())
            {
                dataGridView1.DataSource = context.Students

                    .Select(x => new
                    {
                        x.StudentId,
                        RollNumber = x.Roll,
                        StudentName = x.LastName + " " + x.MidName + " " + x.FirstName
                    })
                    .ToList();
                dataGridView1.Columns[2].Width = 160;

            }
        }

        private Student GetStudentInfo() { 
            Student student = new Student();
            student.Roll = textBox2.Text.Trim();   
            student.LastName = textBox3.Text.Trim();
            student.MidName = textBox4.Text.Trim();
            student.FirstName = textBox5.Text.Trim();
            return student;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            Student currentStudent = students[e.RowIndex];
            textBox1.Text = currentStudent.StudentId.ToString();
            textBox2.Text = currentStudent.Roll;
            textBox3.Text = currentStudent.LastName;
            textBox4.Text = currentStudent.MidName;
            textBox5.Text = currentStudent.FirstName;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Student student = GetStudentInfo();
            if (textBox1.Text != String.Empty)
                student.StudentId = Convert.ToInt32(textBox1.Text);
            StudentManager studentManager = new StudentManager();

            studentManager.Edit(student);
            LoadDataForDGV();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StudentManager studentManager = new StudentManager();
            studentManager.AddStudent(GetStudentInfo());
            LoadDataForDGV();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int studentId = 0;
            if (textBox1 .Text != String.Empty)
                studentId = Convert.ToInt32(textBox1.Text);
            StudentManager studentManager = new StudentManager();
            studentManager.Delete(studentId);
            LoadDataForDGV();
        }
    }
}
