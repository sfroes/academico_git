using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Academico.Domain.Areas.CNC.Specifications;
using SMC.Academico.Domain.Areas.CNC.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CNC.DomainServices
{
    public class ApuracaoFormacaoDomainService : AcademicoContextDomain<ApuracaoFormacao>
    {
        public List<ApuracaoFormacaoVO> BuscarApuracoesFormacaoPorAlunoFormacao(long seqAlunoFormacao)
        {
            var apuracoesFormacao = this.SearchProjectionBySpecification(new ApuracaoFormacaoFilterSpecification() { SeqAlunoFormacao = seqAlunoFormacao }, x => new ApuracaoFormacaoVO()
            {
                Seq = x.Seq,
                SeqAlunoFormacao = x.SeqAlunoFormacao,
                SituacaoAlunoFormacao = x.SituacaoAlunoFormacao,
                DataInicio = x.DataInicio,
                DataInclusao = x.DataInclusao

            }).ToList();

            return apuracoesFormacao;
        }
    }
}