using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using CodeFirst;
using Microsoft.AspNet.Identity;

namespace TheAuction.Models.HubModels
{
    //An alternative to the AppUserManager.Create():

    //class AppUserManager : UserManager<AppUser>
    //{
    //    public AppUserManager(MyDbContext Mdb)
    //        : this(new UserStore<AppUser>(Mdb))
    //    {
    //    }
    //    public AppUserManager(IUserStore<AppUser> userStore)
    //       : base(userStore)
    //    {
    //    }
    //}
    class HubAppUserManager : UserManager<AppUser>
    {
        public HubAppUserManager(IUserStore<AppUser> userStore)
           : base(userStore)
        {
        }

        public static HubAppUserManager Create(MyDbContext Mdb)
        {
            return new HubAppUserManager(new UserStore<AppUser>(Mdb));
        }
    }
}