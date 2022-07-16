using CoreCurdApplicationWithRoleBased.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCurdApplicationWithRoleBased.Controllers
{
    public class BrandController : Controller
    {
        private readonly CompanyDBContext _context;
        public BrandController(CompanyDBContext context)
        {
            _context = context;
        }
        public async Task<ActionResult> Index()
        {
            List<Brand> brands = await _context.Brands.ToListAsync();
            return View(brands);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View(new Brand());
        }
        [HttpPost]
        public async Task<ActionResult> Create(Brand b)
        {
            if (ModelState.IsValid)
            {
                _context.Brands.Add(b);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Brand");
            }
            return View(b);
        }
    }
}

