using Microsoft.AspNetCore.Mvc;
using Udemy_Umbraco_course.ViewModels.Api;
using Umbraco.Cms.Core.Mapping;
using Umbraco.Cms.Web.BackOffice.Controllers;
using UmbracoTutorial.Core.Models.NPoco;
using UmbracoTutorial.Core.Services;

namespace Udemy_Umbraco_course.Controllers.backoffice
{
    public class ContactRequestsApiController : UmbracoAuthorizedApiController
    {
        private readonly IContactRequestService _contactRequestService;
        private readonly IUmbracoMapper _Mapper;
      public ContactRequestsApiController(IContactRequestService contactRequestService, IUmbracoMapper mapper)
        {
            _contactRequestService = contactRequestService;
            _Mapper = mapper;
        }

        public async Task<IActionResult> GetTotalNumber()
        {
            
            return Ok(await _contactRequestService.GetTotalNumber());
        }
        public async Task<IActionResult> GetAll()
        {
            var contacts = await _contactRequestService.GetAll();
            return Ok(_Mapper.MapEnumerable<ContactRequestDBModel, ContactRequestResponseItem>(contacts));
        }
    }
}
