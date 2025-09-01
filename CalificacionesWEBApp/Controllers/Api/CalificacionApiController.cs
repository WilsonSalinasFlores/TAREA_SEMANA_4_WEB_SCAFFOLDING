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
    public class CalificacionApiController : ControllerBase
    {
        private readonly DatosDbContext _context;

        public CalificacionApiController(DatosDbContext context)
        {
            _context = context;
        }



        // GET: api/CalificacionApi/periodo/2024-2025
        [HttpGet("periodo/{periodo}")]
        public async Task<ActionResult<IEnumerable<CalificacionModel>>> GetCalificacionesByPeriodo(string periodo = "2024-2025")
        {
            var query = from a in _context.Calificaciones
                        join b in _context.Materias on a.MateriaId equals b.Id
                        join c in _context.Profesores on a.ProfesorId equals c.Id
                        join d in _context.Cursos on a.CursoId equals d.Id
                        where d.Periodo == periodo
                        group a by new
                        {
                            a.MateriaId,
                            Materia = b.Nombre,
                            a.ProfesorId,
                            Profesor = c.Nombre,
                            a.CursoId,
                            Curso = d.Nombre + " \"" + d.Paralelo + "\" - " + d.Seccion,
                            d.Periodo
                        } into g
                        orderby g.Key.Periodo ascending
                        select new
                        {
                            MateriaId = g.Key.MateriaId,
                            Materia = g.Key.Materia,
                            ProfesorId = g.Key.ProfesorId,
                            Profesor = g.Key.Profesor,
                            CursoId = g.Key.CursoId,
                            Curso = g.Key.Curso,
                            Periodo = g.Key.Periodo,
                            NEstudiantes = g.Count()
                        };

            return Ok(await query.ToListAsync());
        }


        // GET: api/CalificacionApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CalificacionModel>> GetCalificacionModel(int id)
        {
            var calificacionModel = await _context.Calificaciones.Where(x=> x.Id==id)
                .Include(x=> x.Estudiante)
                .Include(x=> x.Profesor)
                .Include(x=> x.Curso)
                .Include(x=> x.Materia)
                .FirstOrDefaultAsync ();

            if (calificacionModel == null)
            {
                return NotFound();
            }

            return calificacionModel;
        }

        // PUT: api/CalificacionApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCalificacionModel(int id, CalificacionModelInput calificacionInput)
        {
            var calificacionModel = await _context.Calificaciones.FirstOrDefaultAsync(x=> x.Id== id);
            if (calificacionModel == null)
            {
                return NotFound();
            }
            if (id != calificacionModel.Id)
            {
                return BadRequest();
            }
            if (calificacionInput.N1>10 || calificacionInput.N1<0)
            {
                return BadRequest();
            }
            if (calificacionInput.N2 > 10 || calificacionInput.N2 < 0)
            {
                return BadRequest();
            }
            if (calificacionInput.N3 > 10 || calificacionInput.N3 < 0)
            {
                return BadRequest();
            }
            calificacionModel.N1 = calificacionInput.N1;
            calificacionModel.N2 = calificacionInput.N2;
            calificacionModel.N3 = calificacionInput.N3;
            calificacionModel.Promedio = (calificacionInput.N1 + calificacionInput.N2 + calificacionInput.N3) / 3;
            calificacionModel.Observacion = calificacionInput.Observacion;
            calificacionModel.Actualizado = DateTime.Now;
            _context.Entry(calificacionModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalificacionModelExists(id))
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

        // POST: api/CalificacionApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("obtenerCalificaciones")]
        public async Task<ActionResult<IEnumerable<CalificacionModel>>> GetCalificacionesByCurrsoModel(CalificacionDTO calificacionDTO)
        {
            var calificaciones = await (
                from a in _context.Estudiantes
                where a.CursoId == calificacionDTO.CursoId
                join b in _context.Calificaciones
                    .Where(x => x.CursoId == calificacionDTO.CursoId && x.ProfesorId == calificacionDTO.ProfesorId && x.MateriaId == calificacionDTO.MateriaId)
                    on a.Id equals b.EstudianteId into califJoin
                from b in califJoin.DefaultIfEmpty()
                select new CalificacionModel
                {
                    Id = b != null ? b.Id : 0,
                    EstudianteId = b != null ? b.EstudianteId : a.Id,
                    MateriaId = b != null ? b.MateriaId : calificacionDTO.MateriaId,
                    N1 = b != null ? b.N1 : 0,
                    N2 = b != null ? b.N2 : 0,
                    N3 = b != null ? b.N3 : 0,
                    Promedio = b != null ? b.Promedio : 0,
                    Observacion = b != null ? b.Observacion : "",
                    Creado = b != null ? b.Creado : DateTime.Now,
                    Actualizado = b != null ? b.Actualizado : null,
                    Eliminado = b != null ? b.Eliminado : false,
                    CursoId = b != null ? b.CursoId : calificacionDTO.CursoId,
                    ProfesorId = b != null ? b.ProfesorId : calificacionDTO.ProfesorId,
                    Estudiante = a
                }
            )
            .ToListAsync();
            var Materia = await _context.Materias.FirstOrDefaultAsync(x => x.Id == calificacionDTO.MateriaId);
            var Profesor = await _context.Profesores.FirstOrDefaultAsync(x => x.Id == calificacionDTO.ProfesorId);
            var Curso = await _context.Cursos.FirstOrDefaultAsync(x => x.Id == calificacionDTO.CursoId);
            var nuevos = false;
            foreach (var cal in calificaciones)
            {
                if (cal.Id == 0)
                {
                    cal.Materia = Materia;
                    cal.Profesor = Profesor;
                    cal.Curso = Curso;
                    var estudiante = await _context.Estudiantes.FirstOrDefaultAsync(x => x.Id == cal.EstudianteId);
                    cal.Estudiante = estudiante;
                    nuevos = true;
                }
            }
            if (nuevos)
            {
                await using var tx = await _context.Database.BeginTransactionAsync();
                try
                {
                    foreach (var cal in calificaciones)
                    {
                        _context.Calificaciones.Add(cal);
                    }
                    await _context.SaveChangesAsync();
                    await tx.CommitAsync();
                    calificaciones = await _context.Calificaciones
                        .Where(x=> x.CursoId==calificacionDTO.CursoId && 
                               x.MateriaId==calificacionDTO.MateriaId && 
                               x.ProfesorId==calificacionDTO.ProfesorId)
                        .Include(x=> x.Estudiante)
                        .Include(x=> x.Curso)
                        .Include(x=> x.Materia)
                    .ToListAsync();
                }
                catch (Exception ex)
                {
                    await tx.RollbackAsync();
                    return StatusCode(500, "Error al registrar la venta: " + ex.Message);
                }

            }
            return calificaciones;

        }


        // POST: api/CalificacionApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CalificacionModel>> PostCalificacionModel(CalificacionModel calificacionModel)
        {
            _context.Calificaciones.Add(calificacionModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCalificacionModel", new { id = calificacionModel.Id }, calificacionModel);
        }

        // DELETE: api/CalificacionApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCalificacionModel(int id)
        {
            var calificacionModel = await _context.Calificaciones.FindAsync(id);
            if (calificacionModel == null)
            {
                return NotFound();
            }

            _context.Calificaciones.Remove(calificacionModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CalificacionModelExists(int id)
        {
            return _context.Calificaciones.Any(e => e.Id == id);
        }
    }
}
