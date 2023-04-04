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
	/// Inicialmente, quando se criou este classe (escolhendo Templated Control), a classe era filha de "Control".
	/// Conforme pode ser visto abaixo, a classe pai foi modificada para AppBarButton, justamente porque o que se 
	/// está querendo é construir um componente com base num já existente.
	/// </summary>
	public sealed class BadgeAppBarButton : AppBarButton
	{
		private const string STATE_BADGE_HIDDEN = "BadgeHidden";
		private const string STATE_BADGE_VISIBLE = "BadgeVisible";

		/// <summary>
		/// DependencyProperty (usada em componentes).
		/// Tem um snipet que já cria um DependencyProperty, chamada "propdp".
		/// </summary>
		public string Badge
		{
			get { return (string)GetValue(BadgeProperty); }
			set { SetValue(BadgeProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Badge.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty BadgeProperty =
			DependencyProperty.Register("Badge", typeof(string), typeof(BadgeAppBarButton), new PropertyMetadata(string.Empty, OnBadgePropertyChanged));

		private static void OnBadgePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var badge = (e.NewValue as string);
			string state = (badge == string.Empty || badge == "0" ? STATE_BADGE_HIDDEN : STATE_BADGE_VISIBLE);
			VisualStateManager.GoToState((Control)d, state, false);
		}

		public BadgeAppBarButton()
		{
			this.DefaultStyleKey = typeof(BadgeAppBarButton);
		}
	}
}
