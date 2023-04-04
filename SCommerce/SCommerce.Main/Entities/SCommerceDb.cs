using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCommerce.Main.Entities
{
	public class SCommerceDb : DbContext
	{
		public DbSet<Product> Products { get; set;  }

		public DbSet<ProductImage> Images { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite("DataSource=data.db");

	}
}
