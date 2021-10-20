using System;
using System.Collections.Generic;
using System.Text;

namespace CoreKendoApplicationService
{
    public class ResourceRow
    {
        //Header Fields
        public int ResourceId { get; set; }
        public string ResourceName { get; set; }

        public int? YearDesignated { get; set; }

        public string ResourceTypeName { get; set; }

        public string DesignationStatusName { get; set; }
        public int? GISId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        //Detail Fields
        public string ResourceDescription { get; set; }
        public string ResourceClass { get; set; }
        public string PrimaryASMSite { get; set; }
        public string OtherSiteNos { get; set; }
        public string ParentDistrict { get; set; }
        public string SensitivityZone { get; set; }




    }
}
