using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CalificacionesWEBApp.Data;
using CalificacionesWEBApp.Models.Entidades;

namespace CalificacionesWEBApp.Controllers
{
    public class EstudianteController : Controller
    {
        private readonly DatosDbContext _context;

        public EstudianteController(DatosDbContext context)
        {
            _context = context;
        }

        // GET: Estudiante
        public async Task<IActionResult> Index()
        {
            var datosDbContext = _context.Estudiantes
                .Where(c => c.Eliminado == false)
                .Include(e => e.Curso);
            return View(await datosDbContext.ToListAsync());
        }

        // GET: Estudiante/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudianteModel = await _context.Estudiantes
                .Include(e => e.Curso)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estudianteModel == null)
            {
                return NotFound();
            }

            return View(estudianteModel);
        }

        // GET: Estudiante/Create
        public IActionResult Create()
        {

            ViewData["CursoId"] = new SelectList(
                _context.Cursos
                    .Where(c => c.Eliminado == false)
                    .Select(c => new { c.Id, CursoNombre = c.Nombre + " " + c.Paralelo + " " + c.Seccion + " - " + c.Periodo })
                    .ToList(),
                "Id",
                "CursoNombre"
            );
            return View();
        }

        // POST: Estudiante/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Apellido,CursoId,Id")] EstudianteModel estudianteModel)
        {
            estudianteModel.Eliminado = false;
            estudianteModel.Actualizado = DateTime.Now;
            estudianteModel.Creado = DateTime.Now;
            _context.Add(estudianteModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Estudiante/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudianteModel = await _context.Estudiantes.FindAsync(id);
            if (estudianteModel == null)
            {
                return NotFound();
            }
            ViewData["CursoId"] = new SelectList(
                _context.Cursos
                    .Where(c => c.Eliminado == false)
                    .Select(c => new { c.Id, CursoNombre = c.Nombre + " " + c.Paralelo + " " + c.Seccion + " - " + c.Periodo })
                    .ToList(),
                "Id",
                "CursoNombre"
            );
            return View(estudianteModel);
        }

        // POST: Estudiante/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nombre,Apellido,CursoId,Id,Creado,Actualizado,Eliminado")] EstudianteModel estudianteModel)
        {
            if (id != estudianteModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    estudianteModel.Actualizado = DateTime.Now;
                    _context.Update(estudianteModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstudianteModelExists(estudianteModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CursoId"] = new SelectList(
                _context.Cursos
                    .Where(c=> c.Eliminado==false)
                    .Select(c => new { c.Id, CursoNombre = c.Nombre + " " + c.Paralelo + " " + c.Seccion + " - " + c.Periodo })
                    .ToList(),
                "Id",
                "CursoNombre"
            );
            return View(estudianteModel);
        }

        // GET: Estudiante/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudianteModel = await _context.Estudiantes
                .Include(e => e.Curso)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estudianteModel == null)
            {
                return NotFound();
            }

            return View(estudianteModel);
        }

        // POST: Estudiante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estudianteModel = await _context.Estudiantes.FindAsync(id);
            if (estudianteModel != null)
            {
                estudianteModel.Eliminado = true;
                _context.Estudiantes.Update(estudianteModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstudianteModelExists(int id)
        {
            return _context.Estudiantes.Any(e => e.Id == id);
        }
    }
}
