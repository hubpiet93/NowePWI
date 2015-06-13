using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using NowePWI.Models;

namespace NowePWI.DAL
{
    public class mydbcontext : IdentityDbContext<ApplicationUser>
    {
        public mydbcontext()
            : base("mydbcontext")
        {

        }

        static mydbcontext()
        {
            //Database.SetInitializer<mydbcontext>(new PhotosDbInitializer());
        }

        public static mydbcontext Create()
        {
            return new mydbcontext();
        }

        public DbSet<MojaEncja> MojaEncja { get; set; }     
    }
}