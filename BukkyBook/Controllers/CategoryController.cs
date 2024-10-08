using BukkyBook.Data;
using BukkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BukkyBook.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db) {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> categoryData = _db.Categories.ToList();
            return View(categoryData);
        }
        
        public IActionResult Create()
        {         
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString()) {
                //ModelState.AddModelError("CustomError", "Name and order cannot be the same");
                ModelState.AddModelError("name", "Name and order cannot be the same");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Get
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) 
            { 
                return NotFound();
            }
            var getItem = _db.Categories.Find(id);
            if (getItem != null) { NotFound(); };


            return View(getItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                //ModelState.AddModelError("CustomError", "Name and order cannot be the same");
                ModelState.AddModelError("name", "Name and order cannot be the same");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                TempData["success"] = "Category updated successfully";
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }  
        
        [HttpGet]      
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var getItem = _db.Categories.Find(id);

            return View(getItem);
                      

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
           var getItem = _db.Categories.Find(id); 

            if (getItem == null) 
            { 
                return NotFound(); 
            };

            _db.Categories.Remove(getItem);
            TempData["success"] = "Category deleted successfully";
            _db.SaveChanges();
            return RedirectToAction("Index");             
                  
            

        }
    }
}
