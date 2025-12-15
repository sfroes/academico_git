using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework.UI.Mvc;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.CNC.Models
{
    public class IntegralizacaoConsultaHistoricoViewModel : SMCViewModelBase
    {
        public IntegralizacaoMatrizCurricularOfertaCabecalhoViewModel DadosCabecalho { get; set; }

        public List<IntegralizacaoMatrizDivisaoViewModel> HistoricoEscolarComMatriz { get; set; }

        public string MensagemComponentesCursados { get; set; }

        public List<IntegralizacaoHistoricoSemMatrizViewModel> HistoricoEscolarSemMatriz { get; set; }

        public IntegralizacaoMatrizCurricularOfertaFiltroViewModel Filtros { get; set; }

        public List<SituacaoComponenteIntegralizacao> LegendaSituacoesComponenteIntegralizacao { get; set; }

        public List<string> LegendaTiposComponentesCurriculares { get; set; }
    }
}
