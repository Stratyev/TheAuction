﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using TheAuction.Models;
using CodeFirst;

namespace TheAuction.Infrastructure
{
    public class AppUserManager : UserManager<AppUser>
    {
        public AppUserManager(IUserStore<AppUser> store)
            : base(store)
        { }

        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options,
            IOwinContext context)
        {
            MyDbContext db = context.Get<MyDbContext>();
            AppUserManager manager = new AppUserManager(new UserStore<AppUser>(db));
            return manager;
        }
    }
}