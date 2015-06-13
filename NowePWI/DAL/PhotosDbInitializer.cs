using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using NowePWI.Migrations;

namespace NowePWI.DAL
{
    public class PhotosDbInitializer : MigrateDatabaseToLatestVersion<mydbcontext, Configuration>
    {
        public static void SeedPhotos(mydbcontext context)
        {

        }
    }
}