using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using catalogo.Models;
using System.IO; // Para manejar el MemoryStream
using Microsoft.AspNetCore.Http; // Para manejar IFormFile

namespace catalogo.Controllers
{
    public class ProductoesController : Controller
    {
        private readonly AddventuraContext _context;

        public ProductoesController(AddventuraContext context)
        {
            _context = context;
        }

        // GET: Productoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Productos.ToListAsync());
        }

        // GET: Productoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.Idproducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Productoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idproducto,Nombreproducto,Precio,Stock")] Producto producto, IFormFile Imagen)
        {
            if (ModelState.IsValid)
            {
                // Verificamos si se ha subido una imagen
                if (Imagen != null && Imagen.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await Imagen.CopyToAsync(memoryStream);
                        producto.Imagen = memoryStream.ToArray(); // Convertimos la imagen en un arreglo de bytes
                    }
                }

                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        // GET: Productoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        // POST: Productoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idproducto,Nombreproducto,Precio,Stock")] Producto producto, IFormFile Imagen)
        {
            if (id != producto.Idproducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Verificamos si se ha subido una nueva imagen
                    if (Imagen != null && Imagen.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await Imagen.CopyToAsync(memoryStream);
                            producto.Imagen = memoryStream.ToArray(); // Convertimos la nueva imagen en un arreglo de bytes
                        }
                    }

                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Idproducto))
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
            return View(producto);
        }

        // GET: Productoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.Idproducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        
        // POST: Productoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            try
            {
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                TempData["ErrorMessage"] = "No se puede eliminar este producto porque está asociado con un pedido.";
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.Idproducto == id);
        }
    }
}
