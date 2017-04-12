using EZCompressor.UI.ViewModels;
using System.Windows.Controls;

namespace EZCompressor.UI.Pages
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            DataContext = new MainPageViewModel(this);  

        }

    }
}
