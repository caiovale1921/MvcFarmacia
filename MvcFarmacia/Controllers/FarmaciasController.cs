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
    public class FarmaciasController : Controller
    {
        private readonly Db _context;

        public FarmaciasController(Db context)
        {
            _context = context;
        }

        // GET: Farmacias
        public async Task<IActionResult> Index()
        {
              return View(await _context.Farmacias.ToListAsync());
        }

        // GET: Farmacias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Farmacias == null)
            {
                return NotFound();
            }

            var farmacia = await _context.Farmacias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (farmacia == null)
            {
                return NotFound();
            }

            return View(farmacia);
        }

        // GET: Farmacias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Farmacias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Endereco,Telefone")] Farmacia farmacia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(farmacia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(farmacia);
        }

        // GET: Farmacias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Farmacias == null)
            {
                return NotFound();
            }

            var farmacia = await _context.Farmacias.FindAsync(id);
            if (farmacia == null)
            {
                return NotFound();
            }
            return View(farmacia);
        }

        // POST: Farmacias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Endereco,Telefone")] Farmacia farmacia)
        {
            if (id != farmacia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(farmacia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FarmaciaExists(farmacia.Id))
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
            return View(farmacia);
        }

        // GET: Farmacias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Farmacias == null)
            {
                return NotFound();
            }

            var farmacia = await _context.Farmacias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (farmacia == null)
            {
                return NotFound();
            }

            return View(farmacia);
        }

        // POST: Farmacias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Farmacias == null)
            {
                return Problem("Entity set 'Db.Farmacias'  is null.");
            }
            var farmacia = await _context.Farmacias.FindAsync(id);
            if (farmacia != null)
            {
                _context.Farmacias.Remove(farmacia);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Farmacias/Produtos/{idFarmacia}", Name = "ProdutosFarmacia")]
        public IActionResult Produtos(int? idFarmacia)
        {
            Farmacia farmacia = _context.Farmacias
                .Include(x => x.Produtos)
                .ThenInclude(y => y.TipoProduto)
                .FirstOrDefault(y => y.Id == idFarmacia);

            if (farmacia is not null)
                return View(farmacia);
            return View();
        }

        [HttpGet("Farmacias/DeletarProdutoFarmacia/{idProduto}", Name = "DeletarProdutosFarmacia")]
        public IActionResult DeletarProdutoFarmacia(int idProduto)
        {
            try
            {
                if (idProduto == 0)
                    return BadRequest();

                var produto = _context.Produtos.Include(x => x.Farmacia).FirstOrDefault(y => y.Id == idProduto);
                if (produto is null)
                    return NotFound();

                var idFarmacia = produto.Farmacia.Id;
                produto.Farmacia = null;
                _context.SaveChanges();
                return RedirectToAction("Produtos", new { idFarmacia = idFarmacia });
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao realizar a operação - Erro: {ex}");
            }
        }

        private bool FarmaciaExists(int id)
        {
          return _context.Farmacias.Any(e => e.Id == id);
        }
    }
}
