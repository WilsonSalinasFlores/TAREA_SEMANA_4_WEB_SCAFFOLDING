using CalificacionesWEBApp.Models.Entidades.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalificacionesWEBApp.Models.Entidades
{

    [Table("Cursos")]
    public class CursoModel : BaseModel
    {
        public CursoModel()
        {
            
            Materias = new HashSet<MateriaModel>();
            Estudiantes = new HashSet<EstudianteModel>();
        }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Paralelo { get; set; }

        [Required]
        public string Seccion { get; set; }

        [Required]
        public string Periodo { get; set; }

        public ICollection<MateriaModel> Materias { get; set; }
        public ICollection<EstudianteModel> Estudiantes { get; set; }
    }
}

