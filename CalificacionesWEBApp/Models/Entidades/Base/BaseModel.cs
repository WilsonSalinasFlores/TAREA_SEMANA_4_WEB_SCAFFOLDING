namespace CalificacionesWEBApp.Models.Entidades.Base
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;

    public class BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime Creado{ get; set; }
        [Column(TypeName = "datetime2")]
        [AllowNull]
        public DateTime? Actualizado { get; set; }
        
        public bool Eliminado { get; set; }
    }
}
