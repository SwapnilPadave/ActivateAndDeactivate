using CoreCurdApplicationWithRoleBased.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCurdApplicationWithRoleBased.Controllers
{
    public class ProductController : Controller
    {
        private readonly CompanyDBContext _context;
       
        public ProductController(CompanyDBContext context)
        {
            _context = context;        
        }
        public async Task<ActionResult> Index(string search = "", string SortColumn = "ProductName", string IconClass = "fa-sort-asc", int PageNo = 1, bool a = true)
        {
            if (User.IsInRole(RoleName.Admin))
            {
                ViewBag.search = search;
                ViewBag.Sortcolumn = SortColumn;
                ViewBag.IconClass = IconClass;
                ViewBag.Categories = _context.Categories.ToList();
                ViewBag.Brands = _context.Brands.ToList();
                var products = await _context.Products.Include(c => c.Category).Where(c => c.Category.ActiveOrNot.Equals(a)).ToListAsync();
                //For ProductId
                if (ViewBag.SortColumn == "ProductId")
                {
                    if (ViewBag.IconClass == "fa-sort-asc")
                    {
                        products = products.OrderBy(temp => temp.ProductId).ToList();
                    }
                    else
                    {
                        products = products.OrderByDescending(temp => temp.ProductId).ToList();
                    }
                }
                //For Product Name
                if (ViewBag.SortColumn == "ProductName")
                {
                    if (ViewBag.IconClass == "fa-sort-asc")
                    {
                        products = products.OrderBy(temp => temp.ProductName).ToList();
                    }
                    else
                    {
                        products = products.OrderByDescending(temp => temp.ProductName).ToList();
                    }
                }
                //For Price
                if (ViewBag.SortColumn == "Price")
                {
                    if (ViewBag.IconClass == "fa-sort-asc")
                    {
                        products = products.OrderBy(temp => temp.Price).ToList();
                    }
                    else
                    {
                        products = products.OrderByDescending(temp => temp.Price).ToList();
                    }
                }
                // For Date Of Purchase
                if (ViewBag.SortColumn == "DateOfPurchase")
                {
                    if (ViewBag.IconClass == "fa-sort-asc")
                    {
                        products = products.OrderBy(temp => temp.DateOfPurchase).ToList();
                    }
                    else
                    {
                        products = products.OrderByDescending(temp => temp.DateOfPurchase).ToList();
                    }
                }
                //For Available Status
                if (ViewBag.SortColumn == "AvailabilityStatus")
                {
                    if (ViewBag.IconClass == "fa-sort-asc")
                    {
                        products = products.OrderBy(temp => temp.AvailabilityStatus).ToList();
                    }
                    else
                    {
                        products = products.OrderByDescending(temp => temp.AvailabilityStatus).ToList();
                    }
                }
                // For CategoryId
                if (ViewBag.SortColumn == "CategoryId")
                {
                    if (ViewBag.IconClass == "fa-sort-asc")
                    {
                        products = products.OrderBy(temp => temp.CategoryId).ToList();
                    }
                    else
                    {
                        products = products.OrderByDescending(temp => temp.CategoryId).ToList();
                    }
                }
                // For BrandId
                if (ViewBag.SortColumn == "BrandId")
                {
                    if (ViewBag.IconClass == "fa-sort-asc")
                    {
                        products = products.OrderBy(temp => temp.BrandId).ToList();
                    }
                    else
                    {
                        products = products.OrderByDescending(temp => temp.BrandId).ToList();
                    }
                }
                int NoOfRecordPerPage = 5;
                int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(products.Count) / Convert.ToDouble(NoOfRecordPerPage)));
                int NoOfRecordToSkip = (PageNo - 1) * NoOfRecordPerPage;
                ViewBag.PageNo = PageNo;
                ViewBag.NoOfPages = NoOfPages;
                products = products.Skip(NoOfRecordToSkip).Take(NoOfRecordPerPage).ToList();
                return View(products);
            }
            else
            {
                var products = await _context.Products.Include(c => c.Category).Where(c => c.Category.ActiveOrNot.Equals(a)).ToListAsync();
                return View(products);
            }     
                   
        }
        [HttpGet]        
        public ActionResult Create()
        {

            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Brands = _context.Brands.ToList();
            return View(new Product());
        }
        [HttpPost]        
        public async Task<ActionResult> Create(Product p)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories.ToList();
                ViewBag.Brands = _context.Brands.ToList();
                _context.Products.Add(p);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Product");
            }
            return View(p);            
        }
        //public ActionResult Details(int id)
        //{
        //    ViewBag.Categories = _context.Categories.ToList();
        //    Product p = _context.Products.Where(temp => temp.ProductId == id).FirstOrDefault();
        //    return View(p);
        //}
        [HttpGet]        
        public ActionResult Edit(int id)
        {
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Brands = _context.Brands.ToList();
            Product p = _context.Products.Where(temp => temp.ProductId == id).FirstOrDefault();
            return View(p);

        }
        [HttpPost]        
        
        public async Task<ActionResult> Edit(Product p)
        {           
            if (ModelState.IsValid)
            {         
                           
                _context.Entry(p).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(p);
        }
        [HttpGet]      
        public ActionResult Delete(int id)
        {
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Brands = _context.Brands.ToList();
            Product exsistingProduct = _context.Products.Where(temp => temp.ProductId == id).FirstOrDefault();
            return View(exsistingProduct);
        }
        [HttpPost]     
        
        public async Task<ActionResult> Delete(int id, Product p)
        {

            Product exsistingProduct = _context.Products.Where(temp => temp.ProductId == id).FirstOrDefault();
            _context.Products.Remove(exsistingProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }        
       
        public ActionResult Booking(int id)
        {
            Product existingProduct = _context.Products.Where(temp => temp.ProductId == id).FirstOrDefault();
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.Products = _context.Products.ToList();
            Random r = new Random();
            int number = r.Next(10, 10000);
            Product p = new Product();
            QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
            QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode("Your Booking No Is:- " + number.ToString() + " " + "Product Id:-" + existingProduct.ProductId + " " + "Product Name:-" + existingProduct.ProductName +
                                                                 " " + "Price:-" + existingProduct.Price + " " + "Category:-" + existingProduct.Category.CategoryName + " " + "Brand:-" + existingProduct.Brand.BrandName,
                                                                 QRCodeGenerator.ECCLevel.Q);
            QRCode qRCode = new QRCode(qRCodeData);
            using (MemoryStream ms = new MemoryStream())
            {
                using (Bitmap bitmap = qRCode.GetGraphic(20))
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    ViewBag.QRCodeImage = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                    ViewBag.BookingId = number.ToString();
                }
            }
            return View();
        }
    }
}
