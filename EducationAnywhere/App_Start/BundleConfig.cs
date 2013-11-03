using System.Web;
using System.Web.Optimization;

namespace EducationAnywhere
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapjs").Include("~/Scripts/bootstrap/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/angularjs").Include(
                    "~/Scripts/angular/angular.min.js",
                    "~/Scripts/angular/angular-bootstrap.min.js",
                    "~/Scripts/angular/angular-bootstrap-prettify.min.js",
                    "~/Scripts/angular/angular-bootstrap.min.js",
                    "~/Scripts/angular/angular-cookies.min.js",                    
                    "~/Scripts/angular/angular-loader.min.js",
                    "~/Scripts/angular/angular-mocks.js",
                    "~/Scripts/angular/angular-resource.min.js",
                    "~/Scripts/angular/angular-sanitize.min.js",
                    "~/Scripts/angular/angular-scenario.js"                    
                    ));

            #region ANGULAR CTRL

            bundles.Add(
                new ScriptBundle("~/bundles/angularjsApp").Include(
                    "~/Scripts/angular/app/app.js"
                ));

            bundles.Add(
                new ScriptBundle("~/bundles/angularjsCtrl").Include(
                    "~/Scripts/angular/Controller/*Ctrl.js"
                ));

            #endregion

            bundles.Add(new ScriptBundle("~/Content/bootstrapcss").Include(
                "~/Content/bootstrap/bootstrap.min.css",
                "~/Content/bootstrap/jumbotron.css", 
                "~/Content/bootstrap/bootstrap-responsive.min.css"));
        }
    }
}