using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TesteBancoMaster.Infra.Entities
{
    [Table("viagem")]
    public class Viagem
    {
        //EF ctor
        public Viagem() { }

        [Key]
        [Column]
        public int Id { get; set; }

        [Column]
        public string Origem { get; set; }

        [Column]
        public string Destino { get; set; }

        [Column]
        public decimal ValorPassagem { get; set; }
    }
}
