using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StadionStats.ViewModels
{
    public class BrugerRollerViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsSelected { get; set; }
    }
}
