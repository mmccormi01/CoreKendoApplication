using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreKendoApplicationService.EntityModels
{
    [Table("Config_ParentDistrict")]
    public partial class ParentDistrict : ReferenceType
    {
        [Key]
        [Column("ParentDistrictId")]
        public int Id { get; set; }

        [StringLength(50)]
        [Column("ParentDistrictName")]
        public string Name { get; set; }

        [StringLength(500)]
        public string ParentDistrictDescription { get; set; }
    }
}
