﻿using System.Web.Mvc;

namespace Mor.Web.Areas.Site
{
    public class SiteAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Site";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Site_default",
                "Site/{controller}/{action}/{id}",
                new { action = "Index",Controller="Index", id = UrlParameter.Optional }
            );
        }
    }
}
