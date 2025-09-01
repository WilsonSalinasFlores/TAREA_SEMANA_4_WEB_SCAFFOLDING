using CalificacionesWEBApp.Models.Entidades.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalificacionesWEBApp.Models.Entidades
{
    [Table("Calificaciones")]
    public class CalificacionModel : BaseModel
    {
        [Required]
        [ForeignKey("EstudianteModel")]
        public int EstudianteId { get; set; }
        public EstudianteModel? Estudiante { get; set; }

        [Required]
        [ForeignKey("MateriaModel")]
        public int MateriaId { get; set; }
        public MateriaModel? Materia { get; set; }

        [Required]
        [ForeignKey("ProfesorModel")]
        public int ProfesorId { get; set; }
        public ProfesorModel? Profesor { get; set; }

        [Required]
        [ForeignKey("CursoModel")]
        public int CursoId { get; set; }
        public CursoModel? Curso { get; set; }

        [Required]
        [Range(0, 10, ErrorMessage = "La nota debe estar entre 0 y 10.")]
        [Column(TypeName = "decimal(4,2)")]
        public double N1 { get; set; }
        [Required]
        [Range(0, 10, ErrorMessage = "La nota debe estar entre 0 y 10.")]
        [Column(TypeName = "decimal(4,2)")]
        public double N2 { get; set; }
        [Required]
        [Range(0, 10, ErrorMessage = "La nota debe estar entre 0 y 10.")]
        [Column(TypeName = "decimal(4,2)")]
        public double N3 { get; set; }

        [Column(TypeName = "decimal(4,2)")]
        public double Promedio { get; set; }
        public string? Observacion { get; set; }

    }
    public class CalificacionDTO
    {
        [Required]
        public int MateriaId { get; set; }
        [Required]
        public int ProfesorId { get; set; }
        [Required]
        public int CursoId { get; set; }
    }

    public class CalificacionModelInput
    {
        public int Id { get; set; }
        public int EstudianteId { get; set; }
        public int MateriaId { get; set; }
        public int ProfesorId { get; set; }
        public int CursoId { get; set; }
        public double N1 { get; set; }
        public double N2 { get; set; }
        public double N3 { get; set; }
        public double Promedio { get; set; }
        public string? Observacion { get; set; }
    }
}
