using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StadionStats.ViewModels
{
    public class RedigerRollerViewModel
    {
        public RedigerRollerViewModel()
        {
            Users = new List<string>();
        }

        public string Id { get; set; }

        [Required(ErrorMessage = "Rollenavn skal udfyldes")]
        public string RoleName { get; set; }

        public List<string> Users { get; set; }
    }
}
