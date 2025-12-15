using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using System.Linq;

namespace SMC.Academico.Domain.Areas.PES.DomainServices
{
    public class PessoaAtuacaoAmostraPpaDomainService : AcademicoContextDomain<PessoaAtuacaoAmostraPpa>
    {

        /// <summary>
        /// Busca os dados da amostra PPA que ainda não foi preenchida para apresentar banner
        /// 
        /// O banner deverá estar visivel quando possuir um registro na tabela de apostra para:
        /// - pessoa atuação informada como parâmetro
        /// - a configuração da avaliação PPA seja do tipo informado por parâmetro;
        /// - a data de preenchimento seja nula;
        /// - a data limite de resposta da configuração de avaliação PPA seja nula OU posterior a data do dia;
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação a ser pesquisada</param>
        /// <param name="tipoAvaliacao">Tipo de avaliação a ser pesquisada</param>
        /// <returns>Dados da amostra PPA encontrada ou NULL</returns>
        public int? BuscarPessoaAtuacaoAmostraPpaNaoPreenchida(long seqPessoaAtuacao, TipoAvaliacaoPpa tipoAvaliacao)
        {
            // Busca a amostra conforme os filtros
            var spec = new PessoaAtuacaoAmostraPpaFilterSpecification()
            {
                SeqPessoaAtuacao = seqPessoaAtuacao,
                TipoAvaliacaoPpa = tipoAvaliacao,
                NaoPreenchido = true,
                DataLimiteRespostasFutura = true,
                DataInicioVigenciaPassada = true
            };
            var amostra = this.SearchByKey(spec);

            // Se encontrou registro de amostra, retorna o código da amostra no PPA
            if (amostra != null)
                return amostra.CodigoAmostraPpa;
            else // senão retorna NULL
                return null;
        }

        public SMCPagerData<PessoaAtuacaoAmostraPpaListaVO> ListarAmostras(PessoaAtuacaoAmostraPpaFiltroVO filtro)
        {
            var spec = filtro.Transform<PessoaAtuacaoAmostraPpaFilterSpecification>();
            spec.SetOrderBy(x => x.PessoaAtuacao.DadosPessoais.Nome);

            int total = 0;
            var lista = this.SearchProjectionBySpecification(spec, x => new PessoaAtuacaoAmostraPpaListaVO
            {
                Seq = x.Seq,
                SeqPessoaAtuacao = x.SeqPessoaAtuacao,
                Nome = x.PessoaAtuacao.DadosPessoais.Nome,
                CodigoAmostraPpa = x.CodigoAmostraPpa,
                DataPreenchimento = x.DataPreenchimento
            },
            out total)
            .ToList();

            return new SMCPagerData<PessoaAtuacaoAmostraPpaListaVO>(lista, total);
        }
    }
}