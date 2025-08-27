using CalificacionesWEBApp.Data;
using CalificacionesWEBApp.Models.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;

namespace CalificacionesWEBApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriaApiController : ControllerBase
    {
        private readonly DatosDbContext _context;

        public MateriaApiController(DatosDbContext context)
        {
            _context = context;
        }

        // GET: api/MateriaApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MateriaModel>>> GetMaterias()
        {
            List<MateriaModel> materias = await _context.Materias
                .Where(m => !m.Eliminado)
                .ToListAsync();
            return materias;
        }

        // GET: api/MateriaApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MateriaModel>> GetMateriaModel(int id)
        {
            var materiaModel = await _context.Materias.FirstOrDefaultAsync(x=> x.Id == id);

            if (materiaModel == null)
            {
                return NotFound();
            }

            return materiaModel;
        }

        // PUT: api/MateriaApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMateriaModel(int id, MateriaDTO materiaDTO)
        {
            MateriaModel materiaModel = await _context.Materias.FindAsync(id);
            
            if (materiaModel == null)
            {
                return NotFound();
            }

            if (id != materiaModel.Id)
            {
                return BadRequest();
            }
            materiaModel.Nombre = materiaDTO.Nombre;
            materiaModel.ProfesorId = materiaDTO.ProfesorId;
            materiaModel.CursoId = materiaDTO.CursoId;
            materiaModel.Curso = await _context.Cursos.FirstOrDefaultAsync(x => x.Id == materiaModel.CursoId);
            materiaModel.Profesor = await _context.Profesores.FirstOrDefaultAsync(x => x.Id == materiaModel.ProfesorId);

            _context.Materias.Update(materiaModel);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MateriaModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MateriaApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MateriaModel>> PostMateriaModel(MateriaDTO materiaDTO)
        {
            MateriaModel materia = new MateriaModel();
            materia.ProfesorId = materiaDTO.ProfesorId;
            materia.Eliminado = false;
            materia.Creado = DateTime.Now;
            materia.Actualizado = DateTime.Now;
            materia.Nombre = materiaDTO.Nombre;
            materia.CursoId = materiaDTO.CursoId;
            materia.Curso = await _context.Cursos.FirstOrDefaultAsync(x => x.Id == materia.CursoId);
            materia.Profesor = await _context.Profesores.FirstOrDefaultAsync(x => x.Id == materia.ProfesorId);
            _context.Materias.Add(materia);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMateriaModel", new { id = materia.Id });
        }

        // DELETE: api/MateriaApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMateriaModel(int id)
        {
            var materiaModel = await _context.Materias
                .Include(m => m.Curso)
                .Include(m => m.Profesor).FirstOrDefaultAsync(x=> x.Id== id);
            if (materiaModel == null)
            {
                return NotFound();
            }
            materiaModel.Eliminado = true;
            materiaModel.Actualizado = DateTime.Now;
            _context.Materias.Update(materiaModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MateriaModelExists(int id)
        {
            return _context.Materias.Any(e => e.Id == id);
        }
    }
}
