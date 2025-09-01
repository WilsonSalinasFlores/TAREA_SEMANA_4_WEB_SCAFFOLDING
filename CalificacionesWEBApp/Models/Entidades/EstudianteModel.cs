using CalificacionesWEBApp.Models.Entidades.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CalificacionesWEBApp.Models.Entidades
{
    [Table("Estudiantes")]
    public class EstudianteModel : BaseModel
    {
        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        [Required]
        [ForeignKey("CursoModel")]
        public int CursoId { get; set; }
        public CursoModel? Curso { get; set; }
        [JsonIgnore]
        public ICollection<CalificacionModel>? Calificaciones { get; set; }
    }
}
