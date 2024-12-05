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

        public IActionResult Index(string Name, DateTime? CreatedAt)
        {
            // Inicia a consulta com todos os materiais
            var materialsQuery = _context.Materials.Include(m => m.Supplier).AsQueryable();

            // Filtra por nome, se fornecido
            if (!string.IsNullOrEmpty(Name))
            {
                materialsQuery = materialsQuery.Where(m => m.Name.Contains(Name));
            }

            // Filtra por data de criação, se fornecida
            if (CreatedAt.HasValue)
            {
                materialsQuery = materialsQuery.Where(m => m.CreatedAt.Date == CreatedAt.Value.Date);
            }

            var materials = materialsQuery
                .Select(m => new
                {
                    m.Code,
                    m.Name,
                    m.Description,
                    m.FiscalCode,
                    m.Specie,
                    m.Supplier,
                    CreatedAt = m.CreatedAt.ToString("dd/MM/yyyy"),
                    SupplierQRCode = m.Supplier != null ? m.Supplier.QRCode : "No QR Code"
                })
                .ToList();

            // Mapeia para a view esperada:
            var viewModel = materials.Select(m => new Material
            {
                Code = m.Code,
                Name = m.Name,
                Description = m.Description,
                FiscalCode = m.FiscalCode,
                Specie = m.Specie,
                Supplier = m.Supplier,
                CreatedAt = DateTime.Parse(m.CreatedAt)
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
            material.CreatedAt = DateTime.Now;
            _context.Materials.Add(material);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
