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
    public partial class Courses : Form
    {

        List<Course> courses;
        public Courses()
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

        

        private void Courses_Load(object sender, EventArgs e)
        {
            //List<RollCallBooks> rollCallBooks = new List<RollCallBooks>();
            //using (APContext context = new APContext())
            //{
            //    dataGridView1.DataSource = context.RollCallBooks
            //        .Include(x => x.Student)
            //        .Include(x => x.TeachingSchedule)
            //        .Select(x => new
            //        {

            //            Subject = x.TeachingSchedule.Course.Subject.SubjectId,
            //            SubjectCode = x.TeachingSchedule.Course.Subject.SubjectCode,
            //            SubjectName = x.TeachingSchedule.Course.Subject.SubjectName,
            //            Class = x.TeachingSchedule.Course.CourseCode,
            //            Slot = x.TeachingSchedule.Slot,
            //            Room = x.TeachingSchedule.Room.RoomCode,
            //            Lecturer = x.TeachingSchedule.Course.Instructor.InstructorLastName + " " + x.TeachingSchedule.Course.Instructor.InstructorMidName + " " + x.TeachingSchedule.Course.Instructor.InstructorFirstName,
            //        })
            //        .ToList();
            //}

            CourseManager courseManager = new CourseManager();
            courses = courseManager.GetCourses();
            dataGridView1.DataSource = courses

                .Select(x => new
                {
                    x.CourseId,
                    x.CourseCode,
                    x.Subject.SubjectCode,
                    Subject = x.Subject.SubjectName,  
                    InstructorName = x.Instructor.InstructorFirstName + " " + x.Instructor.InstructorLastName,
                    Term = x.Term.TermName,
                    Campus = x.Campus.CampusName
                }).ToList();
        }
    }

                        
}
