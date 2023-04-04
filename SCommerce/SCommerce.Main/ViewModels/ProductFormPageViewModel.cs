using Prism.Windows.Mvvm;
using SCommerce.Main.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace SCommerce.Main.ViewModels
{
	public class ProductFormPageViewModel : ViewModelBase
	{
		private readonly IProductService productService;


		private string title;
		public string Title
		{
			get { return title; }
			set { SetProperty(ref title, value); }
		}

		private string description;
		public string Description
		{
			get { return description; }
			set { SetProperty(ref description, value); }
		}

		private int rating;
		public int Rating
		{
			get { return rating; }
			set { SetProperty(ref rating, value); }
		}

		private double price;
		public double Price
		{
			get { return price; }
			set { SetProperty(ref price, value); }
		}

		private ObservableCollection<StorageFile> images;
		public ObservableCollection<StorageFile> Images
		{
			get { return images; }
			set { SetProperty(ref images, value); }
		}

		public ProductFormPageViewModel(IProductService productService)
		{
			this.productService = productService;

			Images = new ObservableCollection<StorageFile>();
		}

		public async void Save()
		{
			await productService.CreateAsync(Title, 
											Description, 
											Rating, 
											Price, 
											Images);

			CoreApplication.GetCurrentView().CoreWindow.Close(); // Fechando a janela 
		}

		public async void AddImage()
		{
			var picker = new FileOpenPicker();
			picker.ViewMode = PickerViewMode.Thumbnail;
			picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
			picker.FileTypeFilter.Add(".jpg");
			picker.FileTypeFilter.Add(".jpeg");
			picker.FileTypeFilter.Add(".png");

			var files = await picker.PickMultipleFilesAsync();

			if(files != null)
			{
				foreach (var file in files)
				{
					Images.Add(file);
				}
			}
		}
	}
}
