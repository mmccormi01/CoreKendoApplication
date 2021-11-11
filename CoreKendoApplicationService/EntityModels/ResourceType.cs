using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreKendoApplicationService.EntityModels
{
    [Table("Config_ResourceType")]
    public partial class ResourceType : ReferenceType
    {
        [Key]
        [Column("ResourceTypeId")]
        public int Id { get; set; }

        [StringLength(50)]
        [Column("ResourceTypeName")]
        public string Name { get; set; }

        [StringLength(500)]
        public string ResourceTypeDescription { get; set; }
    }
}
