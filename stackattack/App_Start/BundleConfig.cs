using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;

namespace stackattack
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/src/app/bower_components/jquery/dist/jquery.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/src/app/bower_components/angular/angular.js",
                "~/Scripts/src/app/bower_components/angular-route/angular-route.js"));


            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/src/app/app.module.js",
                "~/Scripts/src/app/core/core.module.js",
                "~/Scripts/src/app/core/question/question.module.js",
                "~/Scripts/src/app/core/question/question.service.js",
                "~/Scripts/src/app/core/user/user.module.js",
                "~/Scripts/src/app/core/user/user.service.js",
                "~/Scripts/src/app/game-overview/game-overview.module.js",
                "~/Scripts/src/app/game-overview/game-overview.component.js",
                "~/Scripts/src/app/question-detail/question-detail.module.js",
                "~/Scripts/src/app/question-detail/question-detail.component.js",
                "~/Scripts/src/app/question-list/question-list.module.js",
                "~/Scripts/src/app/question-list/question-list.component.js",
                "~/Scripts/src/app/recent-question-list/recent-question-list.module.js",
                "~/Scripts/src/app/recent-question-list/recent-question-list.component.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                 "~/Content/bootstrap.css",
                 "~/Content/Site.css"));
        }
    }
}
