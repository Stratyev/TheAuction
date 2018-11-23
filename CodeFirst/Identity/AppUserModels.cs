using System;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace CodeFirst
{
    public class AppUser : IdentityUser
    {
        public DateTime Reg_date { get; set; }
        public List<Figure> Figures { get; set; }
    }
}
