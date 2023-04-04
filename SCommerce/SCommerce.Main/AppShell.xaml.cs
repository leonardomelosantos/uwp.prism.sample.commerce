using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace SCommerce.Main
{
	public sealed partial class AppShell : Page
	{
		public AppShell()
		{
			this.InitializeComponent();
		}

		/// <summary>
		/// Método adicionado manualmete.
		/// </summary>
		/// <param name="frame">Este parâmetro será passado pelo Prism</param>
		public void SetFrame(Frame frame)
		{
			FrameContainer.Content = frame;
		}
	}
}
