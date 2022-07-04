namespace TesteBancoMaster.API.Dtos
{
    public class DadoVisitado
    {
        readonly List<Node> _visiteds =
            new List<Node>();

        readonly Dictionary<Node, Peso> _Pesos =
            new Dictionary<Node, Peso>();

        readonly List<Node> _scheduled =
            new List<Node>();

        public void RegisterVisitTo(Node node)
        {
            if (!_visiteds.Contains(node))
                _visiteds.Add((node));
        }

        public bool WasVisited(Node node)
        {
            return _visiteds.Contains(node);
        }

        public void UpdatePeso(Node node, Peso newPeso)
        {
            if (!_Pesos.ContainsKey(node))
            {
                _Pesos.Add(node, newPeso);
            }
            else
            {
                _Pesos[node] = newPeso;
            }
        }

        public Peso QueryPeso(Node node)
        {
            Peso result;
            if (!_Pesos.ContainsKey(node))
            {
                result = new Peso(null, int.MaxValue);
                _Pesos.Add(node, result);
            }
            else
            {
                result = _Pesos[node];
            }
            return result;
        }

        public void ScheduleVisitTo(Node node)
        {
            _scheduled.Add(node);
        }

        public bool HasScheduledVisits => _scheduled.Count > 0;

        public Node GetNodeToVisit()
        {
            var ordered = from n in _scheduled
                          orderby QueryPeso(n).Valor
                          select n;

            var result = ordered.First();
            _scheduled.Remove(result);
            return result;
        }

        public bool HasComputedPathToOrigin(Node node)
        {
            return QueryPeso(node).Origem != null;
        }

        public IEnumerable<Node> ComputedPathToOrigin(Node node)
        {
            var n = node;
            while (n != null)
            {
                yield return n;
                n = QueryPeso(n).Origem;
            }
        }
    }
}
