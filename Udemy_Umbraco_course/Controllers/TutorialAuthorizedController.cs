using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.BackOffice.Controllers;
using Umbraco.Cms.Web.Common.Controllers;

namespace UmbracoTutorial.Controllers
{

    //UmbracoAuthorizedController
    //UmbracoAuthorizedApiController
    //UmbracoAuthorizedJsonController
    public class TutorialAuthorizedController : UmbracoAuthorizedJsonController
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}

