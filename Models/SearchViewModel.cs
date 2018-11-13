using StudentProject.ModelStudent;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentProject.Models
{
    public class SearchFilterViewModel
    {

        public string Fname { get; set; }

        public string Lname { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public Nullable<System.DateTime> DateOfBirth { get; set; }

        public string Hobbies { get; set; }

        public string Email { get; set; }
        
        public string Gender { get; set; }

        public int AddressId { get; set; }

        public bool IsVerified { get; set; }

        public bool IsActive { get; set; }

        public System.DateTimeOffset DateCreated { get; set; }
        
        public string UserAddress { get; set; }

      public List<Address> Address { get; set; }
        public string ZipCode { get; set; }
        
        public int CountryId { get; set; }
        //public string CountryName { get; set; }
        public List<Country> CountryList { get; set; }

        public int? StateId { get; set; }
        //public string StateName { get; set; }
        public List<State> StateList { get; set; }

        public int? CityId { get; set; }
        //public string CityName { get; set; }
        public List<City> CityList { get; set; }

        public int? CourseId { get; set; }
        public List<Course> CourseList { get; set; }

        public int? RoleId { get; set; }
        public List<Role> RoleList { get; set; }

        public List<SearchGridViewModel> SearchGridViewModelList { get; set; }
    }
    public class SearchGridViewModel
        {
        public string Fname { get; set; }

        public string Lname { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public Nullable<System.DateTime> DateOfBirth { get; set; }

        public string Hobbies { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }

        public bool IsVerified { get; set; }

        public bool IsActive { get; set; }

        public System.DateTimeOffset DateCreated { get; set; }

        public string UserAddress { get; set; }

        public string ZipCode { get; set; }

        public String RoleName { get; set; }

        public string CityName { get; set; }

        public string CountryName { get; set; }
      
        //public string StateName { get; set; }

    }



}