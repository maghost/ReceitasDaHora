using System.Collections.ObjectModel;
using ReceitasDaHora.Models;
using ReceitasDaHora.Services;
using Xamarin.Forms;

namespace ReceitasDaHora.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
        private readonly IReceitasDaHoraApiService _receitasDaHoraApiService;
        private string _searchTerm;

        public string SearchTerm
        {
            get { return _searchTerm; }
            set
            {
                SetProperty(ref _searchTerm, value);
                SearchCommand.ChangeCanExecute();
            }
        }

        public Command SearchCommand { get; }

        public ObservableCollection<Receita> SearchResults { get; }

        public Command<Receita> ShowReceitaCommand { get; }

        public SearchViewModel(IReceitasDaHoraApiService receitasDaHoraApiService)
        {
            _receitasDaHoraApiService = receitasDaHoraApiService;

            SearchResults = new ObservableCollection<Receita>();
            SearchCommand = new Command(ExecuteSearchCommand, CanExecuteSearchCommand);
            ShowReceitaCommand = new Command<Receita>(ExecuteShowReceitaCommand);
        }

        private async void ExecuteShowReceitaCommand(Receita receita)
        {
            await PushAsync<ReceitaViewModel>(receita);
        }

        private bool CanExecuteSearchCommand()
        {
            return string.IsNullOrWhiteSpace(SearchTerm) == false;
        }

        private async void ExecuteSearchCommand()
        {
            var searchResults = await _receitasDaHoraApiService.GetReceitasByFilterAsync(SearchTerm);

            SearchResults.Clear();
            if (searchResults == null)
            {
                await DisplayAlert("ReceitasDaHora", "Nenhum resultado encontrado.", "Ok");
                return;
            }

            foreach (var searchResult in searchResults)
            {
                SearchResults.Add(searchResult);
            }
        }
    }
}
