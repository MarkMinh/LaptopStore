using LaptopStore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LaptopStore.ViewComponents
{
	public class BrandMenuViewComponent : ViewComponent
	{
		private readonly IBrandRepo brandRepo;
		public BrandMenuViewComponent(IBrandRepo brandRepo)
		{
			this.brandRepo = brandRepo;
		}
		public IViewComponentResult Invoke()
		{
			var brand = brandRepo.GetAll().OrderBy(x => x.BrandName);
			return View(brand);
		}
	}
}
