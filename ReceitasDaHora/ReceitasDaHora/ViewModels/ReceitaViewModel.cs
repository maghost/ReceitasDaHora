using ReceitasDaHora.Models;

namespace ReceitasDaHora.ViewModels
{
    public class ReceitaViewModel : BaseViewModel
    {
        public Receita Receita { get; }

        public ReceitaViewModel(Receita receita)
        {
            Receita = receita;
        }
    }
}
