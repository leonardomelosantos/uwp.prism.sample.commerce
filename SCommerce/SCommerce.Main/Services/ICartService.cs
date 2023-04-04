using SCommerce.Main.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SCommerce.Main.Services
{
	public interface ICartService
	{
		List<CartEntry> GetItemsForCheckout();

		Task AddAsync(int productId, int quantity);

		void Remove(int productId);

		void Subtract(int productId, int quantity);
	}
}