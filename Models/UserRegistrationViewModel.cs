using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentProject.ModelStudent;

namespace StudentProject.Models
{
    public class UserRegistrationViewModel
    {

        public System.Guid UserId { get; set; }

        [Required()]
        [Display(Name = "First name")]
        [StringLength(100, MinimumLength = 3)]
        public string Fname { get; set; }


        [Required()]
        [Display(Name = "Last name")]
        [StringLength(100, MinimumLength = 4)]
        public string Lname { get; set; }

        
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public Nullable<System.DateTime> DateOfBirth { get; set; }

        [Display(Name = "Hobbies")]
        public string Hobbies { get; set; }

        [Required()]
        [Display(Name = "Email Address ")]
        [StringLength(200, MinimumLength = 5)]
        [EmailAddress]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please Enter your valid email which contains the @ Sign")]
        public string Email { get; set; }

        [Required]
        [StringLength(18, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        
        public string Gender { get; set; }

        public bool IsVerified { get; set; }

       
        public bool IsActive { get; set; }


        public System.DateTimeOffset DateCreated { get; set; }

        public Nullable<System.DateTimeOffset> DateModified { get; set; }

        public List<UserRegistration> UserRegistrations { get; set; }


        /// <summary>
        /// from now properties of address table are added
        /// </summary>
        public int AddressId { get; set; }

        [Display(Name = "Current Address")]
        public string CurrentAddress { get; set; }

        [Display(Name = "Permanent Address")]
        public string PermanentAddress { get; set; }

        public int CountryId { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; }

        public int StateId { get; set; }
        public IEnumerable<SelectListItem> States { get; set; }
       
        public int CityId { get; set; }
        public IEnumerable<SelectListItem> Cities { get; set; }

        public string ZipCode { get; set; }

        public virtual City City { get; set; }

        public virtual Country Country { get; set; }

        public virtual State State { get; set; }

        /// <summary>
        /// this below list are used in drop down list
        /// </summary>
        public List<Country> CountryList { get; set; }
        public string CountryName { get; set; }
        public List<State> StateList { get; set; }
        public string StateName { get; set; }
        public List<City> CityList { get; set; }
        public string CityName { get; set; }
        /// <summary>
        /// this is for course dropdown
        /// </summary>
        /// <param name="v"></param>
       
        public int? CourseId { get; set; }
        public string CourseName { get; set; }
        public List<Course> CourseList { get; set; }

        /// <summary>
        /// this is for Role dropdown
        /// </summary>
        /// <param name="v"></param>
        [Required]
        public int? RoleId { get; set; }

        
        public string RoleName { get; set; }

        public List<Role> RoleList { get; set; }

        public static explicit operator UserRegistrationViewModel(UserRegistration v)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// subjects
        /// </summary>
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public List<Subject> SubjectList { get; set; }

        [Display(Name = "Security Pin")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]

        public int SecurityPIN { get; set; }
    }
  

}