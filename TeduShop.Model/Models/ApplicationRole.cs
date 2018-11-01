﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeduShop.Model.Models
{
    public class ApplicationRole : IdentityRole
    {
        // da co key do tu IdentityRole
        public ApplicationRole() : base()
        {

        }

        [StringLength(250)]
        public string Description { get; set; }
    }
}
