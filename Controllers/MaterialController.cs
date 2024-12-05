using Microsoft.AspNetCore.Mvc;
using Product_Supplier_Registration.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Xml.Linq;

namespace Product_Supplier_Registration.Controllers
{
    public class MaterialController : Controller
    {
        private readonly MyDbContext _context;

        public MaterialController(MyDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string Name, DateTime? CreatedAt)
        {
          
                var materialsQuery = _context.Materials.AsQueryable();
            if (!string.IsNullOrEmpty(Name))
                {
                    materialsQuery = materialsQuery.Where(m => m.Name.Contains(Name));
                }

                if (CreatedAt.HasValue)
                {
                    materialsQuery = materialsQuery.Where(m => m.CreatedAt.Date == CreatedAt.Value.Date);
                }

            ViewData["Name"] = Name;
            ViewData["CreatedAt"] = CreatedAt?.ToString("dd-MM-yyyy");


            var materials = materialsQuery
                    .Select(m => new
                    {
                        m.Code,
                        m.Name,
                        m.Description,
                        m.FiscalCode,
                        m.Specie,
                        m.IdSupplier,
                        CreatedAt = m.CreatedAt.ToString("dd/MM/yyyy"),
                        SupplierQRCode = _context.Suppliers
                            .Where(s => s.Id == m.IdSupplier)
                            .Select(s => s.QRCode)
                            .FirstOrDefault()
                    })
                    .ToList();

                var viewModel = materials.Select(m => new Material
                {
                    Code = m.Code,
                    Name = m.Name,
                    Description = m.Description,
                    FiscalCode = m.FiscalCode,
                    Specie = m.Specie,
                    IdSupplier = m.IdSupplier,
                    CreatedAt = DateTime.Parse(m.CreatedAt),
                    UpdatedAt = null,
                    UpdatedBy = null,
                    CreatedBy = "",
                    SupplierQRCode = m.SupplierQRCode
                }).ToList();

                return View(viewModel);
            }

        
        [HttpGet]
        public IActionResult Create()
        {
            // Carregar todos os fornecedores para preencher o dropdown
            ViewBag.Suppliers = _context.Suppliers.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Material material)
        {
            var supplierExists = _context.Suppliers.Any(s => s.Id == material.IdSupplier);
            if (!supplierExists)
            {
                TempData["SupplierError"] = "O ID do fornecedor não existe.";
                return RedirectToAction("Index");
            }
            material.CreatedBy = "root";
            material.CreatedAt = DateTime.Now;
            _context.Materials.Add(material);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

     

    }
}
