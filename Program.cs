﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MrsTaster.Data;

namespace MrsTaster
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            try
            {



                var scope = host.Services.CreateScope();

                var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                ctx.Database.EnsureCreated();

                var adminRole = new IdentityRole("Admin");
                if (!ctx.Roles.Any())
                {
                    //create a role
                    roleMgr.CreateAsync(adminRole).GetAwaiter().GetResult();
                }

                if (!ctx.Users.Any(u => u.UserName == "parang"))
                {
                    //create an admin
                    var adminUser = new IdentityUser
                    {
                        UserName = "parang"
                    };
                    var result = userMgr.CreateAsync(adminUser, "yursbll").GetAwaiter().GetResult();
                    //add role to user
                    userMgr.AddToRoleAsync(adminUser, adminRole.Name).GetAwaiter().GetResult();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

                host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
