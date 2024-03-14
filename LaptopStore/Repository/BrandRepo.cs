using LaptopStore.Models;
using LaptopStore.Models.DataModels;

namespace LaptopStore.Repository
{
	public class BrandRepo : IBrandRepo
	{
		private readonly LaptopStoreContext _context;
		public BrandRepo(LaptopStoreContext context)
		{
			_context = context;	
		}
		public Brand Add(Brand brand)
		{
			_context.Brands.Add(brand);
			_context.SaveChanges();
			return brand;
		}

		public Brand Delete(int brandId)
		{
			throw new NotImplementedException();
		}

		public Brand Get(int brandId)
		{
			return _context.Brands.SingleOrDefault(b => b.Id == brandId);
		}

		public IEnumerable<Brand> GetAll()
		{
			return _context.Brands;
		}

		public Brand Update(Brand brand)
		{
			_context.Brands.Update(brand);
			_context.SaveChanges();
			return brand;
		}
	}
}
