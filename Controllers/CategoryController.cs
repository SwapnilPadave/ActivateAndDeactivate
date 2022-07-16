using CoreCurdApplicationWithRoleBased.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCurdApplicationWithRoleBased.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CompanyDBContext _context;
        
        public CategoryController(CompanyDBContext context)
        {
            _context = context;
        }
        public async Task<ActionResult> Index(string search=" ")
        {
            List<Category> category = await _context.Categories.Where(temp => temp.CategoryName.Contains(search)).ToListAsync();
            return View(category);
           
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View(new Category());
        }
        [HttpPost]
        public async Task<ActionResult> Create (Category c)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(c);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Category");
            }
            return View(c);
        }
        //public ActionResult Details(int id)
        //{

        //    Category c = _context.Categories.Where(temp => temp.CategoryId == id).FirstOrDefault();
        //    return View(c);
        //}
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Category c = _context.Categories.Where(temp => temp.CategoryId == id).FirstOrDefault();
            return View(c);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(Category c)
        {
            if (ModelState.IsValid)
            {

                _context.Entry(c).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Category");
            }
            return View(c);
        }
        [HttpGet]       

        public ActionResult Delete(int id)
        {
            Category existingCategory = _context.Categories.Where(temp => temp.CategoryId == id).FirstOrDefault();
            return View(existingCategory);
        }
        [HttpPost]
        
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(long id, Category c)
        {
            Category existingCategory = _context.Categories.Where(temp => temp.CategoryId == id).FirstOrDefault();
            _context.Categories.Remove(existingCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Active(int id)
        {
            var act = await _context.Categories.SingleAsync(c => c.CategoryId == id);
            act.ActiveOrNot = true;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Product");
        }
        public async Task<ActionResult> Deactive(int id)
        {
            var deact = await _context.Categories.SingleAsync(c => c.CategoryId == id);
            deact.ActiveOrNot = false;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Product");

        }
    }
}
