using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreKendoApplicationService.EntityModels
{
    [Table("Config_DesignationStatus")]
    public partial class DesignationStatus : ReferenceType
    {
        [Key]
        [Column("DesignationStatusId")]
        public int DesignationStatusId { get; set; }

        [StringLength(50)]
        public string DesignationStatusName { get; set; }

        [StringLength(500)]
        public string DesignationStatusDescription { get; set; }
    }
}
