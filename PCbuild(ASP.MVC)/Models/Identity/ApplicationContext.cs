﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace PCbuilder_ASP.MVC_.Models.Identity
{
    public class ApplicationContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext():base("IdentityDb"){}

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }
    }
}