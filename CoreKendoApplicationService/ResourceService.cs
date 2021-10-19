using System.Collections.Generic;
using System.Linq;

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
                        ResourceDescription = rts.r.ResourceDescription,
                        YearDesignated = rts.r.YearDesignated,
                        ResourceTypeName = rts.t.ResourceTypeName,
                        DesignationStatusName = rts.s.DesignationStatusName,
                        GISId = rts.r.GISId ?? 0,
                        ModifiedDate = rts.r.ModifiedDate != null ? rts.r.ModifiedDate : new System.DateTime(1800, 1, 1),
                    })
                    .OrderBy(o => o.ResourceName).ToList();
            }


            return ResourceList;
        }
    }
}
