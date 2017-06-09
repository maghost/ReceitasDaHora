using ReceitasDaHora.Services;
using ReceitasDaHora.ViewModels;
using Xamarin.Forms;

namespace ReceitasDaHora
{
    public partial class MainPage
    {
        private MainViewModel ViewModel => BindingContext as MainViewModel;

        public MainPage()
        {
            InitializeComponent();
            var receitasDaHoraApiService = DependencyService.Get<IReceitasDaHoraApiService>();
            BindingContext = new MainViewModel(receitasDaHoraApiService);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (ViewModel != null)
                await ViewModel.LoadAsync();
        }
    }
}
