using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ALN.DomainServices
{
    public class IngressanteFormacaoEspecificaDomainService : AcademicoContextDomain<IngressanteFormacaoEspecifica>
    {
        /// <summary>
        /// Busca as formações específicas do ingressante
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <returns>Dados das assoociações de formação específica do aluno</returns>
        public List<AlunoFormacaoVO> BuscarSequenciaisFormacoesIngressante(long seqPessoaAtuacao)
        {
            var spec = new IngressanteFormacaoEspecificaFilterSpecification() { SeqIngressante = seqPessoaAtuacao };

            var seqsFormacoes = SearchProjectionBySpecification(spec, p => new AlunoFormacaoVO()
            {
                SeqFormacaoEspecifica = p.SeqFormacaoEspecifica,
                TokenTipoFormacaoEspecifica = p.FormacaoEspecifica.TipoFormacaoEspecifica.Token
            }).ToList();

            return seqsFormacoes;
        }
    }
}
