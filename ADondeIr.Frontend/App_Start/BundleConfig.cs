namespace ADondeIr.Frontend
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region DataTables
            //Styles
            var dataTablesStyles = new StyleBundle("~/Styles/DataTables");
            dataTablesStyles.Include("~/Content/plugins/dataTables/css/dataTables.bootstrap.min.css");
            bundles.Add(dataTablesStyles);

            var dataTablesSelectedStyles = new StyleBundle("~/Styles/DataTablesSelected");
            dataTablesSelectedStyles.Include("~/Content/plugins/dataTables/css/dataTables.bootstrap.min.css",
                "~/Content/plugins/dataTables/plugins/selected/select.dataTables.min.css");
            bundles.Add(dataTablesSelectedStyles);

            //Scripts
            var dataTablesScripts = new ScriptBundle("~/Scripts/DataTables");
            dataTablesScripts.Include("~/Content/plugins/dataTables/js/jquery.dataTables.min.js",
                "~/Content/plugins/dataTables/js/dataTables.bootstrap.min.js",
                "~/Content/plugins/dataTables/plugins/buttons/dataTables.buttons.min.js",
                "~/Content/plugins/dataTables/plugins/buttons/buttons.flash.min.js",
                "~/Content/plugins/dataTables/plugins/buttons/jszip.min.js",
                "~/Content/plugins/dataTables/plugins/buttons/buttons.html5.min.js",
                "~/Content/js/dataTables.config.min.js");
            bundles.Add(dataTablesScripts);

            var dataTablesExportScripts = new ScriptBundle("~/Scripts/DataTablesExport");
            dataTablesExportScripts.Include("~/Content/plugins/dataTables/js/jquery.dataTables.min.js",
                "~/Content/plugins/dataTables/js/dataTables.bootstrap.min.js",
                "~/Content/plugins/dataTables/plugins/buttons/dataTables.buttons.min.js",
                "~/Content/plugins/dataTables/plugins/buttons/buttons.flash.min.js",
                "~/Content/plugins/dataTables/plugins/buttons/jszip.min.js",
                "~/Content/plugins/dataTables/plugins/buttons/buttons.html5.min.js",
                "~/Content/js/dataTables.config.min.js");
            bundles.Add(dataTablesExportScripts);

            var dataTablesSelectedScripts = new ScriptBundle("~/Scripts/dataTablesSelected");
            dataTablesSelectedScripts.Include("~/Content/plugins/dataTables/js/jquery.dataTables.min.js",
                "~/Content/plugins/dataTables/js/dataTables.bootstrap.min.js",
                "~/Content/plugins/dataTables/plugins/selected/dataTables.select.min.js",
                "~/Content/js/dataTables.config.min.js");
            bundles.Add(dataTablesSelectedScripts);
            #endregion

            #region Validation
            var validationScripts = new ScriptBundle("~/Scripts/Validation");
            validationScripts.Include("~/Content/plugins/Jquery-Validation/jquery.validate.min.js",
                "~/Content/plugins/Jquery-Validation-unobtrusive/jquery.validate.unobtrusive.min.js",
                "~/Content/plugins/jquery-unobtrusive-ajax/jquery.unobtrusive-ajax.min.js",
                "~/Content/plugins/MvcFoolProof/mvcfoolproof.unobtrusive.min.js");
            bundles.Add(validationScripts);
            #endregion

            #region DataPicker
            var datePickerStyles = new StyleBundle("~/Styles/DatePicker");
            datePickerStyles.Include("~/Content/plugins/Datepicker/datepicker3.css");
            bundles.Add(datePickerStyles);


            var datePickerScripts = new ScriptBundle("~/Scripts/DatePicker");
            datePickerScripts.Include("~/Content/plugins/Datepicker/bootstrap-datepicker.js",
                "~/Content/plugins/Datepicker/locales/bootstrap-datepicker.es.js",
                "~/Content/js/bootstrap-datepicker.config.js");
            bundles.Add(datePickerScripts);
            #endregion

            BundleTable.EnableOptimizations = true;
        }
    }
}