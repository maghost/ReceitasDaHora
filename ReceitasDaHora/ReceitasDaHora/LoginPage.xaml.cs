using ReceitasDaHora.ViewModels;

namespace ReceitasDaHora
{
    public partial class LoginPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();
        }
    }
}