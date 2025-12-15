using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.CNC.Models
{
    public class IntegralizacaoMatrizCurricularOfertaFiltroViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long SeqPessoaAtuacao { get; set; }

        public string FiltroDescricaoConfiguracao { get; set; }

        [SMCSelect]
        public SituacaoComponenteIntegralizacao? FiltroSituacaoConfiguracao { get; set; }
    }
}
