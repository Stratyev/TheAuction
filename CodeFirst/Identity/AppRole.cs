using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CodeFirst
{
    public class AppRole : IdentityRole
    {
        public AppRole() : base() { }

        public AppRole(string name)
            : base(name)
        { }
    }
}
