using System.Web.Mvc;

namespace FUTOMedical.Areas.Pharmacists
{
    public class PharmacistsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Pharmacists";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Pharmacists_default",
                "Pharmacists/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}