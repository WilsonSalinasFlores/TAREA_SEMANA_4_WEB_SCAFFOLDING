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
        public EstudianteModel Estudiante { get; set; }

        [Required]
        [ForeignKey("MateriaModel")]
        public int MateriaId { get; set; }
        public MateriaModel Materia { get; set; }

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
        public string Observacion { get; set; }

    }

}
