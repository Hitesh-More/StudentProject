using StudentProject.ModelStudent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentProject.Models
{
    public class AssignSubjectToTeacher
    { 
        public string Email { get; set; }
         public System.Guid UserId { get; set; }
    public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public List<Subject> SubjectList { get; set; }
    }

}
