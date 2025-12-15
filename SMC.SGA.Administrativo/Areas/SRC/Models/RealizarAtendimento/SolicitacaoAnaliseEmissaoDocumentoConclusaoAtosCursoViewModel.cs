using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class SolicitacaoAnaliseEmissaoDocumentoConclusaoAtosCursoViewModel : SMCViewModelBase
    {
        [SMCSize(SMCSize.Grid12_24)]
        [SMCValueEmpty("-")]
        public long SeqCursoOfertaLocalidade { get; set; }

        [SMCSize(SMCSize.Grid12_24)]
        [SMCValueEmpty("-")]
        public long? CodigoCursoOfertaLocalidade { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        [SMCValueEmpty("-")]
        public long? CodigoOrgaoRegulador { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        [SMCValueEmpty("-")]
        public DateTime? DataConclusao { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        [SMCValueEmpty("-")]
        public string DescricaoGrauAcademico { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        [SMCValueEmpty("-")]
        public string AtoAutorizacaoCurso { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        [SMCValueEmpty("-")]
        public string AtoReconhecimentoCurso { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        [SMCValueEmpty("-")]
        public string AtoRenovacaoReconhecimentoCurso { get; set; }
    }
}