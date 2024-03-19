
using AutoMapper;
using LaptopStore.Models;
using LaptopStore.Models.DataModels;
using LaptopStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data.SqlTypes;
using System.Security;
using X.PagedList;

namespace LaptopStore.Areas.Admin.Controllers
{

    [Area("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]
    public class HomeAdminController : Controller
    {
        IMapper mapper;
        IWebHostEnvironment env;
        public HomeAdminController(IMapper mapper, IWebHostEnvironment env)
        {
            this.mapper = mapper;
            this.env = env;
        }
        LaptopStoreContext db = new LaptopStoreContext();
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("AccountList")]
        public IActionResult AccountList(int? page)
        {
            int pageSize = 12;
            int pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var listAccount = db.Accounts.ToList();
            PagedList<Account> list = new PagedList<Account>(listAccount, pageNumber, pageSize);
            return View(list);
        }

        [Route("DetailAccount")]
        [HttpGet]
        public IActionResult DetailAccount(string username)
        {

            if (username.IsNullOrEmpty())
            {
                return NotFound();
            }
            var account = db.Accounts.SingleOrDefault(p => p.Username.Equals(username));
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        [Route("productlist")]
        public IActionResult ProductList(int? page)
        {
            int pageSize = 12;
            int pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var listProduct = db.Products.ToList();
            PagedList<Product> list = new PagedList<Product>(listProduct, pageNumber, pageSize);
            return View(list);
        }

        [Route("AddNewProduct")]
        [HttpGet]
        public IActionResult AddNewProduct()
        {
            Product pro = new Product();
            
            //Product newProduct = new Product();
            //newProduct.Image = "default.jpg";

            //newProduct.CreateAt = DateTime.Now;

            //db.Products.Add(newProduct);
            //db.SaveChanges();
            //int latestId = db.Products.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault();
            //var latestProduct = db.Products.FirstOrDefault(x => x.Id == latestId);
            ////ViewBag.MaChatLieu = new SelectList(db.TChatLieus.ToList(), "MaChatLieu","ChatLieu");
            ////ViewBag.MaHangSx = new SelectList(db.THangSxes.ToList(), "MaHangSx","HangSx");
            ////ViewBag.MaNuocSx = new SelectList(db.TQuocGia.ToList(), "MaNuoc","TenNuoc");
            ////ViewBag.MaLoai = new SelectList(db.TLoaiSps.ToList(), "MaLoai","Loai");
            ViewBag.Category = new SelectList(db.Categories.ToList(), "Id", "CategoryName");
            ViewBag.Brand = new SelectList(db.Brands.ToList(), "Id", "BrandName");
            //return RedirectToAction("EditProduct",latestId);
            return View(pro);

        }
        [Route("AddNewProduct")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNewProduct(Product product)
        {
            String filename = "";
            if(product.ImageUpLoad != null)
            {
                String uploadFolder = Path.Combine(env.WebRootPath, "images");
                filename = Guid.NewGuid().ToString() + "_" + product.ImageUpLoad.FileName;
                String filePath = Path.Combine(uploadFolder, filename);
                product.ImageUpLoad.CopyTo(new FileStream(filePath,FileMode.Create));
            }
            else
            {
                filename = "default.jpg";
            }
            if(product.UnitPrice == 0)
            {
                TempData["Message"] = "Unit price must be more than 0";
                return View(product);
                
            }
            if (ModelState.IsValid)
            {
				TempData["Message"] = "";
                product.Image = filename;
                product.CreateAt = DateTime.Now;
                product.SoldNumber = 0;
                db.Products.Add(product);
                db.SaveChanges();
				TempData["Message"] = "Add new product successfully";
				return RedirectToAction("ProductList");
            }
            return View(product);
        }

        [Route("DetailProduct")]
        [HttpGet]
        public IActionResult DetailProduct(int productId)
        {

            if (productId == 0)
            {
                return NotFound();
            }
            var product = db.Products.SingleOrDefault(p => p.Id == productId);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [Route("EditProduct")]
        [HttpGet]
        public IActionResult EditProduct(int productId)
        {
            TempData["MessagePrice"] = "";
            TempData["MessageSold"] = "";
            if (productId == 0)
            {
                return NotFound();
            }
            
            var product = db.Products.Find(productId);
            if (product == null)
            {
                return NotFound();
            }
            
            ViewBag.Category = new SelectList(db.Categories.ToList(), "Id", "CategoryName");
            ViewBag.Brand = new SelectList(db.Brands.ToList(), "Id", "BrandName");
            

            return View(product);
        }
        [Route("EditProduct")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProduct(Product product)
        {
            TempData["MessagePrice"] = "";
            TempData["MessageSold"] = "";
            if(product.UnitPrice == 0)
            {
                TempData["MessagePrice"] = "Unit Price must be than 0";
            }
            String filename = "";
            if (product.ImageUpLoad != null)
            {
                String uploadFolder = Path.Combine(env.WebRootPath, "images");
                filename = Guid.NewGuid().ToString() + "_" + product.ImageUpLoad.FileName;
                String filePath = Path.Combine(uploadFolder, filename);
                product.ImageUpLoad.CopyTo(new FileStream(filePath, FileMode.OpenOrCreate));
                product.Image = filename;
            }
            
            if (ModelState.IsValid)
            {
                
                TempData["Message"] = "";
				product.UpdateAt = DateTime.Now;
                db.Products.Update(product);
                db.SaveChanges();
				TempData["Message"] = "Update product with id "+product.Id+" successfully";
				return RedirectToAction("ProductList", "homeadmin");
            }
            return View(product);
        }
        [Route("UpdateImg")]
        [HttpGet]
        public IActionResult UpdateImg(int productId)
        {
            var product = db.Products.FirstOrDefault(x => x.Id == productId);
            return View(product);
        }

        //[Route("UpdateImg")]
        //[HttpPost]
        //public IActionResult UpdateImg(int productId)
        //{
        //    var product = db.Products.FirstOrDefault(x => x.Id == productId);
        //    return View(product);
        //}

        [Route("DeleteProduct")]
        [HttpGet]
        public IActionResult DeleteProduct(int productId)
        {
            TempData["Message"] = "";


            db.Remove(db.Products.Find(productId));
            db.SaveChanges();
            TempData["Message"] = "Remove this product successfully";
            return RedirectToAction("ProductList", "HomeAdmin");
        }
    }
}
