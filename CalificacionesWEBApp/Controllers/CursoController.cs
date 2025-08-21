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
    public class CursoController : Controller
    {
        private readonly DatosDbContext _context;

        public CursoController(DatosDbContext context)
        {
            _context = context;
        }

        // GET: Curso
        public async Task<IActionResult> Index()

        {
            List<CursoModel> cursos = await _context.Cursos
                .Where(c => !c.Eliminado)
                .ToListAsync();
            return View(cursos);
        }

        // GET: Curso/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cursoModel = await _context.Cursos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cursoModel == null)
            {
                return NotFound();
            }

            return View(cursoModel);
        }

        // GET: Curso/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Curso/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Paralelo,Seccion,Periodo,Id,Creado,Actualizado,Eliminado")] CursoModel cursoModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cursoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cursoModel);
        }

        // GET: Curso/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cursoModel = await _context.Cursos.FindAsync(id);
            if (cursoModel == null)
            {
                return NotFound();
            }
            return View(cursoModel);
        }

        // POST: Curso/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nombre,Paralelo,Seccion,Periodo,Id,Creado,Actualizado,Eliminado")] CursoModel cursoModel)
        {
            if (id != cursoModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cursoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CursoModelExists(cursoModel.Id))
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
            return View(cursoModel);
        }

        // GET: Curso/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cursoModel = await _context.Cursos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cursoModel == null)
            {
                return NotFound();
            }

            return View(cursoModel);
        }

        // POST: Curso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cursoModel = await _context.Cursos.FindAsync(id);
            if (cursoModel != null)
            {
                _context.Cursos.Remove(cursoModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CursoModelExists(int id)
        {
            return _context.Cursos.Any(e => e.Id == id);
        }
    }
}
