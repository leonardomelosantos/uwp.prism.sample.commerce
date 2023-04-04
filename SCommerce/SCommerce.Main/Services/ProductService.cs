using SCommerce.Main.Entities;
using SCommerce.Main.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace SCommerce.Main.Services
{
	public class ProductService : IProductService
	{
		private readonly IProductRepository productRepository;

		public ProductService(IProductRepository productRepository)
		{
			this.productRepository = productRepository;
		}

		public async Task<Product> CreateAsync(string title, string description, int rating, double price, IList<Windows.Storage.StorageFile> files)
		{
			List<ProductImage> imagesCopied = await CopyImages(files);

			var newProduct = new Product()
			{
				Title = title,
				Description = description,
				Rating = rating,
				Price = price,
				Images = imagesCopied
			};

			await this.productRepository.AddAsync(newProduct);

			return newProduct;
		}

		private static async Task<List<ProductImage>> CopyImages(IList<StorageFile> files)
		{
			var destFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("Images", CreationCollisionOption.OpenIfExists);
			var imagesCopied = new List<ProductImage>();
			foreach (var file in files)
			{
				var imageCopied = await file.CopyAsync(destFolder, file.Name, NameCollisionOption.GenerateUniqueName);
				imagesCopied.Add(new ProductImage() { Path = imageCopied.Name });
			}
			return imagesCopied;
		}

		public Task<List<Product>> ListAsync() => productRepository.ListAsync();

		public Task<Product> FindAsync(int id) => productRepository.FindAsync(id);
	}
}
