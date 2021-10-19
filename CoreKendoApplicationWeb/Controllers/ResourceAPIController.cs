using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CoreKendoApplicationService;
using CoreKendoApplicationWeb.Identity;
using static CoreKendoApplicationWeb.Identity.HttpAuthRoleGroupConstants;

namespace CoreKendoApplicationWeb
{
    //[Route("api/Default")]
    //[ApiController]
    //public class ResourceAPIController : ControllerBase
    //{
    //    public ResourceAPIController()
    //    {
    //        LogHelper.Debug("Instantiating DefaultAPIController");
    //    }

    //    [HttpGet]
    //    [Route("GetHelloWorld")]
    //    public string GetHelloWorld(string name)
    //    {
    //        LogHelper.Debug("Name is " + name);

    //        ResourceService defaultService = new ResourceService();
    //        return "Anything..."; // defaultService.GetHelloWorld(name);
    //    }
    //}

    [Route("api/Resource")]
    [ApiController]
    public class ResourceApiController : ControllerBase
    {
        //private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //private readonly AppAdminService appAdminService = new AppAdminService();
        //private readonly ProjectService projectService = new ProjectService();
        //private readonly AgencyService agencyService = new AgencyService();

        private readonly ResourceService ResourceService = new ResourceService();

        public ResourceApiController()
        {
            LogHelper.Debug("Instantiating ResourceAPIController");
        }

        [HttpGet]
        [Route("GetResources")]
    //    [AuthorizeRolesAttribute(Roles = HTTP_ROLE_GROUP_ALL)]
        public List<ResourceRow> GetResources()
        {
            LogHelper.Debug("Requesting Resource Row Data from Service.");

            return ResourceService.GetResources();
        }

        [HttpPost]
        [Route("UpdateResource")]
        [AuthorizeRolesAttribute(Roles = HTTP_ROLE_GROUP_ALL)]
        public void UpdateResource(ResourceRow ResourceRow)
        {
            LogHelper.Debug("Updating Resource Row Data from Grid.");
            //ResourceRow.ModifiedBy = User.Identity.Name;
            //ResourceService.UpdateResource(ResourceRow);
        }

        [HttpPost]
        [Route("CreateResource")]
        [AuthorizeRolesAttribute(Roles = HTTP_ROLE_GROUP_ALL)]
        public void CreateResource(ResourceRow ResourceRow)
        {
            LogHelper.Debug("Creating Resource Row Data from Grid.");
            //ResourceRow.ModifiedBy = User.Identity.Name;
            //ResourceService.CreateResource(ResourceRow);
        }

        [HttpGet]
        [Route("DeactivateResource")]
        [AuthorizeRolesAttribute(Roles = HTTP_ROLE_GROUP_ALL)]
        public void DeactivateResource(int ResourceID)
        {
            LogHelper.Debug("Deactivating Resource Row Data from Grid (Setting to inactive).");

            //ResourceService.SetResourceActiveStatus(ResourceID, User.Identity.Name, false);
        }
    }
}