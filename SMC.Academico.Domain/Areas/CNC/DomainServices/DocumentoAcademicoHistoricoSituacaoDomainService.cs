using SMC.Academico.Common.Areas.CNC.Constants;
using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Academico.Domain.Areas.CNC.Specifications;

namespace SMC.Academico.Domain.Areas.CNC.DomainServices
{
    public class DocumentoAcademicoHistoricoSituacaoDomainService : AcademicoContextDomain<DocumentoAcademicoHistoricoSituacao>
    {
        private SituacaoDocumentoAcademicoDomainService SituacaoDocumentoAcademicoDomainService => Create<SituacaoDocumentoAcademicoDomainService>();

        public void InserirHistoricoAguardandoAssinatura(long seqDocumentoConclusao)
        {
            var situacaoAguardandoAssinaturas = SituacaoDocumentoAcademicoDomainService.SearchByKey(new SituacaoDocumentoAcademicoFilterSpecification { Token = TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.AGUARDANDO_ASSINATURAS });

            if (situacaoAguardandoAssinaturas != null)
            {
                var situacaoAtual = new DocumentoAcademicoHistoricoSituacao
                {
                    SeqSituacaoDocumentoAcademico = situacaoAguardandoAssinaturas.Seq,
                    SeqDocumentoAcademico = seqDocumentoConclusao
                };
                this.SaveEntity(situacaoAtual);
            }
        }
    }
}
