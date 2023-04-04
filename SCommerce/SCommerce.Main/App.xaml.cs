using Microsoft.Practices.Unity;
using Prism.Unity.Windows;
using Prism.Windows.AppModel;
using SCommerce.Main.Common;
using SCommerce.Main.Entities;
using SCommerce.Main.Repositories;
using SCommerce.Main.Services;
using SCommerce.Main.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Resources;
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
    public sealed partial class App : PrismUnityApplication
    {
        /// <summary>
        /// É necesssário implementar o método abstrado abaixo, da classe herdada "PrismUnityApplication".
        /// É neste método onde decidimos qual a primeira página que será exibida na aplicação.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
		protected override async Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
		{

            // Trecho de código apenas para garantir que o banco de dados será criado.
            using (var db = new SCommerceDb())
			{
                await db.Database.EnsureCreatedAsync();
			}

            NavigationService.Navigate(PageTokens.ProductsPage, null);
		}

		/// <summary>
        /// Sobrescrevendo o método para adotar um menu customizado.
        /// </summary>
        /// <param name="rootFrame"></param>
        /// <returns></returns>
        protected override UIElement CreateShell(Frame rootFrame)
		{
            var appShell = new AppShell();
            appShell.SetFrame(rootFrame);
			return appShell;
		}

        /// <summary>
        /// IoC / Injeção de dependência
        /// </summary>
		protected override void ConfigureContainer()
		{
			base.ConfigureContainer();

            // Esta linha abaixo foi necessária adicionar para que funcione o "IResourceLoader" na injeção de dependência.
            // O "IResourceLoader" é utilizado para obter valores dos arquivos de Resources, em tempo de execução.
            Container.RegisterInstance(typeof(IResourceLoader), new ResourceLoaderAdapter(new ResourceLoader()), new ContainerControlledLifetimeManager());

            RegisterTypeIfMissing(typeof(IProductService), typeof(ProductService), true);
            RegisterTypeIfMissing(typeof(ICartService), typeof(CartService), true);

            RegisterTypeIfMissing(typeof(IProductRepository), typeof(ProductRepository), true);
        }
	}
}
