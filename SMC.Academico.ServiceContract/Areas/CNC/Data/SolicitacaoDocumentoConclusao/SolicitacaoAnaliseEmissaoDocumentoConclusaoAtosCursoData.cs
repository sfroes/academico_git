using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data.SolicitacaoDocumentoConclusao
{
    public class SolicitacaoAnaliseEmissaoDocumentoConclusaoAtosCursoData : ISMCMappable
    {
        public long SeqCursoOfertaLocalidade { get; set; }

        public long? CodigoCursoOfertaLocalidade { get; set; }

        public long? CodigoOrgaoRegulador { get; set; }

        public DateTime? DataConclusao { get; set; }

        public string DescricaoGrauAcademico { get; set; }

        public string AtoAutorizacaoCurso { get; set; }

        public string AtoReconhecimentoCurso { get; set; }

        public string AtoRenovacaoReconhecimentoCurso { get; set; }
    }
}
