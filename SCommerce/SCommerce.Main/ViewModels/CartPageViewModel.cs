using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using SCommerce.Main.Models;
using SCommerce.Main.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCommerce.Main.ViewModels
{
	public class CartPageViewModel : ViewModelBase
	{
		private readonly ICartService cartService;

		#region Properties (propp)

		private ObservableCollection<CartItemViewModel> items;
		public ObservableCollection<CartItemViewModel> Items
		{
			get { return items; }
			set { SetProperty(ref items, value); }
		}

		private List<string> steps;
		public List<string> Steps
		{
			get { return steps; }
			set { SetProperty(ref steps, value); }
		}

		private string selectedStep;
		public string SelectedStep
		{
			get { return selectedStep; }
			set { SetProperty(ref selectedStep, value); }
		}

		#endregion

		public CartPageViewModel(ICartService cartService)
		{
			this.cartService = cartService;
			
			InitializeSteps();
		}

		public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
		{
			base.OnNavigatedTo(e, viewModelState);

			InitializeItemsViewModel();
		}

		private void InitializeSteps()
		{
			Steps = new List<string>() { "Checkout", "Address", "Payment" };
			SelectedStep = Steps.FirstOrDefault();
		}

		private void InitializeItemsViewModel()
		{
			var list = cartService.GetItemsForCheckout()
							.Select(item => CartItemViewModel.Create(item, AddItem, SubtractItem, RemoveItem)).ToList();
			this.Items = new ObservableCollection<CartItemViewModel>(list);
		}

		#region Métodos usados pelo controle de Etapas do carrinho

		int count = 0;
		public void ChangeSelectedStep()
		{
			count++;
			SelectedStep = Steps[count % Steps.Count];
		}

		#endregion

		#region Métodos referentes ao Stepper (add, diminuir, remover

		private void AddItem(int productId, int quantity)
		{
			cartService.AddAsync(productId, quantity);
		}

		private void SubtractItem(int productId, int quantity)
		{
			cartService.Subtract(productId, quantity);
		}

		private void RemoveItem(int productId)
		{
			cartService.Remove(productId);
			var itemToRemove = Items.FirstOrDefault(i => i.ProductId == productId);
			Items.Remove(itemToRemove);
		}

		#endregion
	}
}
