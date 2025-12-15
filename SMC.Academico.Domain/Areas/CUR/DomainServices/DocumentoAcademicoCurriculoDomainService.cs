using SMC.Academico.Common.Areas.CNC.Constants;
using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Common.Areas.CNC.Exceptions;
using SMC.Academico.Domain.Areas.CNC.DomainServices;
using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Academico.Domain.Areas.CNC.Specifications;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Framework.Extensions;
using SMC.Framework.UnitOfWork;

namespace SMC.Academico.Domain.Areas.CUR.DomainServices
{
    public class DocumentoAcademicoCurriculoDomainService : AcademicoContextDomain<DocumentoAcademicoCurriculo>
    {
        private SituacaoDocumentoAcademicoDomainService SituacaoDocumentoAcademicoDomainService => Create<SituacaoDocumentoAcademicoDomainService>();
        private DocumentoAcademicoHistoricoSituacaoDomainService DocumentoAcademicoHistoricoSituacaoDomainService => Create<DocumentoAcademicoHistoricoSituacaoDomainService>();

        public long SalvarDocumentoAcademicoCurriculo(DocumentoAcademicoCurriculoVO modelo)
        {
            var dominio = modelo.Transform<DocumentoAcademicoCurriculo>();
            this.SaveEntity(dominio);

            return dominio.Seq;
        }

        public void AtualizarDocumentoAcademicoCurriculoDigital(long seqDocumentoAcademicoGAD)
        {
            var spec = new DocumentoAcademicoCurriculoFilterSpecification
            {
                SeqDocumentoAcademicoGAD = seqDocumentoAcademicoGAD,
                ClasseSituacaoDocumento = ClasseSituacaoDocumento.EmissaoEmAndamento
            };

            var documentoAcademicoCurriculo = this.SearchProjectionByKey(spec, s => new
            {
                s.Seq,
                s.NumeroViaDocumento,
                s.TipoDocumentoAcademico.Token
            });

            if (documentoAcademicoCurriculo != null)
            {
                var specSituacaoDocumentoAcademico = new SituacaoDocumentoAcademicoFilterSpecification() { Token = TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.VALIDO };
                var seqSituacaoDocumentoAcademico = SituacaoDocumentoAcademicoDomainService.SearchProjectionByKey(specSituacaoDocumentoAcademico, s => s.Seq);

                if (seqSituacaoDocumentoAcademico == 0)
                    throw new TokenSituacaoDocumentoAcademicoNaoEncontradoException(TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.VALIDO);

                using (var transacao = SMCUnitOfWork.Begin())
                {
                    var documentoAcademicoHistoricoSituacao = new DocumentoAcademicoHistoricoSituacao()
                    {
                        SeqDocumentoAcademico = documentoAcademicoCurriculo.Seq,
                        SeqSituacaoDocumentoAcademico = seqSituacaoDocumentoAcademico
                    };

                    DocumentoAcademicoHistoricoSituacaoDomainService.SaveEntity(documentoAcademicoHistoricoSituacao);

                    transacao.Commit();
                }

            }
        }
    }
}
