namespace ReceitasDaHora.Models
{
    public class Receita
    {
        public string Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public string Rendimento { get; set; }
        public string TempoPreparo { get; set; }
        public string Banner { get; set; }
        public string ModoPreparo { get; set; }
        public string Ingredientes { get; set; }
        public string Fonte { get; set; }
    }
}
