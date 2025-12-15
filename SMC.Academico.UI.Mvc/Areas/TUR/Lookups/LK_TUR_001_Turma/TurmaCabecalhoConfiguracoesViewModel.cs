using SMC.Academico.Common.Enums;
using SMC.Framework.UI.Mvc;

namespace SMC.Academico.UI.Mvc.Areas.TUR.Lookups
{
    public class TurmaCabecalhoConfiguracoesViewModel : SMCViewModelBase
    {

        public long SeqConfiguracaoComponente { get; set; }

        public long SeqComponenteCurricular { get; set; }

        public string DescricaoConfiguracaoComponente { get; set; }

        public LegendaPrincipal ConfiguracaoPrincipal { get; set; }
    }
}
