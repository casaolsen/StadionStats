using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace StadionstatsApi.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public bool IsValidAddress(string Email)
        {
            Regex regex = new Regex(@"^[\w0-9._%+-]+@[\w0-9.-]+\.[\w]{2,6}$");
            return regex.IsMatch(Email);
        }

    }

}
