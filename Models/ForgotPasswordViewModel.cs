using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentProject.Models
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Pin is required")]
        [Display(Name = "Security Pin")]
        [RegularExpression("[^0-9]", ErrorMessage = "Pin must be numeric")]
        [DataType(DataType.Password)]
        public int SecurityPIN { get; set; }

        [Required(ErrorMessage = "Email is required")]
      [EmailAddress]
        public string Email { get; set; }
    }
}