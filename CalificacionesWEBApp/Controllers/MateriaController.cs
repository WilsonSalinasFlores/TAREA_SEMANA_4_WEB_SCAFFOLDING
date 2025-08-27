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
    public class MateriaController : Controller
    {
        private readonly DatosDbContext _context;

        public MateriaController(DatosDbContext context)
        {
            _context = context;
        }

        // GET: Materia
        public async Task<IActionResult> Index()
        {
            var datosDbContext = _context.Materias
                .Where(x=> x.Eliminado==false)
                .Include(m => m.Curso)
                .Include(m => m.Profesor);
            return View(await datosDbContext.ToListAsync());
        }

        // GET: Materia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materiaModel = await _context.Materias
                .Include(m => m.Curso)
                .Include(m => m.Profesor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (materiaModel == null)
            {
                return NotFound();
            }

            return View(materiaModel);
        }

        // GET: Materia/Create
        public IActionResult Create()
        {
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Nombre");
            ViewData["ProfesorId"] = new SelectList(_context.Profesores, "Id", "Especialidad");
            return View();
        }

        // POST: Materia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,ProfesorId,CursoId,Id,Creado,Actualizado,Eliminado")] MateriaModel materiaModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(materiaModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Nombre", materiaModel.CursoId);
            ViewData["ProfesorId"] = new SelectList(_context.Profesores, "Id", "Especialidad", materiaModel.ProfesorId);
            return View(materiaModel);
        }

        // GET: Materia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materiaModel = await _context.Materias.FindAsync(id);
            if (materiaModel == null)
            {
                return NotFound();
            }
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Nombre", materiaModel.CursoId);
            ViewData["ProfesorId"] = new SelectList(_context.Profesores, "Id", "Especialidad", materiaModel.ProfesorId);
            return View(materiaModel);
        }

        // POST: Materia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nombre,ProfesorId,CursoId,Id,Creado,Actualizado,Eliminado")] MateriaModel materiaModel)
        {
            if (id != materiaModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(materiaModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MateriaModelExists(materiaModel.Id))
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
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Nombre", materiaModel.CursoId);
            ViewData["ProfesorId"] = new SelectList(_context.Profesores, "Id", "Especialidad", materiaModel.ProfesorId);
            return View(materiaModel);
        }

        // GET: Materia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materiaModel = await _context.Materias
                .Include(m => m.Curso)
                .Include(m => m.Profesor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (materiaModel == null)
            {
                return NotFound();
            }

            return View(materiaModel);
        }

        // POST: Materia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var materiaModel = await _context.Materias.FindAsync(id);
            if (materiaModel != null)
            {
                _context.Materias.Remove(materiaModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MateriaModelExists(int id)
        {
            return _context.Materias.Any(e => e.Id == id);
        }
    }
}
