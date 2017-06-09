using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ReceitasDaHora.Models;
using ReceitasDaHora.Services;
using Xamarin.Forms;

namespace ReceitasDaHora.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IReceitasDaHoraApiService _receitasDaHoraApiService;
        public ObservableCollection<Categoria> Categorias { get; }

        public Command AboutCommand { get; }

        public Command SearchCommand { get; }

        public Command<Categoria> ShowCategoriaCommand { get; }

        public MainViewModel(IReceitasDaHoraApiService receitasDaHoraApiService)
        {
            _receitasDaHoraApiService = receitasDaHoraApiService;
            Categorias = new ObservableCollection<Categoria>();
            AboutCommand = new Command(ExecuteAboutCommand);
            SearchCommand = new Command(ExecuteSearchCommand);
            ShowCategoriaCommand = new Command<Categoria>(ExecuteShowCategoriaCommand);
        }

        private async void ExecuteSearchCommand()
        {
            await PushAsync<SearchViewModel>();
        }

        private async void ExecuteShowCategoriaCommand(Categoria categoria)
        {
            await PushAsync<CategoriaViewModel>(categoria);
        }

        private async void ExecuteAboutCommand()
        {
            await PushAsync<AboutViewModel>();
        }

        public async Task LoadAsync()
        {
            var categorias = await _receitasDaHoraApiService.GetCategoriasAsync();

            Categorias.Clear();
            foreach (var categoria in categorias)
            {
                Categorias.Add(categoria);
            }
        }
    }
}
