using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCommerce.Main.Models
{
	public class CartEntry
	{
		public int ProductId { get; set; }

		public string Title { get; set; }

		public double Price { get; set; }

		public int Quantity { get; set; }

		public string Image { get; set; }
	}
}
