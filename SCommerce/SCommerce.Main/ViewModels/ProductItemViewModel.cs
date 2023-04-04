using Prism.Mvvm;
using SCommerce.Main.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace SCommerce.Main.ViewModels
{
	public class ProductItemViewModel : BindableBase
	{
		private string imagePath;

		public int Id { get; set; }

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

		private BitmapImage thumbnail;
		public BitmapImage Thumbnail
		{
			get { return thumbnail; }
			set { SetProperty(ref thumbnail, value); }
		}

		public ProductItemViewModel(Product product)
		{
			Id = product.Id;
			Title = product.Title;
			Price = product.Price;
			imagePath = product.Images.FirstOrDefault()?.Path;
		}

		public async void LoadThumbnail()
		{
			if (imagePath != null)
			{
				var path = Path.Combine("images", imagePath);

				var file = await ApplicationData.Current.LocalFolder.GetFileAsync(path);

				using (var stream = file.OpenReadAsync().AsTask().Result)
				{
					var bi = new BitmapImage();
					bi.SetSource(stream);

					Thumbnail = bi;
				}
			}

		}
	}
}
