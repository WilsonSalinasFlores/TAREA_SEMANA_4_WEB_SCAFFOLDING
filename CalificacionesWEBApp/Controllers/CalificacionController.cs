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
    public class CalificacionController : Controller
    {
        private readonly DatosDbContext _context;

        public CalificacionController(DatosDbContext context)
        {
            _context = context;
        }

        // GET: Calificacion
        public async Task<IActionResult> Index()
        {
            var datosDbContext = _context.Calificaciones.Where(c => !c.Eliminado);
            return View(await datosDbContext.ToListAsync());
        }

        // GET: Calificacion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calificacionModel = await _context.Calificaciones
                .Include(c => c.Estudiante)
                .Include(c => c.Materia)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (calificacionModel == null)
            {
                return NotFound();
            }

            return View(calificacionModel);
        }

        // GET: Calificacion/Create
        public IActionResult Create()
        {
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "Apellido");
            ViewData["MateriaId"] = new SelectList(_context.Materias, "Id", "Nombre");
            return View();
        }

        // POST: Calificacion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EstudianteId,MateriaId,N1,N2,N3,Promedio,Observacion,Id,Creado,Actualizado,Eliminado")] CalificacionModel calificacionModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(calificacionModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "Apellido", calificacionModel.EstudianteId);
            ViewData["MateriaId"] = new SelectList(_context.Materias, "Id", "Nombre", calificacionModel.MateriaId);
            return View(calificacionModel);
        }

        // GET: Calificacion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calificacionModel = await _context.Calificaciones.FindAsync(id);
            if (calificacionModel == null)
            {
                return NotFound();
            }
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "Apellido", calificacionModel.EstudianteId);
            ViewData["MateriaId"] = new SelectList(_context.Materias, "Id", "Nombre", calificacionModel.MateriaId);
            return View(calificacionModel);
        }

        // POST: Calificacion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EstudianteId,MateriaId,N1,N2,N3,Promedio,Observacion,Id,Creado,Actualizado,Eliminado")] CalificacionModel calificacionModel)
        {
            if (id != calificacionModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calificacionModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalificacionModelExists(calificacionModel.Id))
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
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "Apellido", calificacionModel.EstudianteId);
            ViewData["MateriaId"] = new SelectList(_context.Materias, "Id", "Nombre", calificacionModel.MateriaId);
            return View(calificacionModel);
        }

        // GET: Calificacion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calificacionModel = await _context.Calificaciones
                .Include(c => c.Estudiante)
                .Include(c => c.Materia)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (calificacionModel == null)
            {
                return NotFound();
            }

            return View(calificacionModel);
        }

        // POST: Calificacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var calificacionModel = await _context.Calificaciones.FindAsync(id);
            if (calificacionModel != null)
            {
                _context.Calificaciones.Remove(calificacionModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CalificacionModelExists(int id)
        {
            return _context.Calificaciones.Any(e => e.Id == id);
        }
    }
}
