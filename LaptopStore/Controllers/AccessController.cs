
using LaptopStore.Models;
using LaptopStore.Models.DataModels;
using Microsoft.AspNetCore.Mvc;

namespace LaptopStore.Controllers
{
    public class AccessController : Controller
	{
		LaptopStoreContext db = new LaptopStoreContext();
		[HttpGet]
		public IActionResult Login()
		{
			if (HttpContext.Session.GetString("username") == null)
			{
				return View();
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}

		[HttpPost]
		public IActionResult Login(Account account)
		{
			if (HttpContext.Session.GetString("username") == null)
			{
				var u = db.Accounts.Where(u => u.Username.Equals(account.Username) && u.Password.Equals(account.Password)).FirstOrDefault();
				if (u != null)
				{
					HttpContext.Session.SetString("username", account.Username.ToString());
					return RedirectToAction("Index", "Home");
				}
			}
			return View();
		}

		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			HttpContext.Session.Remove("username");
			return RedirectToAction("Login", "Access");
		}
	}
}
