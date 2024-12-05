using Microsoft.AspNetCore.Mvc;
using Product_Supplier_Registration.Models;
namespace Product_Supplier_Registration.Controllers
{
    public class SupplierController : Controller
    {
        private readonly MyDbContext _context;

        public SupplierController(MyDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        public IActionResult Create(Supplier supplier)
        {
            supplier.CreatedAt = DateTime.Now;
            supplier.QRCode = supplier.GenerateQRCode();
            _context.Suppliers.Add(supplier);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Index(string name, DateTime? createdAt)
        {
            var query = _context.Suppliers.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(s => s.Name.Contains(name));
            }

            if (createdAt.HasValue)
            {
                query = query.Where(s => s.CreatedAt.Date == createdAt.Value.Date);
            }

            var suppliers = query.ToList();

            // Mantém os valores dos filtros na ViewData para persistência no front-end
            ViewData["Name"] = name;
            ViewData["CreatedAt"] = createdAt?.ToString("yyyy-MM-dd");

            return View(suppliers);
        }
    }

}
