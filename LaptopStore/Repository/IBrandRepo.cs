using LaptopStore.Models.DataModels;

namespace LaptopStore.Repository
{
	public interface IBrandRepo
	{
		Brand Add(Brand brand);
		Brand Update(Brand brand);
		Brand Delete(int brandId);
		Brand Get(int brandId);
		IEnumerable<Brand> GetAll();
	}
}
