﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCGalenos.Context;
using MVCGalenos.Models;

namespace MVCGalenos.Controllers
{
    public class PrestadorMedicoController : Controller
    {
        private readonly GalenosDatabaseContext _context;

        public PrestadorMedicoController(GalenosDatabaseContext context)
        {
            _context = context;
        }

        // GET: PrestadorMedicoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Medicos.ToListAsync());
        }

        // GET: PrestadorMedicoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestadorMedico = await _context.Medicos
                .FirstOrDefaultAsync(m => m.IdPrestador == id);
            if (prestadorMedico == null)
            {
                return NotFound();
            }

            return View(prestadorMedico);
        }

        // GET: PrestadorMedicoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PrestadorMedicoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPrestador,Especialidad,NombreCompleto,MatriculaProfesional,MailMedico,TelefonoMedico,DireccionMedico")] PrestadorMedico prestadorMedico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prestadorMedico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prestadorMedico);
        }

        // GET: PrestadorMedicoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestadorMedico = await _context.Medicos.FindAsync(id);
            if (prestadorMedico == null)
            {
                return NotFound();
            }
            return View(prestadorMedico);
        }

        // POST: PrestadorMedicoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPrestador,Especialidad,NombreCompleto,MatriculaProfesional,MailMedico,TelefonoMedico,DireccionMedico")] PrestadorMedico prestadorMedico)
        {
            if (id != prestadorMedico.IdPrestador)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prestadorMedico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrestadorMedicoExists(prestadorMedico.IdPrestador))
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
            return View(prestadorMedico);
        }

        // GET: PrestadorMedicoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestadorMedico = await _context.Medicos
                .FirstOrDefaultAsync(m => m.IdPrestador == id);
            if (prestadorMedico == null)
            {
                return NotFound();
            }

            return View(prestadorMedico);
        }

        // POST: PrestadorMedicoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prestadorMedico = await _context.Medicos.FindAsync(id);
            if (prestadorMedico != null)
            {
                _context.Medicos.Remove(prestadorMedico);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrestadorMedicoExists(int id)
        {
            return _context.Medicos.Any(e => e.IdPrestador == id);
        }
    }
}