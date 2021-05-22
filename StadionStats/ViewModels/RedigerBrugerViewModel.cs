﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StadionStats.ViewModels
{
    public class RedigerBrugerViewModel
    {
        public RedigerBrugerViewModel()
        {
            Roles = new List<string>();
        }

        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public IList<string> Roles { get; set; }
    }
}
