using Prism.Events;
using SCommerce.Main.Events;
using SCommerce.Main.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCommerce.Main.Services
{
	public class CartService : ICartService
	{
		private Dictionary<int, CartEntry> cart;

		private readonly IEventAggregator eventAggregator;
		private readonly IProductService productService;

		public CartService(IEventAggregator eventAggregator,
			IProductService productService)
		{
			this.cart = new Dictionary<int, CartEntry>();

			this.productService = productService;
			this.eventAggregator = eventAggregator;
		}

		public async Task AddAsync(int productId, int quantity)
		{
			if (cart.ContainsKey(productId))
			{
				cart[productId].Quantity += quantity;
			}
			else
			{
				var productIntance = await this.productService.FindAsync(productId);

				var cartEntry = new CartEntry()
				{
					ProductId = productId,
					Quantity = quantity,
					Price = productIntance.Price,
					Title = productIntance.Title,
					Image = productIntance.Images.FirstOrDefault().Path
				};

				cart.Add(productId, cartEntry);
			}

			AddedToCartEvent.Payload payload = new AddedToCartEvent.Payload()
			{
				ProductId = productId,
				Quantity = quantity
			};
			this.eventAggregator.GetEvent<AddedToCartEvent>().Publish(payload);
		}

		public void Subtract(int productId, int quantity)
		{
			if (cart.ContainsKey(productId))
			{
				cart[productId].Quantity -= quantity;

				var payload = new SubtractedToCartEvent.Payload()
				{
					ProductId = productId,
					Quantity = quantity
				};
				this.eventAggregator.GetEvent<SubtractedToCartEvent>().Publish(payload);
			}
		}

		public void Remove(int productId)
		{
			if (cart.ContainsKey(productId))
			{
				cart.Remove(productId);
				
				var payload = new RemovedFromCartEvent.Payload()
				{
					ProductId = productId
				};
				this.eventAggregator.GetEvent<RemovedFromCartEvent>().Publish(payload);
			}
		}

		public List<CartEntry> GetItemsForCheckout()
		{
			return this.cart.Values.ToList();
		}
	}
}
