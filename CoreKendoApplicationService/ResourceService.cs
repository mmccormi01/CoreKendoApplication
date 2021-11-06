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
            List<int> ClassList = new List<int>();

            using (var context = new Context())
            {

                //Method syntax for Left Outer Joins
                ResourceList = context.Resources                                    //r = Resources
                    .GroupJoin(
                        context.ResourceTypes, 
                        r => r.ResourceTypeId, 
                        t => t.ResourceTypeId, 
                        (r, t) => new { r, t })                                     //t = ResourceTypes
                    .SelectMany(
                        rt => rt.t.DefaultIfEmpty(), 
                        (r, t) => new { r, t })

                    .GroupJoin(
                        context.ResourceClasses, 
                        rt => rt.r.r.ResourceClassId, 
                        c => c.Id, 
                        (r, c) => new { r, c })                                     //c = ResourceClasses
                    .SelectMany(
                        rtc => rtc.c.DefaultIfEmpty(), 
                        (r, c) => new { r, c })  

                    .GroupJoin(
                        context.DesignationStatuses,
                        rtcs => rtcs.r.r.r.r.DesignationStatusId, 
                        s => s.DesignationStatusId,                                 //s = DesignationStatuses
                        (r, s) =>  new { r, s }) 
                    .SelectMany(
                        rtcs => rtcs.s.DefaultIfEmpty(),
                        (r, s) =>  new ResourceRow()

                    {
                        ResourceId = r.r.r.r.r.r.ResourceId,
                        ResourceName = r.r.r.r.r.r.ResourceName,
                        YearDesignated = r.r.r.r.r.r.YearDesignated,
                        ResourceTypeId = r.r.r.r.t.ResourceTypeId,
                        ResourceTypeName = r.r.r.r.t.ResourceTypeName,
                        DesignationStatusId = s.DesignationStatusId,
                        DesignationStatusName = s.DesignationStatusName,
                        GISId = r.r.r.r.r.r.GISId,
                        ModifiedDate = r.r.r.r.r.r.ModifiedDate,

                        ResourceDescription = r.r.r.r.r.r.ResourceDescription,

                        PrimaryASMSiteNumber = r.r.r.r.r.r.PrimaryASMSiteNumber,

                        ParentDistrictId = r.r.r.r.r.r.ParentDistrictId,
                        ParentSensitivityZoneId = r.r.r.r.r.r.ParentSensitivityZoneId

                    })
                    .OrderBy(o => o.ResourceName).ToList();
            }
            //ClassList.Add(1);
            //ClassList.Add(2);
              ResourceList[0].TemporalClasses = new List<int> { 1, 2 };
            //  ResourceList[0].ResourceClasses = ClassList;
            //  ResourceList[3].ResourceClassIds = ClassList;
            return ResourceList;
        }


        public List<ResourceClass> GetResourceClasses()
        {
            List<ResourceClass> ResourceClasses;

            using (var context = new Context())
            {
                ResourceClasses = context.ResourceClasses.OrderByDescending(o => o.Id).ToList();
            }

            return ResourceClasses;
        }

        //public List<ResourceClass> GetResourceClasses()
        //{
        //    LogHelper.Debug("GetResourcesClasses");

        //    List<ResourceClass> ClasssList;
        //    using (var context = new Context())
        //    {
        //        ClasssList = context.ResourceClasses
        //           .Select(s => new ResourceClass
        //           {
        //               Id = s.Id,
        //               Name = s.Name
        //           })
        //           .ToList();
        //    }
        //    return ClasssList;
        //}

        //public List<ResourceClass> GetResourceClasses()
        //{
        //    List<ResourceClass> ClasssList;

        //    using (var context = new Context())
        //    {
        //        ClasssList = (from c in context.ResourceClasses
        //                      select new ResourceClass
        //                      {
        //                          Id = c.Id,
        //                          Name = c.Name
        //                      }).ToList();
        //    }
        //    return ClasssList;
        //}

        //public List<ResourceClass> GetResourceClasses()
        //{
        //    LogHelper.Debug("GetResourcesClasses");

        //    List<ResourceClass> ResourceList = new List<ResourceClass>();

        //    ResourceList.Add(new ResourceClass
        //    {
        //        Id = 3,
        //        Name = "something",
        //    });
        //    ResourceList.Add(new ResourceClass
        //    {
        //        Id = 5,
        //        Name = "something completely different",
        //    });

        //    ResourceList.Add(new ResourceClass
        //    {
        //        Id = 2,
        //        Name = "somethingelse",
        //    });

        //    return ResourceList;
        //}
    }
}
