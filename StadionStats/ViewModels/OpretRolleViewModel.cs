using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StadionStats.ViewModels
{
    public class OpretRolleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
