using SMC.Academico.Domain.Areas.ORG.ValueObjects;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class SolicitacaoAnaliseEmissaoDocumentoConclusaoAtosCursoVO : ISMCMappable
    {
        public long SeqCursoOfertaLocalidade { get; set; }

        public long? CodigoCursoOfertaLocalidade { get; set; }

        public long? CodigoOrgaoRegulador { get; set; }

        public DateTime? DataConclusao { get; set; }

        public string DescricaoGrauAcademico { get; set; }

        public string AtoAutorizacaoCurso { get; set; }

        public string AtoReconhecimentoCurso { get; set; }

        public string AtoRenovacaoReconhecimentoCurso { get; set; }

        public DadosAtoNormativoVO Autorizacao { get; set; }

        public DadosAtoNormativoVO Reconhecimento { get; set; }

        public DadosAtoNormativoVO RenovacaoReconhecimento { get; set; }
    }
}
