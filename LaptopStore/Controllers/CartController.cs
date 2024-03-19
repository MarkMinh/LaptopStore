using LaptopStore.Models;
using LaptopStore.Models.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LaptopStore.Controllers
{
	public class CartController : Controller
	{
		private readonly LaptopStoreContext db;

		public CartController(LaptopStoreContext db)
		{
			this.db = db;
		}
		public IActionResult Index()
		{
			return RedirectToAction("Index", "Home");
		}



		[HttpPost]
		public IActionResult AddToCart(int productId, int quantity)
		{
			TempData["Err"] = "";

			Product product = db.Products.SingleOrDefault(x => x.Id == productId);
			db.Entry(product).State = EntityState.Detached;
			string username = HttpContext.Session.GetString("username");
			if(username == null)
			{
				TempData["msg"] = "You are not logged into the system";
				return RedirectToAction("Login", "Access");	
			}
			if (product == null)
			{
				return RedirectToAction("Home", "Index");
			}
			if (quantity <= 0 || quantity > product.UnitsInStock)
			{
				TempData["Err"] = "Please select valid quantity";
				return View(product);
			}
			Order order = db.Orders.FirstOrDefault(o => o.ProductId == productId && o.StatusId == 1 && o.Username == username);
			if (order == null)
			{
				Order orderNew = new Order
				{
					ProductId = productId,
					Quantity = quantity,
					StatusId = 1,
					CreateAt = DateTime.Now,
					Username = username
				};

				db.Orders.Add(orderNew);

			}
			else
			{
				order.Quantity += quantity;
				db.Orders.Update(order);

			}
			db.SaveChanges();
			return View(product);
		}

		public IActionResult ViewCart()
		{
			TempData["empty"] = "";
			if (HttpContext.Session.GetString("username") != null)
			{
				IEnumerable<Order> orderList = db.Orders.Where(o => o.StatusId == 1 && o.Username == HttpContext.Session.GetString("username"));
				if (orderList.Count() == 0)
				{
					TempData["empty"] = "There's nothing in your cart, please add to cart";
				}
				return View(orderList);
			}
			else
			{
				TempData["msg"] = "You are not logged into this system";
				return RedirectToAction("Login", "Access");
			}
		}

		[HttpPost]
		public IActionResult DeleteOrder(int orderId)
		{
			var order = db.Orders.Find(orderId);
			db.Orders.Remove(order);
			db.SaveChanges();
			return RedirectToAction("ViewCart");
		}

		[HttpPost]
		public IActionResult Buy()
		{
			IEnumerable<Order> orderItems = db.Orders.Where(o => o.StatusId == 1 && o.Username == HttpContext.Session.GetString("username"));
			IEnumerable<Order> orderItems2 = db.Orders.Where(o => o.StatusId == 1 && o.Username == HttpContext.Session.GetString("username")).ToList();
			var b = orderItems2.Count();
			var a = orderItems.Count();
			if (orderItems.Count() == 0)
			{
				
				return RedirectToAction("ViewCart");
			}
			else
			{
				foreach(Order o in orderItems)
				{
					o.StatusId = 2;
					db.Orders.Update(o);
				}
			}
			db.SaveChanges();
			var c = orderItems2.Count();
			if (orderItems2.Count() != 0)
			{

				foreach (var item in orderItems2)
				{

					var product = db.Products.Find(item.ProductId);

					if (product != null)
					{

						product.UnitsInStock -= item.Quantity;
						product.SoldNumber += item.Quantity;

						db.Products.Update(product);
					}
				}
			}

			
			db.SaveChanges();

			return RedirectToAction("ViewCart");
		}
	}
}
