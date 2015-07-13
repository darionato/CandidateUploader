using System.Web.Optimization;

namespace CandidateUploader.App_Start
{
    public class BundleConfig
    {

        public static void RegisterBundles(BundleCollection bundles)
        {


            // jquery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));




            // angular js
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/angular.js"));



            // bootstrap
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.js"));



            // angular file upload
            bundles.Add(new ScriptBundle("~/bundles/angularfileupload").Include(
                        "~/Scripts/angular-file-upload.js"));




            // angular file upload shim
            bundles.Add(new ScriptBundle("~/bundles/angularfileuploadshim").Include(
                        "~/Scripts/angular-file-upload-shim.js"));


        }

    }
}