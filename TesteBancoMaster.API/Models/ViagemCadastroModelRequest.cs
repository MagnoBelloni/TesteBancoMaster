using System.ComponentModel.DataAnnotations;

namespace TesteBancoMaster.API.Models
{
    public class ViagemCadastroModelRequest
    {
        ///<example>GRU</example>
        [Required(ErrorMessage = "O atributo {0} é obrigatório")]
        public string Origem { get; set; }

        ///<example>BRC</example>
        [Required(ErrorMessage = "O atributo {0} é obrigatório")]
        public string Destino { get; set; }
        
        ///<example>10.50</example>
        public decimal ValorPassagem { get; set; }
    }
}
