using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace NowePWI
{
    public class Bundle
    {
        public static void RegisterBundle(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/Bundle/css")
                .Include(
                        "~/Content/bootstrap.min.css",
                        "~/MyContent/Moje.css")
                );

            bundles.Add(new ScriptBundle("~/Scripts/Bundle/js")
                .Include(
                    "~/Scripts/jquery-2.1.4.min.js",
                    "~/Scripts/bootstrap.min.js")
                );
        }
    }
}