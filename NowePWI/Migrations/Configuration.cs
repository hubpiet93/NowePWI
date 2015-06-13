using NowePWI.DAL;

namespace NowePWI.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<NowePWI.DAL.mydbcontext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(NowePWI.DAL.mydbcontext context)
        {
          //  PhotosDbInitializer.SeedPhotos(context);
            
        }
    }
}
