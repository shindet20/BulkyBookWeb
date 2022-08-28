using BulkyBook.DataAccess;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MyWebsite1.Controllers
{

    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }

        //Get Action
        public IActionResult Create()
        {
         
            return View();
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Custom Error", "The Display Order Cannot Exactly Match the Name");
            }

            if(ModelState.IsValid)
            { 
            _db.Categories.Add(obj);
            _db.SaveChanges();
             TempData["success"] = "Category Created Successfully!";
            return RedirectToAction("Index");
            }
            return View(obj);
        }


        //Get Action
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) 
                { return NotFound(); } 

            var CategoryFromDB = _db.Categories.Find(id);

            if (CategoryFromDB == null)
            {
                return NotFound();
            }
                
            return View(CategoryFromDB);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Custom Error", "The Display Order Cannot Exactly Match the Name");
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Updated Successfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        //Get Action
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            { return NotFound(); }

            var CategoryFromDB = _db.Categories.Find(id);

            if (CategoryFromDB == null)
            {
                return NotFound();
            }

            return View(CategoryFromDB);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {


            if (ModelState.IsValid)
            {
                var obj = _db.Categories.Find(id);
                if (obj == null)
                {
                    return NotFound();
                }

                _db.Categories.Remove(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Deleted Successfully!";
                return RedirectToAction("Index");
            }
            return View();

        }
    }
}
