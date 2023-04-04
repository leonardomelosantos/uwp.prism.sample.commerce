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
	public sealed class Rating : Control
	{
		public int Value
		{
			get { return (int)GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ValueProperty =
			DependencyProperty.Register("Value", typeof(int), typeof(Rating), new PropertyMetadata(1, OnValuePropertyChanged));

		private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var self = (Rating)d;
			self.AtualizarState();
		}

		public Rating()
		{
			this.DefaultStyleKey = typeof(Rating);
		}

		private void AtualizarState()
		{
			var val = Math.Clamp(Value, 1, 5);
			var state = $"Rating{val}";
			var result = VisualStateManager.GoToState(this, state, false);
		}

		/// <summary>
		/// Sobrescreveu este método porque ocorreu um pequeno problema. 
		/// O state não estava atualizando porque o template ainda não estava sendo aplicado.
		/// </summary>
		protected override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			AtualizarState(); // Forçando a chamada da atualização do estado.
		}
	}
}
