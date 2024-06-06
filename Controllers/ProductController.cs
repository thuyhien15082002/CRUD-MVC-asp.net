using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Services;

namespace MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDB db;
        private readonly IWebHostEnvironment environment;

        public ProductController(ApplicationDB dB, IWebHostEnvironment environment)
        {
            this.db = dB;
            this.environment = environment;
        }
        public IActionResult Index()
        {
            var p = db.products.OrderByDescending(p => p.Id).ToList();
           
            return View(p);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductDto productDto)
        {
            if (productDto.Image == null)
            {
                ModelState.AddModelError("ImageFile", "null");
            }
            if (!ModelState.IsValid)
            {
                return View(productDto);
            }

            string newFileName = DateTime.Now.ToString("ddMMyyyy");
            newFileName += Path.GetExtension(productDto.Image!.FileName);
            string imagePath = environment.WebRootPath + "/products/" + newFileName;
            using (var stream = System.IO.File.Create(imagePath))
            {
                productDto.Image.CopyTo(stream);
            }
            // luu du lieu moi vao db
            Product product = new Product()
            {
                Name = productDto.Name,
                Category = productDto.Category,
                Image = newFileName,
                Price = productDto.Price,
                Description = productDto.Description,
                Status = productDto.Status,
                CreatedAt = DateTime.Now,

            };
            db.products.Add(product);
            db.SaveChanges();

            return RedirectToAction("Index", "Product");
        }

        public IActionResult Edit(int id)
        {
            var p = db.products.Find(id);
            if (p == null)
            {
                return RedirectToAction("Index", "Product");

            }
            Product productDto = new Product()
            {
                Name = p.Name,
                Category = p.Category,
                Price = p.Price,
                Description = p.Description,
                Status = p.Status,
                CreatedAt = DateTime.Now,

            };

            ViewData["ProductId"] = p.Id;
            ViewData["ImageFile"] = p.Image;
            ViewData["CreateAt"] = p.CreatedAt.ToString("dd/MM/yyyy");

            db.SaveChanges();
            return View(productDto);
        }
        [HttpPost]
        public IActionResult Edit(int id, ProductDto productDto)
        {
            var p = db.products.Find(id);
            if (p == null)
            {
                return RedirectToAction("Index", "Product");

            }
            if (!ModelState.IsValid)
            {
                ViewData["ProductId"] = p.Id;
                ViewData["ImageFile"] = p.Image;
                ViewData["CreateAt"] = p.CreatedAt.ToString("dd/MM/yyyy");
                return View(productDto);
            }
            string newFileName = p.Image;
            if (productDto.Image !=null)
            {
                newFileName = DateTime.Now.ToString("ddMMyyyy");
                newFileName += Path.GetExtension(productDto.Image!.FileName);
                string imagePath = environment.WebRootPath + "/products/" + newFileName;
                using (var stream = System.IO.File.Create(imagePath))
                {
                    productDto.Image.CopyTo(stream);
                }

                string oldImage = environment.WebRootPath + "/products/" + p.Image;
                System.IO.File.Delete(imagePath);
            }
            p.Name = productDto.Name;
            p.Category = productDto.Category;
            p.Image = newFileName;
            p.Price = productDto.Price;
            p.Description = productDto.Description;
            p.Status = productDto.Status;

            db.SaveChanges();
            return RedirectToAction("Index", "Product");


        }

        

        public IActionResult Delete(int id)
        {
            var p = db.products.Find(id);
            if (p == null)
            {
                return RedirectToAction("Index", "Product");

            }
            db.products.Remove(p);
            db.SaveChanges();
            return RedirectToAction("Index", "Product");
        }
    }

}
