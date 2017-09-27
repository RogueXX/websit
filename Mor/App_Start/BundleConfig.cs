using System.Web;
using System.Web.Optimization;

namespace Mor
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));
                
            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            // bootstrap
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.min.js",
                 "~/Content/js/theme.js",
                "~/Scripts/respond.min.js"));


            // 通用脚本
            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                "~/Scripts/knockout-3.3.0.js",
                "~/Scripts/knockout.validation.min.js",
                "~/Scripts/bootstrap-prompts-alert.js",
                "~/Scripts/require.js",
                "~/Scripts/knockout.components.js",
                "~/Scripts/common.js"));
              
            // 页面样式
            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/Content/bootstrap.min.css",
                "~/content/css/bootstrap/bootstrap-theme.css",
                "~/content/css/bootstrap/bootstrap-overrides.css",

                "~/content/css/compiled/layout.css",
                "~/content/css/compiled/elements.css",
                "~/content/css/compiled/table.css",
                //  "~/content/css/compiled/icons.css",
                "~/content/css/lib/bootstrap.datepicker.css",
                "~/Content/font-awesome.min.css",
                "~/Content/site.css"
                )
            );


        }
    }
}