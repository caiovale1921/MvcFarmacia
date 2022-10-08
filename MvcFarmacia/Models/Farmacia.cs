using System.ComponentModel;

namespace MvcFarmacia.Models
{
    public class Farmacia
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public List<Produto>? Produtos { get; set; }
        [DisplayName("Endereço")]
        public string? Endereco { get; set; }
        public string? Telefone { get; set; }
    }
}
