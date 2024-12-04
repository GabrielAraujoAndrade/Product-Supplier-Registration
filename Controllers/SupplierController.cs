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

        public IActionResult Index()
        {
            var suppliers = _context.Suppliers.ToList();
            return View(suppliers);
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
    }

}
