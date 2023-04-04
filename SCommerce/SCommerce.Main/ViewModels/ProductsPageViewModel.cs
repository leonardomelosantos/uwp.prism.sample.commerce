using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using SCommerce.Main.Common;
using SCommerce.Main.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace SCommerce.Main.ViewModels
{
	public class ProductsPageViewModel : ViewModelBase
	{
		private readonly IProductService productService;
		private readonly INavigationService navigationService;


		private ObservableCollection<ProductItemViewModel> items;
		public ObservableCollection<ProductItemViewModel> Items
		{
			get { return items; }
			set { SetProperty(ref items, value); }
		}

		public ProductsPageViewModel(IProductService productService, INavigationService navigationService)
		{
			this.productService = productService;
			this.navigationService = navigationService;
		}

		public override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
		{
			base.OnNavigatedTo(e, viewModelState);

			var products = await productService.ListAsync();

			var list = products.Select(i => new ProductItemViewModel(i));

			Items = new ObservableCollection<ProductItemViewModel>(list);
		}

		public void OpenProductDetails(object sender, ItemClickEventArgs e)
		{
			if(e.ClickedItem is ProductItemViewModel item)
			{
				navigationService.Navigate(PageTokens.ProductDetailsPage, item.Id);
			}
		}
	}
}
