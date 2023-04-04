using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCommerce.Main.Entities
{
	public class Product
	{
		public int Id { get; set; }
		
		public string Title { get; set; }

		public string Description { get; set; }

		public double Price { get; set; }
		
		public int Rating { get; set; }

		public List<ProductImage> Images { get; set; }

	}
}
