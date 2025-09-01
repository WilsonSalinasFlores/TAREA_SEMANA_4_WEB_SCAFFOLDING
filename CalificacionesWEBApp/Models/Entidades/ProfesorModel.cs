using CalificacionesWEBApp.Models.Entidades.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CalificacionesWEBApp.Models.Entidades
{
    [Table("Profesores")]
    public class ProfesorModel : BaseModel
    {
        public ProfesorModel()
        {
            Materias = [];
        }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Especialidad { get; set; }
        [JsonIgnore]
        public ICollection<MateriaModel>? Materias { get; set; }
    }
}
