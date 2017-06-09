using ReceitasDaHora.Helpers;
using ReceitasDaHora.Services;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ReceitasDaHora.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        AzureService azureService;
        ICommand loginCommand;

        public ICommand LoginCommand =>
            loginCommand ?? (loginCommand = new Command(async () => await ExecuteLoginCommandAsync()));

        public LoginViewModel()
        {
            azureService = DependencyService.Get<AzureService>();
        }
        private async Task ExecuteLoginCommandAsync()
        {
            if (IsBusy || !(await LoginAsync()))
                return;
            else
            {
                await PushAsync<MainViewModel>();
                RemovePageFromStack();
            }
        }

        private void RemovePageFromStack()
        {
            INavigation navigation = Application.Current.MainPage.Navigation;
            var existingPage = navigation.NavigationStack.ToList();
            foreach (var page in existingPage)
            {
                if (page.GetType() == typeof(LoginPage))
                    navigation.RemovePage(page);
            }
        }

        public Task<bool> LoginAsync()
        {
            if (Settings.IsLoggedIn)
            {
                return Task.FromResult(true);
            }

            return azureService.LoginAsync();
        }
    }
}
