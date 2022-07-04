using System.ComponentModel.DataAnnotations;

namespace TesteBancoMaster.API.Models
{
    public class ViagemObterRotaCustoBaixoModelRequest
    {
        ///<example>GRU</example>
        [Required(ErrorMessage = "O atributo {0} é obrigatório")]
        public string Origem { get; set; }

        ///<example>CDG</example>
        [Required(ErrorMessage = "O atributo {0} é obrigatório")]
        public string Destino { get; set; }
    }
}
