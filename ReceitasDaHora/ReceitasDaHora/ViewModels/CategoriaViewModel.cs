using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ReceitasDaHora.Models;
using ReceitasDaHora.Services;
using Xamarin.Forms;

namespace ReceitasDaHora.ViewModels
{
    public class CategoriaViewModel : BaseViewModel
    {
        private readonly IReceitasDaHoraApiService _receitasDaHoraApiService;
        private readonly Categoria _categoria;

        public ObservableCollection<Receita> Receitas { get; }

        public Command<Receita> ShowReceitaCommand { get; }

        public CategoriaViewModel(IReceitasDaHoraApiService receitasDaHoraApiService, Categoria categoria)
        {
            _receitasDaHoraApiService = receitasDaHoraApiService;
            _categoria = categoria;

            Receitas = new ObservableCollection<Receita>();
            ShowReceitaCommand = new Command<Receita>(ExecuteShowReceitaCommand);
        }

        private async void ExecuteShowReceitaCommand(Receita receita)
        {
            await PushAsync<ReceitaViewModel>(receita);
        }

        public async Task LoadAsync()
        {
            var receitas = await _receitasDaHoraApiService.GetReceitasByCategoriaIdAsync(_categoria.Id);

            Receitas.Clear();
            foreach (var receita in receitas)
            {
                Receitas.Add(receita);
            }
        }
    }
}
