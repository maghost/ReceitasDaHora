using Version.Plugin;

namespace ReceitasDaHora.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public string Versao => CrossVersion.Current.Version;
    }
}
