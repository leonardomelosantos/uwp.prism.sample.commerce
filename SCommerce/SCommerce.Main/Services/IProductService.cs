using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using SCommerce.Main.Entities;
using Windows.Storage;

namespace SCommerce.Main.Services
{
	public interface IProductService
	{
		Task<Product> FindAsync(int id);

		Task<Product> CreateAsync(string title, string description, int rating, double price, IList<Windows.Storage.StorageFile> files);

		Task<List<Product>> ListAsync();
	}
}