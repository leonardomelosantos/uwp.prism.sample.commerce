using Prism.Events;
using Prism.Mvvm;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using SCommerce.Main.Common;
using SCommerce.Main.Events;
using SCommerce.Main.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace SCommerce.Main.ViewModels
{

	public class HeaderViewModel : BindableBase
	{
		private readonly INavigationService navigationService;
		private readonly IEventAggregator eventAggregator;

		private int cartQuantity;
		public int CartQuantity
		{
			get { return cartQuantity; }
			set { SetProperty(ref cartQuantity, value); }
		}

		public HeaderViewModel(
			INavigationService navigationService,
			IEventAggregator eventAggregator)
		{
			this.navigationService = navigationService;
			this.eventAggregator = eventAggregator;

			this.eventAggregator.GetEvent<AddedToCartEvent>().Subscribe(HandleAddedToCartEvent);
			this.eventAggregator.GetEvent<SubtractedToCartEvent>().Subscribe(HandleSubtractedToCartEvent);
		}

		private void HandleSubtractedToCartEvent(SubtractedToCartEvent.Payload payload)
		{
			this.CartQuantity -= payload.Quantity;
		}

		private void HandleAddedToCartEvent(AddedToCartEvent.Payload payload)
		{
			this.CartQuantity += payload.Quantity;
		}

		public void NavigateToCartPage()
		{
			this.navigationService.Navigate(PageTokens.CartPage, null);
		}

		/// <summary>
		/// Método que representa o clique do botão "+" na barra de navegação. 
		/// A receita abaixo é para abrir uma page numa nova janela.
		/// </summary>
		public async void OpenAddProduct()
		{
			var view = CoreApplication.CreateNewView();
			int viewId = 0;

			await view.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => 
				{
					var page = new ProductFormPage();
					Window.Current.Content = page;
					Window.Current.Activate();
					viewId = ApplicationView.GetForCurrentView().Id;
				}
			);

			await ApplicationViewSwitcher.TryShowAsStandaloneAsync(viewId);
		}
	}
}
