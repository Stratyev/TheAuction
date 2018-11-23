﻿
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using TheAuction.Models;
using CodeFirst;

namespace TheAuction.Infrastructure
{
    public class AppRoleManager : RoleManager<AppRole>, IDisposable
    {
        public AppRoleManager(RoleStore<AppRole> store)
            : base(store)
        { }

        public static AppRoleManager Create(
            IdentityFactoryOptions<AppRoleManager> options,
            IOwinContext context)
        {
            return new AppRoleManager(new
                RoleStore<AppRole>(context.Get<MyDbContext>()));
        }
    }
}