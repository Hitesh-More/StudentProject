using StudentProject.ModelStudent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentProject.Models
{
    public class MapCourseAndSubjectViewModel
    {
        //added due to mapping
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public virtual Course Course { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public virtual Subject Subject { get; set; }
        public List<Subject> SubjectList { get; set; }
         public List<Course> CourseList { get; set; }
    }
}