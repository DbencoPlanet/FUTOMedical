using System.Web.Mvc;

namespace FUTOMedical.Areas.Laboratorist
{
    public class LaboratoristAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Laboratorist";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Laboratorist_default",
                "Laboratorist/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}