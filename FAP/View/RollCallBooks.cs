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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FAP.View
{
    public partial class RollCallBooks : Form
    {
        public RollCallBooks()
        {
            InitializeComponent();
        }

        private void RollCallBooks_Load(object sender, EventArgs e)
        {
            List<RollCallBooks> rollCallBooks = new List<RollCallBooks>();
            using (APContext context = new APContext())
            {
                dataGridView1.DataSource = context.RollCallBooks
                    .Include(x => x.Student)
                    .Include(x => x.TeachingSchedule)
                    .Select(x => new
                    {
                        x.Student.Roll,
                        StudentName = x.Student.LastName + " " + x.Student.MidName + " " + x.Student.FirstName,
                        Date = x.TeachingSchedule.TeachingDate,
                        Slot =  x.TeachingSchedule.Slot,
                        Room =  x.TeachingSchedule.Room.RoomCode,
                        Lecturer =  x.TeachingSchedule.Course.Instructor.InstructorLastName +" "+ x.TeachingSchedule.Course.Instructor.InstructorMidName + " " + x.TeachingSchedule.Course.Instructor.InstructorFirstName,
                        x.TeachingSchedule.Course.CourseCode,
                        x.TeachingSchedule.Course.Instructor.Department.DepartmentName,
                        x.TeachingSchedule.Course.Subject.SubjectCode,
                        AttedanceStatus =x.IsAbsent

                    })
                    .ToList();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            HomePage form1 = new HomePage();
            this.Hide();
            form1.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                string code = textBox1.Text;
                using (APContext context = new APContext())
                {
                    dataGridView1.DataSource = context.RollCallBooks
                        .Where(x => x.Student.FirstName == code || x.Student.MidName == code || x.Student.LastName == code)
                        .Include(x => x.Student)
                        .Include(x => x.TeachingSchedule)
                        .Select(x => new
                        {
                            x.Student.Roll,
                            StudentName = x.Student.LastName + " " + x.Student.MidName + " " + x.Student.FirstName,
                            Date = x.TeachingSchedule.TeachingDate,
                            Slot = x.TeachingSchedule.Slot,
                            Room = x.TeachingSchedule.Room.RoomCode,
                            Lecturer = x.TeachingSchedule.Course.Instructor.InstructorLastName + " " + x.TeachingSchedule.Course.Instructor.InstructorMidName + " " + x.TeachingSchedule.Course.Instructor.InstructorFirstName,
                            x.TeachingSchedule.Course.CourseCode,
                            x.TeachingSchedule.Course.Instructor.Department.DepartmentName,
                            x.TeachingSchedule.Course.Subject.SubjectCode,
                            AttedanceStatus = x.IsAbsent


                        })
                        .ToList();
                }
            
        }

        
    }
}
