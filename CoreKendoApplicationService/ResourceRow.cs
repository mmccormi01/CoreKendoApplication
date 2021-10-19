using System;
using System.Collections.Generic;
using System.Text;

namespace CoreKendoApplicationService
{
    public class ResourceRow
    {
        public int ResourceId { get; set; }
        public string ResourceName { get; set; }
        public string ResourceDescription { get; set; }
        public int? YearDesignated { get; set; }

        public string ResourceTypeName { get; set; }

        public string DesignationStatusName { get; set; }
        public int? GISId { get; set; }

        public DateTime? ModifiedDate { get; set; }



    }
}
