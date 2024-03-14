using LaptopStore.Models;
using LaptopStore.Models.Authentication;
using LaptopStore.Models.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using X.PagedList;

namespace LaptopStore.Controllers
{
	public class HomeController : Controller
	{
		LaptopStoreContext db = new LaptopStoreContext();
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}
        [Authentication]
        public IActionResult Index(int? page)
		{
            int pageSize = 8;
            int pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var productList = db.Products.AsNoTracking().OrderBy(x => x.ProductName);
            PagedList<Product> list = new PagedList<Product>(productList, pageNumber, pageSize);
            return View(list);
        }

		public IActionResult ProductByCategory(int id, int? page)
		{
			int pageSize = 8;
			int pageNumber = page == null || page <= 0 ? 1 : page.Value;


			List<Brand> listProduct = db.Brands.AsNoTracking().Where(x => x.Id == id).OrderBy(x => x.BrandName).ToList();
			PagedList<Brand> list = new PagedList<Brand>(listProduct, pageNumber, pageSize);
			ViewBag.id = id;
			return View(list);
		}

		public IActionResult ProductDetail(int productId)
		{
			var product = db.Brands.SingleOrDefault(x => x.Id == productId);
			var productImg = db.Brands.Where(x => x.Id == productId).ToList();
			ViewBag.productImg = productImg;

			return View(product);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
