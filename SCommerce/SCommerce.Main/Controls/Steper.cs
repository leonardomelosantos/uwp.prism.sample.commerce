using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace SCommerce.Main.Controls
{
	/// <summary>
	/// Componente criado do zero. (New Item > "Templated Control")
	/// </summary>
	[TemplatePart(Name = PART_AddButton, Type = typeof(Button))]
	[TemplatePart(Name = PART_SubtractButton, Type = typeof(Button))]
	[TemplatePart(Name = PART_RemoveButton, Type = typeof(Button))]
	public sealed class Steper : Control
	{
		private const string PART_AddButton = "PART_AddButton";
		private const string PART_SubtractButton = "PART_SubtractButton";
		private const string PART_RemoveButton = "PART_RemoveButton";

		/// <summary>
		/// Estados sendo definidos
		/// </summary>
		private const string STATE_NORMAL = "Normal";
		private const string STATE_MANY_ITEMS = "ManyItems";

		#region DependencyProperties

		public int Value
		{
			get { return (int)GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ValueProperty =
			DependencyProperty.Register("Value", typeof(int), typeof(Steper), new PropertyMetadata(0, OnValuePropertyChanged));

		#endregion

		#region Eventos

		public event RoutedEventHandler OnRemoved;
		public event RoutedEventHandler OnAdded;
		public event RoutedEventHandler OnSubtracted;

		#endregion

		/// <summary>
		/// Este método não vem por padrão quando criamos uma DependencyProperty.
		/// </summary>
		/// <param name="d"></param>
		/// <param name="e"></param>
		private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var self = (Steper)d;
			self.UpdateVisualState();
		}

		public Steper()
		{
			this.DefaultStyleKey = typeof(Steper);
		}

		/// <summary>
		/// Sobrescrevendo intencionamente para atualizar o state do controle assim que o template é aplicado.
		/// </summary>
		protected override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			AssinarEventosBotoes();
			
			UpdateVisualState(); // Chamando forçadamente
		}

		private void AssinarEventosBotoes()
		{
			if (GetTemplateChild(PART_AddButton) is Button addBtn)
			{
				addBtn.Click += OnAddClick;
			}
			if (GetTemplateChild(PART_SubtractButton) is Button subtractBtn)
			{
				subtractBtn.Click += OnSubtractClick;
			}
			if (GetTemplateChild(PART_RemoveButton) is Button removeBtn)
			{
				removeBtn.Click += OnSubtractClick;
			}
		}

		private void OnAddClick(object sender, RoutedEventArgs e)
		{
			Value++;
			OnAdded?.Invoke(this, new RoutedEventArgs());
		}

		private void OnSubtractClick(object sender, RoutedEventArgs e)
		{
			Value--;
			OnSubtracted?.Invoke(this, new RoutedEventArgs());

			if (Value == 0)
			{
				OnRemoved?.Invoke(this, new RoutedEventArgs());
			}
		}

		/// <summary>
		/// Método criado manualmente e sendo chamado por outros dois. É a melhor abordagem.
		/// </summary>
		private void UpdateVisualState()
		{
			var state = (Value <= 1 ? STATE_NORMAL : STATE_MANY_ITEMS);
			VisualStateManager.GoToState(this, state, false);
		}
	}
}
