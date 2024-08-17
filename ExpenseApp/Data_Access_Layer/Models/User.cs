using Data_Access_Layer.helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [EmailAddress(ErrorMessage ="Username should be the Email Address!")]
        public string Username { get; set; }


        [Required(ErrorMessage ="Password is Required")]
        [PasswordValidation(ErrorMessage="Password length must be between 8 to 12 digits long inclusive and must include atleast one uppercase letter, one lowercase letter, one special character and one number")]
        public string Password { get; set; }

        [Required(ErrorMessage ="Role is required")]
        [RegularExpression("^(Administrator|User)", ErrorMessage ="Role must be either 'Administrator' or 'User'")]
        public string Role { get; set; }
    }
}
