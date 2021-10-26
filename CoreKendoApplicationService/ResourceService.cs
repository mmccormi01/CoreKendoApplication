using System.Collections.Generic;
using System.Linq;
using CoreKendoApplicationService.EntityModels;

namespace CoreKendoApplicationService
{
    public class ResourceService
    {
        public List<ResourceRow> GetResources()
        {
            LogHelper.Debug("GetResources");

            List<ResourceRow> ResourceList;

            using (var context = new Context())
            {
                ResourceList = context.Resources                                                                                //r = Resources
                    .Join(context.ResourceTypes, r => r.ResourceTypeId, t => t.ResourceTypeId, (r, t) => new { r, t })          //t = ResourceTypes
                    .Join(context.DesignationStatuses, rt => rt.r.DesignationStatusId, s => s.DesignationStatusId, (rt, s) => new { rt.r, rt.t, s }) //s = DesignationStatuses
                    .Select(rts => new ResourceRow()
                    {
                        ResourceId = rts.r.ResourceId,
                        ResourceName = rts.r.ResourceName,
                        YearDesignated = rts.r.YearDesignated,
                        ResourceTypeName = rts.t.ResourceTypeName,
                        DesignationStatusName = rts.s.DesignationStatusName,
                        GISId = rts.r.GISId ?? 0,
                        ModifiedDate = rts.r.ModifiedDate != null ? rts.r.ModifiedDate : new System.DateTime(1800, 1, 1),

                        ResourceDescription = rts.r.ResourceDescription,
                        ResourceClassId = rts.r.ResourceClassId,
                        PrimaryASMSite = rts.r.PrimaryASMSiteNumber,
                        OtherSiteNos = rts.r.SecondarySitesId.ToString(),
                        ParentDistrict = rts.r.ParentDistrictId.ToString(),
                        SensitivityZone = rts.r.ParentSensitivityZoneId.ToString()

                    })
                    .OrderBy(o => o.ResourceName).ToList();
            }


            return ResourceList;
        }
        public List<ResourceClass> GetResourceClasses()
        {
            LogHelper.Debug("GetResourcesClasses");

            List<ResourceClass> ResourceList = new List<ResourceClass>();

            ResourceList.Add(new ResourceClass
            {
                Id = 3,
                Name = "something",
                ResourceClassDescription = "description"
            });
            ResourceList.Add(new ResourceClass
            {
                Id = 5,
                Name = "something completely different",
                ResourceClassDescription = "and now for something completely different..."
            });

            ResourceList.Add(new ResourceClass
            {
                Id = 2,
                Name = "somethingelse",
                ResourceClassDescription = "another description"
            });

            return ResourceList;
        }
    }
}
