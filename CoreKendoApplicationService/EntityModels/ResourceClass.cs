using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreKendoApplicationService.EntityModels
{
    [Table("Config_ResourceClass")]
    public partial class ResourceClass 
    {
        [Key]
        [Column("ResourceClassId")]
        public int Id { get; set; }

        [StringLength(50)]
        [Column("ResourceClassName")]
        public string Name { get; set; }

    }
}
