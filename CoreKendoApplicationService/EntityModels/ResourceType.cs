using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreKendoApplicationService.EntityModels
{
    [Table("Config_ResourceType")]
    internal partial class ResourceType
    {
        [Key]
        [Column("ResourceTypeId")]
        public int ResourceTypeId { get; set; }

        [StringLength(50)]
        public string ResourceTypeName { get; set; }

        [StringLength(500)]
        public string ResourceTypeDescription { get; set; }
    }
}
