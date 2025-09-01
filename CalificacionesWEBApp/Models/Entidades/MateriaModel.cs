using CalificacionesWEBApp.Models.Entidades.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CalificacionesWEBApp.Models.Entidades
{
    [Table("Materias")]
    public class MateriaModel : BaseModel
    {
        public MateriaModel()
        {
            Calificaciones = [];
            Curso = new CursoModel();
            Profesor = new ProfesorModel();

        }
        [Required]
        public string Nombre { get; set; }

        [Required]
        [ForeignKey("ProfesorModel")]

        public int ProfesorId { get; set; }
        [JsonIgnore]
        public ProfesorModel Profesor { get; set; }

        [Required]
        [ForeignKey("CursoModel")]
        public int CursoId { get; set; }
        [JsonIgnore]
        public CursoModel Curso { get; set; }
        [JsonIgnore]
        public ICollection<CalificacionModel> Calificaciones { get; set; }
    }

    public class MateriaDTO
    {
        public string Nombre { get; set; }
        [Required]
        public int ProfesorId { get; set; }
        [Required]
        public int CursoId { get; set; }
    }
    


}
