using CalificacionesWEBApp.Models.Entidades.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalificacionesWEBApp.Models.Entidades
{
    [Table("Materias")]
    public class MateriaModel : BaseModel
    {
        [Required]
        public string Nombre { get; set; }

        [Required]
        [ForeignKey("ProfesorModel")]
        public int ProfesorId { get; set; }
        public ProfesorModel Profesor { get; set; }

        [Required]
        [ForeignKey("CursoModel")]
        public int CursoId { get; set; }
        public CursoModel Curso { get; set; }

        public ICollection<CalificacionModel> Calificaciones { get; set; }
    }

}
