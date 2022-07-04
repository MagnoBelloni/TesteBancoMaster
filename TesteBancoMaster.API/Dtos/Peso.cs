namespace TesteBancoMaster.API.Dtos
{
    public class Peso
    {
        public Node Origem { get; }
        public decimal Valor { get; }

        public Peso(Node origem, decimal valor)
        {
            Origem = origem;
            Valor = valor;
        }
    }
}
