using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Prods_and_Categories.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace Prods_and_Categories.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;

        // here we can "inject" our context service into the constructor
        public HomeController(MyContext context)
        {
            dbContext = context;
        }
        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            List<Product> AllProds = dbContext.products.ToList();
            ViewBag.Prods = AllProds;
            return View();
        }

        [Route("createP")]
        [HttpPost]
        public IActionResult CreateP(Product newProd)
        {
            dbContext.products.Add(newProd);
            dbContext.SaveChanges();

            // HttpContext.Session.SetInt32("UserId", newUser.UserId);


            // var userInDb = dbContext.users.FirstOrDefault(u => u.Email == newUser.Email);

            return RedirectToAction("Index");
        }

        [Route("categories")]
        [HttpGet]
        public IActionResult CategoriesPage()
        {
            List<Category> AllCat = dbContext.categories.ToList();
            ViewBag.Cat = AllCat;

            return View();
        }

        [Route("createC")]
        [HttpPost]
        public IActionResult CreateC(Category newCat)
        {
            dbContext.categories.Add(newCat);
            dbContext.SaveChanges();

            return RedirectToAction("CategoriesPage");
        }

        [Route("productAdd/{id}")]
        [HttpGet]
        public IActionResult ProductAdd(int id)
        {
            var dbProduct = dbContext.products.FirstOrDefault(p => p.ProductId == id);
            ViewBag.Prod = dbProduct;
            List<Category> AllCat = dbContext.categories.ToList();
            ViewBag.Cat = AllCat;
            // List<Association> Allass = dbContext.associations.ToList();
            IEnumerable<Association> asso = dbContext.associations.Where(a => a.ProductId == id);
            System.Console.WriteLine(asso);
            foreach (var x in asso)
            {
                System.Console.WriteLine("x.Categories.Name");
            }

            var productsWithCategories = dbContext.products
                .Include(x => x.ProductA)
                .ThenInclude(y => y.Categories)
                .FirstOrDefault(things => things.ProductId == id);
        ViewBag.List = productsWithCategories;
        System.Console.WriteLine(productsWithCategories.Name);
            // foreach (var x in productsWithCategories)
            // {
            //     System.Console.WriteLine("x.");
            // }

            return View();
        }

        [Route("AddToCategory/{id}")]
        [HttpPost]
        public IActionResult AddToCategory(Association newA, int id)
        {
            newA.CategoryId = Int32.Parse(Request.Form["CategoryId"]);
            newA.ProductId = (int)id;
            dbContext.associations.Add(newA);
            dbContext.SaveChanges();


            // dbContext.trans.Add(newTrans);

            // dbContext.SaveChanges();

            return RedirectToAction("ProductAdd", new { id = id });
        }


        [Route("CategoryPage/{id}")]
        [HttpGet]
        public IActionResult CategoryPage(int id)
        {
            var dbProduct = dbContext.products.FirstOrDefault(p => p.ProductId == id);
            ViewBag.Prod = dbProduct;
            // List<Category> AllCat = dbContext.categories.ToList();
            // ViewBag.Cat = AllCat;
            // List<Association> Allass = dbContext.associations.ToList();
            IEnumerable<Association> asso = dbContext.associations.Where(a => a.ProductId == id);
            System.Console.WriteLine(asso);
            foreach (var x in asso)
            {
                System.Console.WriteLine("x.Categories.Name");
            }

            var productsWithCategories = dbContext.products
                .Include(x => x.ProductA)
                .ThenInclude(y => y.Categories)
                .FirstOrDefault(things => things.ProductId == id);
        ViewBag.List = productsWithCategories;
        System.Console.WriteLine(productsWithCategories.Name);
            // foreach (var x in productsWithCategories)
            // {
            //     System.Console.WriteLine("x.");
            // }

            return View();
        }


    }
}
