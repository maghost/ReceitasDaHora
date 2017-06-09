using ReceitasDaHora.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReceitasDaHora.Services
{
    public interface IReceitasDaHoraApiService
    {
        Task<List<Receita>> GetReceitasByCategoriaIdAsync(string categoriaId);
        Task<List<Categoria>> GetCategoriasAsync();
        Task<List<Receita>> GetReceitasByFilterAsync(string filter);
        Task<Receita> GetReceitaByIdAsync(string receitaId);
    }
}
