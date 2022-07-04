using System.Collections;

namespace TesteBancoMaster.API.Dtos
{
    //Vertices
    public class Node
    {
        public string Nome { get; }
        public Node(string nome)
        {
            Nome = nome;
        }

        readonly List<Edge> _edges = new();
        public IEnumerable<Edge> Edges => _edges;

        public IEnumerable<NeighborhoodInfo> Neighbors
        {
            get
            {
                return from edge in Edges
                       select new NeighborhoodInfo(
                           edge.Origem == this ? edge.Destino : edge.Origem,
                           edge.Valor
                           );
            }
        }

        public void Assign(Edge edge)
        {
            _edges.Add(edge);
        }

        public void ConnectTo(Node nodeDestino, decimal valor)
        {
            Edge.Create(valor, this, nodeDestino);
        }

        public struct NeighborhoodInfo
        {
            public Node Node { get; }
            public decimal Valor { get; }

            public NeighborhoodInfo(Node node, decimal valor)
            {
                Node = node;
                Valor = valor;
            }
        }
    }
}
