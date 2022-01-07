using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreKendoApplicationService.EntityModels
{
    [Table("Config_ParentSensitivityZone")]
    public partial class ParentSensitivityZone : ReferenceType
    {
        [Key]
        [Column("ParentSensitivityZoneId")]
        public int Id { get; set; }

        [StringLength(50)]
        [Column("ParentSensitivityZoneName")]
        public string Name { get; set; }

        [StringLength(500)]
        public string ParentSensitivityZoneDescription { get; set; }
    }
}
