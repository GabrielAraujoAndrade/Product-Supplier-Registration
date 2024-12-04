using Microsoft.AspNetCore.Mvc;
using Product_Supplier_Registration.Models;
using Microsoft.EntityFrameworkCore;

namespace Product_Supplier_Registration.Controllers
{
    public class MaterialController : Controller
    {
        private readonly MyDbContext _context;

        public MaterialController(MyDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
 
            return View();
        }


        [HttpPost]
        public IActionResult Create(Material material)
        {
            material.CreatedAt = DateTime.Now;
            _context.Materials.Add(material);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}
