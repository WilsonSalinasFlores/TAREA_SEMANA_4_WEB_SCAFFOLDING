using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CalificacionesWEBApp.Data;
using CalificacionesWEBApp.Models.Entidades;

namespace CalificacionesWEBApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoApiController : ControllerBase
    {
        private readonly DatosDbContext _context;

        public CursoApiController(DatosDbContext context)
        {
            _context = context;
        }

        // GET: api/CursoApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CursoModel>>> GetCursos()
        {
            var cursos = await _context.Cursos.Where(c => !c.Eliminado)
                .OrderByDescending(c => c.Periodo)
                .ThenBy(c => c.Nombre)
                .ThenBy(c => c.Paralelo)
                .ThenBy(c => c.Seccion)
                .ToListAsync();
            return cursos;
        }
        
        // GET: api/CursoApi/Periodos
        [HttpGet("periodo")]
        public async Task<ActionResult<IEnumerable<CursoModel>>> GetPeriodo()
        {
            var periodos = await _context.Cursos
                .Where(c => !c.Eliminado)
                .Select(c => c.Periodo)
                .Distinct()
                .ToListAsync();

            return Ok(periodos);
        }

        // GET: api/CursoApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CursoModel>> GetCursoModel(int id)
        {
            var cursoModel = await _context.Cursos.FindAsync(id);

            if (cursoModel == null)
            {
                return NotFound();
            }

            return cursoModel;
        }

        // PUT: api/CursoApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCursoModel(int id, CursoModel cursoModel)
        {
            if (id != cursoModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(cursoModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CursoModelExists(id))
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

        // POST: api/CursoApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CursoModel>> PostCursoModel(CursoModel cursoModel)
        {
            _context.Cursos.Add(cursoModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCursoModel", new { id = cursoModel.Id }, cursoModel);
        }

        // DELETE: api/CursoApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCursoModel(int id)
        {
            var cursoModel = await _context.Cursos.FindAsync(id);
            if (cursoModel == null)
            {
                return NotFound();
            }

            _context.Cursos.Remove(cursoModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CursoModelExists(int id)
        {
            return _context.Cursos.Any(e => e.Id == id);
        }
    }
}
