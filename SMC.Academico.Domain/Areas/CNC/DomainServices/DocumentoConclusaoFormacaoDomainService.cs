using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Academico.Domain.Areas.CNC.Specifications;
using SMC.Academico.Domain.Areas.CNC.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CNC.DomainServices
{
    public class DocumentoConclusaoFormacaoDomainService : AcademicoContextDomain<DocumentoConclusaoFormacao>
    {
        public List<DocumentoConclusaoFormacaoListarVO> BuscarDocumentosConclusaoFormacaoPorDocumentoConclusao(long seqDocumentoConclusao)
        {
            var documentosConclusaoFormacao = this.SearchProjectionBySpecification(new DocumentoConclusaoFormacaoFilterSpecification() { SeqDocumentoConclusao = seqDocumentoConclusao }, x => new DocumentoConclusaoFormacaoListarVO()
            {
                Seq = x.Seq,
                SeqDocumentoConclusao = x.SeqDocumentoConclusao,
                SeqAlunoFormacao = x.SeqAlunoFormacao,
                SeqDocumentoConclusaoApostilamento = x.SeqDocumentoConclusaoApostilamento

            }).ToList();

            return documentosConclusaoFormacao;
        }
    }
}
