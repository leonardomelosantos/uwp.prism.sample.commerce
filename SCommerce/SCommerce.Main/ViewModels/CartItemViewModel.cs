using Prism.Mvvm;
using SCommerce.Main.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCommerce.Main.ViewModels
{
	public class CartItemViewModel : BindableBase
	{
		private int productId;
		public int ProductId
		{
			get { return productId; }
			set { SetProperty(ref productId, value); }
		}

		private string title;
		public string Title
		{
			get { return title; }
			set { SetProperty(ref title, value); }
		}

		private double price;
		public double Price
		{
			get { return price; }
			set { SetProperty(ref price, value); }
		}

		private int quantity;
		public int Quantity
		{
			get { return quantity; }
			set { SetProperty(ref quantity, value); }
		}

		private string image;
		public string Image
		{
			get { return image; }
			set { SetProperty(ref image, value); }
		}

		private Action<int, int> addItemCallback;
		private Action<int, int> subtractItemCallback;
		private Action<int> removeItemCallback;

		public void Add()
		{
			this.addItemCallback?.Invoke(ProductId, 1);
		}

		public void Subtract()
		{
			this.subtractItemCallback?.Invoke(ProductId, 1);
		}

		public void Remove()
		{
			this.removeItemCallback?.Invoke(ProductId);
		}

		#region Factory

		public static CartItemViewModel Create(
			CartEntry model,
			Action<int, int> addItemCallback, 
			Action<int, int> subtractItemCallback, 
			Action<int> removeItemCallback)
		{
			return new CartItemViewModel()
			{
				Image = model.Image,
				Price = model.Price,
				ProductId = model.ProductId,
				Quantity = model.Quantity,
				Title = model.Title,
				addItemCallback = addItemCallback,
				subtractItemCallback = subtractItemCallback,
				removeItemCallback = removeItemCallback
			};

		}

		#endregion

	}
}
