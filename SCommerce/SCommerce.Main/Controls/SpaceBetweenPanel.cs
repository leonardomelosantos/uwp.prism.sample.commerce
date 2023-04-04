using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace SCommerce.Main.Controls
{
	public class SpaceBetweenPanel : Panel
	{
		double itemsWidth = 0;

		/// <summary>
		/// Sobrescrevendo intencionalmente.
		/// </summary>
		/// <param name="availableSize"></param>
		/// <returns></returns>
		protected override Size MeasureOverride(Size availableSize)
		{
			var remainingSize = availableSize;
			var size = new Size(0, 0);

			foreach (var item in Children)
			{
				item.Measure(remainingSize);

				if (item.DesiredSize.Height > size.Height)
				{
					size.Height = item.DesiredSize.Height;
				}
				size.Width += item.DesiredSize.Width;
				
				remainingSize.Width -= item.DesiredSize.Width;
			}

			itemsWidth = size.Width;
			return size;
		}

		/// <summary>
		/// Sobrescrevendo intencionalmente. É onde está a lógica dos itens.
		/// </summary>
		/// <param name="finalSize"></param>
		/// <returns></returns>
		protected override Size ArrangeOverride(Size finalSize)
		{
			var size = new Size();
			double spaceBetween = (finalSize.Width - itemsWidth) / (Children.Count - 1);
			
			foreach (var item in Children)
			{
				if (item.DesiredSize.Height > size.Height)
				{
					size.Height = item.DesiredSize.Height;
				}

				item.Arrange(new Rect(size.Width, 0, item.DesiredSize.Width, item.DesiredSize.Height));

				size.Width += item.DesiredSize.Width + spaceBetween;
			}

			return size;
		}
	}
}
