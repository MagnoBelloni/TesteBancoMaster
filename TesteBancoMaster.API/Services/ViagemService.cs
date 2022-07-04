using AutoMapper;
using TesteBancoMaster.API.Dtos;
using TesteBancoMaster.API.Models;
using TesteBancoMaster.API.Services.Interfaces;
using TesteBancoMaster.Infra.Entities;
using TesteBancoMaster.Infra.Repositories;

namespace TesteBancoMaster.API.Services
{
    public class ViagemService : Service, IViagemService
    {
        private readonly IViagemRepository _repository;
        private readonly IMapper _mapper;

        public ViagemService(IViagemRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<Viagem>> ObterTodos()
        {
            var resultado = await _repository.ObterTodos();
            return resultado;
        }

        public async Task<List<Viagem>> ObterDestinos(string origem)
        {
            var resultado = await _repository.ObterViagens(x => x.Origem == origem);
            return resultado;
        }

        public async Task<Viagem> CadastrarViagem(ViagemCadastroModelRequest request)
        {
            var viagemExistente = await _repository.ObterViagens(x => x.Origem == request.Origem && x.Destino == request.Destino);
            if (viagemExistente != null)
            {
                throw new Exception("Viagem já existente");
            }

            var viagem = _mapper.Map<Viagem>(request);
            await _repository.CadastrarViagem(viagem);
            await PersistirDados(_repository.UnitOfWork);

            return viagem;
        }

        public async Task<Viagem> AtualizarViagem(int idOrigem, ViagemAtualizarModelRequest request)
        {
            var viagemExistente = await _repository.ObterViagem(x => x.Id == idOrigem);
            if (viagemExistente == null)
            {
                throw new Exception("Viagem não existente");
            }

            viagemExistente.Destino = request.Destino;
            viagemExistente.ValorPassagem = request.ValorPassagem;

            _repository.AtualizarViagem(viagemExistente);
            await PersistirDados(_repository.UnitOfWork);

            return viagemExistente;
        }

        public async Task<bool> DeletarViagem(int id)
        {
            var viagemExistente = await _repository.ObterViagem(x => x.Id == id);
            if (viagemExistente == null)
            {
                throw new Exception("Viagem não existente");
            }

            var resultado = _repository.DeletarViagem(viagemExistente);
            await PersistirDados(_repository.UnitOfWork);

            return resultado;
        }

        public async Task<string> ObterRotaCustoBaixo(ViagemObterRotaCustoBaixoModelRequest request)
        {
            var rotas = await _repository.ObterTodos();
            if (rotas == null)
            {
                throw new ArgumentException("O destino/origem informados não existem");
            }

            var localizacoes = ObterLocalizoesDistintas(rotas);

            var listaGrafos = MontarGrafos(localizacoes, rotas);

            var origem = listaGrafos.FirstOrDefault(x => x.Nome == request.Origem);
            var destino = listaGrafos.FirstOrDefault(x => x.Nome == request.Destino);

            var caminhoMaisCurto = EncontrarCaminhoMaisCurto(origem, destino);

            var retorno = string.Join(" -> ", caminhoMaisCurto.rota.Select(x => x.Nome)) + $" ao custo de {caminhoMaisCurto.valorFinal}";
            
            return retorno;
        }

        private HashSet<string> ObterLocalizoesDistintas(List<Viagem> rotas)
        {
            var retorno = new HashSet<string>();

            foreach (var item in rotas)
            {
                retorno.Add(item.Origem);
                retorno.Add(item.Destino);
            }

            return retorno;
        }

        private List<Node> MontarGrafos(HashSet<string> localizoes, List<Viagem> rotas)
        {
            var listaNodes = new List<Node>();
            //Cria os Nós com base no Hash
            foreach (var item in localizoes)
            {
                var node = new Node(item);
                listaNodes.Add(node);
            }

            foreach (var node in listaNodes)
            {
                //Obtem as conexões com base na origem
                var conexoes = rotas.Where(x => x.Origem == node.Nome).Select(x => (x.Destino, x.ValorPassagem));
                foreach (var conexao in conexoes)
                {
                    //Obtem o node correto com base no destino
                    var nodeDestino = listaNodes.FirstOrDefault(x => x.Nome == conexao.Destino);
                    node.ConnectTo(nodeDestino, conexao.ValorPassagem);
                }    
            }

            return listaNodes;
        }

        private (decimal valorFinal, List<Node> rota) EncontrarCaminhoMaisCurto(Node from, Node to)
        {
            var controle = new DadoVisitado();

            controle.UpdatePeso(from, new Peso(null, 0));
            controle.ScheduleVisitTo(from);

            while (controle.HasScheduledVisits)
            {
                var visitingNode = controle.GetNodeToVisit();
                var visitingNodeWeight = controle.QueryPeso(visitingNode);
                controle.RegisterVisitTo(visitingNode);

                foreach (var neighborhoodInfo in visitingNode.Neighbors)
                {
                    if (!controle.WasVisited(neighborhoodInfo.Node))
                    {
                        controle.ScheduleVisitTo(neighborhoodInfo.Node);
                    }

                    var neighborWeight = controle.QueryPeso(neighborhoodInfo.Node);

                    var probableWeight = (visitingNodeWeight.Valor + neighborhoodInfo.Valor);
                    if (neighborWeight.Valor > probableWeight)
                    {
                        controle.UpdatePeso(neighborhoodInfo.Node, new Peso(visitingNode, probableWeight));
                    }
                }
            }

            return controle.HasComputedPathToOrigin(to)
                ? (controle.QueryPeso(to).Valor, controle.ComputedPathToOrigin(to).Reverse().ToList())
                : (0, new List<Node>());
        }
    }
}
