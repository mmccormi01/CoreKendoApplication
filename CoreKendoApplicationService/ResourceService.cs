﻿using System.Collections.Generic;
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

                ResourceList = (from rm in context.Resources
                                join rt in context.ResourceTypes on
                                     rm.ResourceTypeId equals rt.ResourceTypeId into typeJoin
                                from rt in typeJoin.DefaultIfEmpty()
                                join ds in context.DesignationStatuses on
                                     rm.DesignationStatusId equals ds.DesignationStatusId into statusJoin
                                from ds in statusJoin.DefaultIfEmpty()
                                join rc in context.ResourceClasses on
                                     rm.ResourceClassId equals rc.Id into classJoin
                                from rc in classJoin.DefaultIfEmpty()
                                select new ResourceRow()
                                {
                                    ResourceId = rm.ResourceId,
                                    ResourceName = rm.ResourceName,
                                    YearDesignated = rm.YearDesignated,
                                    ResourceTypeId = rm.ResourceTypeId,
                                   // ResourceTypeName = rt.ResourceTypeName,
                                    DesignationStatusId = rm.DesignationStatusId,
                                   // DesignationStatusName = ds.DesignationStatusName,
                                    GISId = rm.GISId,
                                    ModifiedDate = rm.ModifiedDate,

                                    ResourceDescription = rm.ResourceDescription,
                                    ResourceClassId = rm.ResourceClassId,

                                    PrimaryASMSiteNumber = rm.PrimaryASMSiteNumber,

                                    ParentDistrictId = rm.ParentDistrictId,
                                    ParentSensitivityZoneId = rm.ParentSensitivityZoneId


                                }).ToList();

                return ResourceList;

            }

            //using (var context = new Context())
            //{

            //    //Method syntax for Left Outer Joins
            //    ResourceList = context.Resources                                    //r = Resources
            //        .GroupJoin(
            //            context.ResourceTypes,
            //            r => r.ResourceTypeId,
            //            t => t.ResourceTypeId,
            //            (r, t) => new { r, t })                                     //t = ResourceTypes
            //        .SelectMany(rt => rt.t.DefaultIfEmpty(),
            //            (r, t) => new
            //            { rm = r.r, rt = t })
            //        .GroupJoin(
            //            context.ResourceClasses,
            //            rec => rec.rm.ResourceClassId,
            //            c => c.Id,
            //            (r, c) => new
            //            { r, c })                                                       //c = ResourceClasses
            //        .SelectMany(
            //            rec => rec.c.DefaultIfEmpty(),
            //            (r, c) => new
            //            { rm = r.r.rm, rc = c })
            //        .GroupJoin(
            //            context.DesignationStatuses,
            //            rec => rec.rm.DesignationStatusId,
            //            ds => ds.DesignationStatusId,                                 //s = DesignationStatuses
            //            (rm, ds) => new { rm.rm, ds })
            //        .SelectMany(
            //            rec => rec.ds.DefaultIfEmpty(),
            //            (rec, ds) => new ResourceRow()
            //            {
            //                ResourceId = rec.rm.ResourceId,
            //                ResourceName = rec.rm.ResourceName,
            //                YearDesignated = rec.rm.YearDesignated,
            //                ResourceTypeId = rec.rm.ResourceTypeId,
            //                ResourceTypeName = rec.rt.ResourceTypeName,
            //                DesignationStatusId = rec.rm.DesignationStatusId,
            //                DesignationStatusName = ds.DesignationStatusName,
            //                GISId = rec.rm.GISId,
            //                ModifiedDate = rec.rm.ModifiedDate,

            //                ResourceDescription = rec.rm.ResourceDescription,

            //                PrimaryASMSiteNumber = rec.rm.PrimaryASMSiteNumber,

            //                ParentDistrictId = rec.rm.ParentDistrictId,
            //                ParentSensitivityZoneId = rec.rm.ParentSensitivityZoneId

            //            })
            //        .OrderBy(o => o.ResourceName).ToList();
            //}
            ////ClassList.Add(1);
            ////ClassList.Add(2);
            //ResourceList[0].TemporalClasses = new List<int> { 1, 2 };
            ////  ResourceList[0].ResourceClasses = ClassList;
            ////  ResourceList[3].ResourceClassIds = ClassList;
            //return ResourceList;
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
