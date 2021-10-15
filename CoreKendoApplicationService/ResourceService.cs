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
                ResourceList = context.Resources
                    .Join(context.ResourceTypes, p => p.ResourceTypeId, t => t.ResourceTypeId, (p, t) => new { Resources = p, ResourceTypes = t })
                    //.Where(w => w.Projects.Active)
                    .Select(s => new ResourceRow()
                    {
                        ResouceId = s.Resources.ResourceId,
                        ResourceName = s.Resources.ResourceName ?? "",          
                        GISID = s.Resources.GISId,
                        LastModified = (s.Resources.ModifiedDate == null).ToString(),
                        ResourceTypeId = s.Resources.ResourceTypeId,
                      //  DocumentFilePath = s.Projects.DocumentFilePath ?? ""

                    })
                    .OrderByDescending(o => o.ResourceName)
                    .ToList();
            }

            return ResourceList;
        }
    }
}
