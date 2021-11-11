using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CoreKendoApplicationService;
using CoreKendoApplicationService.EntityModels;
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
        public string GetResources()
        {
            LogHelper.Debug("Requesting Resource Row Data from Service.");
            List<ResourceRow> rows = ResourceService.GetResources();
            //  JArray jArray = new JArray();
            var rval = JsonConvert.SerializeObject(rows);

            //string rval = ConvertListToJson(rows);
            //   jArray.Add(2);
            //JObject jObj = new JObject(
            //    new JProperty("ResourceId", 1),
            //    new JProperty("ResourceName", "49er's Site"),
            //    new JProperty("YearDesignated", 2004),
            //    new JProperty("ResourceTypeName", "Building"),
            //    new JProperty("ResourceClasses", jArray),

            //    new JProperty("DesignationStatusName", null),
            //    new JProperty("GISId", 2564),
            //    new JProperty("ModifiedDate", "2021-09-01T00:00:00"),
            //    new JProperty("ResourceDescription", "49er's Site Description"),
            //    new JProperty("PrimaryASMSite", "AZ BB:14:17(ASM)"),
            //    new JProperty("ParentDistrict", null),
            //    new JProperty("SensitivityZone", null));

            return rval;  //kendo only likes flat deserialized json string
            //return new JsonResult(JsonConvert.SerializeObject(tmp));
        }

        //[AuthorizeRoles(AccessLevel = HTTP_ROLE_GROUP_ALL)]
        //public List<ResourceRow> GetResources()
        //{
        //    LogHelper.Debug("Requesting Resource Row Data from Service.");

        //    return ResourceService.GetResources();
        //}

        [HttpPost]
        [Route("UpdateResource")]
        [AuthorizeRoles(AccessLevel = HTTP_ROLE_GROUP_ALL)]
        public IActionResult UpdateResource([FromBody] ResourceRow resourceRow)
        {
            JObject jObj = new JObject(new JProperty("rval", "return value"));
            LogHelper.Debug("Updating Resource Row Data from Grid.");
            //ResourceRow.ModifiedBy = User.Identity.Name;
            //ResourceService.UpdateResource(ResourceRow);

            return new JsonResult(jObj, null);
        }
        [HttpPost]
        [Route("CreateResource")]
        [AuthorizeRoles(AccessLevel = HTTP_ROLE_GROUP_ALL)]
        public void CreateResource(ResourceRow resourceRow)
        {
            LogHelper.Debug("Creating Resource Row Data from Grid.");
            //ResourceRow.ModifiedBy = User.Identity.Name;
            //ResourceService.CreateResource(ResourceRow);
        }

        [HttpGet]
        [Route("DeactivateResource")]
        [AuthorizeRoles(AccessLevel = HTTP_ROLE_GROUP_ALL)]
        public void DeactivateResource(int resourceId)
        {
            LogHelper.Debug("Deactivating Resource Row Data from Grid (Setting to inactive).");

            //ResourceService.SetResourceActiveStatus(ResourceID, User.Identity.Name, false);
        }

        [HttpGet]
        [Route("GetResourceTypes")]
        public List<ResourceType> GetResourceTypes()
        {
            LogHelper.Debug("Requesting Resource Row Data from Service.");
            return ResourceService.GetResourceTypes();

        }

        [HttpGet]
        [Route("GetResourceClasses")]
        public List<ResourceClass> GetResourceClasses()
        {
            LogHelper.Debug("Requesting Resource Row Data from Service.");
            return ResourceService.GetResourceClasses();

        }

        [HttpGet]
        [Route("GetDesignationStatuses")]
        public List<DesignationStatus> GetDesignationStatuses()
        {
            LogHelper.Debug("Requesting Resource Row Data from Service.");
            return ResourceService.GetDesignationStatuses();

        }


        [HttpGet]
        [Route("GetParentDistricts")]
        public List<ParentDistrict> GetParentDistricts()
        {
            LogHelper.Debug("Requesting Resource Row Data from Service.");
            return ResourceService.GetParentDistricts();

        }
        private string ConvertListToJson(List<ResourceRow> resourceRows)
        { 
            //Array jArr = new JArray();
            JObject jobj = new JObject();
            List<string> jString = new List<string>();

            foreach (ResourceRow row in resourceRows)
            {
                jobj = new JObject(
                    new JProperty("ResourceId", row.ResourceId),
                    new JProperty("ResourceName", row.ResourceName),
                    new JProperty("YearDesignated", row.YearDesignated),
                    new JProperty("ResourceTypeName", row.ResourceTypeName),
                    new JProperty("ResourceClassId", row.ResourceClassId,

                    new JProperty("DesignationStatusName", row.DesignationStatusName),
                    new JProperty("GISId", row.GISId),
                    new JProperty("ModifiedDate", row.ModifiedDate),
                    new JProperty("ResourceDescription", row.ResourceDescription),
                    new JProperty("PrimaryASMSiteNumber", row.PrimaryASMSiteNumber),
                    new JProperty("ParentDistrictId", row.ParentDistrictId),
                    new JProperty("ParentSensitivityZoneId", row.ParentSensitivityZoneId)));

                jString.Add(jobj.ToString());
            }

            return jString.ToString();
        }
    }
}