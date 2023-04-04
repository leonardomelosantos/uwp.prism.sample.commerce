using SCommerce.Main.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SCommerce.Main.Repositories
{
	public interface IProductRepository
	{
		Task AddAsync(Product product);

		Task<List<Product>> ListAsync();
		Task<Product> FindAsync(int id);
	}
}