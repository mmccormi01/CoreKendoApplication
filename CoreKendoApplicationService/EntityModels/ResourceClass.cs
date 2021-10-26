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
            public string Name { get; set; }

            [StringLength(500)]
            public string ResourceClassDescription { get; set; }
        }
    }
