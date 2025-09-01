using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using CalificacionesWEBApp.Data;
using CalificacionesWEBApp.Models.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CalificacionesWEBApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesorApiController : ControllerBase
    {
        private readonly DatosDbContext _context;

        public ProfesorApiController(DatosDbContext context)
        {
            _context = context;
        }

        // GET: api/ProfesorApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfesorModel>>> GetProfesores()
        {
            var profesores = await _context.Profesores
                .Where(p => !p.Eliminado)
                .OrderBy(p => p.Nombre)
                .ToListAsync();
            return profesores;
        }



        // GET: api/ProfesorApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProfesorModel>> GetProfesorModel(int id)
        {
            var profesorModel = await _context.Profesores.FindAsync(id);

            if (profesorModel == null)
            {
                return NotFound();
            }

            return profesorModel;
        }

        // PUT: api/ProfesorApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfesorModel(int id, ProfesorModel profesorModel)
        {
            if (id != profesorModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(profesorModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfesorModelExists(id))
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

        // POST: api/ProfesorApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProfesorModel>> PostProfesorModel(ProfesorModel profesorModel)
        {
            _context.Profesores.Add(profesorModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfesorModel", new { id = profesorModel.Id }, profesorModel);
        }

        // DELETE: api/ProfesorApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfesorModel(int id)
        {
            var profesorModel = await _context.Profesores.FindAsync(id);
            if (profesorModel == null)
            {
                return NotFound();
            }

            _context.Profesores.Remove(profesorModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProfesorModelExists(int id)
        {
            return _context.Profesores.Any(e => e.Id == id);
        }
    }
}
