namespace TesteBancoMaster.API.Dtos
{
    //Arestas
    public class Edge
    {
        public decimal Valor { get; }
        public Node Origem { get; }
        public Node Destino { get; }

        public Edge(decimal valor, Node origem, Node destino)
        {
            Valor = valor;
            Origem = origem;
            Destino = destino;

            origem.Assign(this);
            destino.Assign(this);
        }

        public static Edge Create(decimal value, Node node1, Node node2)
        {
            return new Edge(value, node1, node2);
        }
    }
}
