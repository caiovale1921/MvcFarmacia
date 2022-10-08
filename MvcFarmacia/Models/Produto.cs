using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcFarmacia.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Descrição")]
        public string? Descricao { get; set; }
        [DisplayName("Preço")]
        [Range(0.1, 99999.99, ErrorMessage = "O Preço de Venda deve estar entre " + "0,1 e 99999,99.")]
        public decimal Preco { get; set; }
        [ForeignKey("TipoProduto")]
        [DisplayName("Tipo de Produto")]
        public int IdTipoProduto { get; set; }
        public TipoProduto? TipoProduto { get; set; }
        [ForeignKey("Farmacia")]
        [DisplayName("Farmácia")]
        public int IdFarmacia { get; set; }
        public Farmacia? Farmacia { get; set; }
    }
}
