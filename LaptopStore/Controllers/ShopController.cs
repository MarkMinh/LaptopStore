﻿using Microsoft.AspNetCore.Mvc;

namespace LaptopStore.Controllers
{
	public class ShopController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
