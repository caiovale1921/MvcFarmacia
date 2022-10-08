using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcFarmacia.Models
{
    public class TipoProduto
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Descrição")]
        public string? Descricao { get; set; }
    }
}
