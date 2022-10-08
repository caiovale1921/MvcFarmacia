using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcFarmacia.Data;
using MvcFarmacia.Models;

namespace MvcFarmacia.Controllers
{
    public class TipoProdutosController : Controller
    {
        private readonly Db _context;

        public TipoProdutosController(Db context)
        {
            _context = context;
        }

        // GET: TipoProdutos
        public async Task<IActionResult> Index()
        {
              return View(await _context.TipoProdutos.ToListAsync());
        }

        // GET: TipoProdutos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TipoProdutos == null)
            {
                return NotFound();
            }

            var tipoProduto = await _context.TipoProdutos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoProduto == null)
            {
                return NotFound();
            }

            return View(tipoProduto);
        }

        // GET: TipoProdutos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoProdutos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descricao")] TipoProduto tipoProduto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoProduto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoProduto);
        }

        // GET: TipoProdutos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TipoProdutos == null)
            {
                return NotFound();
            }

            var tipoProduto = await _context.TipoProdutos.FindAsync(id);
            if (tipoProduto == null)
            {
                return NotFound();
            }
            return View(tipoProduto);
        }

        // POST: TipoProdutos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao")] TipoProduto tipoProduto)
        {
            if (id != tipoProduto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoProduto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoProdutoExists(tipoProduto.Id))
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
            return View(tipoProduto);
        }

        // GET: TipoProdutos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TipoProdutos == null)
            {
                return NotFound();
            }

            var tipoProduto = await _context.TipoProdutos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoProduto == null)
            {
                return NotFound();
            }

            return View(tipoProduto);
        }

        // POST: TipoProdutos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TipoProdutos == null)
            {
                return Problem("Entity set 'Db.TipoProdutos'  is null.");
            }
            var tipoProduto = await _context.TipoProdutos.FindAsync(id);
            if (tipoProduto != null)
            {
                _context.TipoProdutos.Remove(tipoProduto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoProdutoExists(int id)
        {
          return _context.TipoProdutos.Any(e => e.Id == id);
        }
    }
}
