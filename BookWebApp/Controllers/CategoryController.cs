﻿using BookWebApp.Controllers.Data;
using BookWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookWebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDBContext _db;

        public CategoryController(ApplicationDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
           IEnumerable<Category> objCategoryList = _db.Categories.ToList();
           return View(objCategoryList);
        }
        //GET
        public IActionResult Create()
        {
            
            return View();
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "The DisplayOrder cannot exactly match the Name.");
            }
            if(ModelState.IsValid) { 
            _db.Categories.Add(obj);
            _db.SaveChanges();
            TempData["success"] = "Category created successfully";
            return RedirectToAction("Index");
            }
            return View(obj);
        }

		public IActionResult Edit(int? id)
		{
            if(id == null || id == 0) { return NotFound(); }
            var categoryFromDb = _db.Categories.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.ID == id);
			//var categoryFromDbSingle= _db.Categories.SingleOrDefault(u => u.ID == id);
            if(categoryFromDb == null) { return NotFound(); }

			return View(categoryFromDb);
		}

		//Post
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Category obj)
		{
			if (obj.Name == obj.DisplayOrder.ToString())
			{
				ModelState.AddModelError("CustomError", "The DisplayOrder cannot exactly match the Name.");
			}
			if (ModelState.IsValid)
			{
				_db.Categories.Update(obj);
				_db.SaveChanges();
                TempData["success"] = "Category edited successfully";
                return RedirectToAction("Index");
			}
			return View(obj);
		}

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) { return NotFound(); }
            var categoryFromDb = _db.Categories.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.ID == id);
            //var categoryFromDbSingle= _db.Categories.SingleOrDefault(u => u.ID == id);
            if (categoryFromDb == null) { return NotFound(); }

            return View(categoryFromDb);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Categories.Find(id);
            if(obj == null) { return NotFound(); }

            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
            
    }
}

