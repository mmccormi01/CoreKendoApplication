using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreKendoApplicationService.EntityModels
{
    [Table("Resource_Main")]
    public partial class Resource
    {
        [Key]
        public int ResourceId { get; set; }

//        [Column("ResourceTypeId")]
        public int ResourceTypeId { get; set; }

        [StringLength(50)]
        public string ResourceName { get; set; }
        public string ResourceDescription { get; set; }
        public int? GISId { get; set; }
        public int ResourceClassId { get; set; }
        public int ResourceAreaId { get; set; }
        public int DesignationStatusId { get; set; }
        public int YearDesignated { get; set; }

  //      [Column("ModifiedDate")]
        public DateTime ModifiedDate { get; set; }


        public string PrimaryASMSiteNumber { get; set; }
        public int SecondarySitesId { get; set; }
        public int ParentDistrictId { get; set; }
        public int ParentSensitivityZoneId { get; set; }

    }

}
