using Microsoft.AspNetCore.Mvc;
using catalogo.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace catalogo.Controllers
{
    public class HomeController : Controller
    {
        private readonly AddventuraContext _context;

        public HomeController(AddventuraContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var productos = await _context.Productos.ToListAsync();
            return View(productos);
        }
    }
}
