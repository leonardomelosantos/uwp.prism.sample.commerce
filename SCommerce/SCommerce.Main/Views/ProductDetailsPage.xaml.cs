using SCommerce.Main.ViewModels;
using Windows.UI.Xaml.Controls;

namespace SCommerce.Main.Views
{
    public sealed partial class ProductDetailsPage : Page
    {
        
        /// <summary>
        /// Atenção:  o uso do "=>" indica que o corpo da atribuição será executado toda vez que a property for acessada.
        /// </summary>
        public ProductDetailsPageViewModel ViewModel => (ProductDetailsPageViewModel)this.DataContext;

        public ProductDetailsPage()
        {
            this.InitializeComponent();

            // Comentando o trecho abaixo, depois que passou a usar o Prism,
            // porque o Prism já gerencia isso.
            //this.DataContext = new ProductDetailsPageViewModel(); 
        }
    }
}
