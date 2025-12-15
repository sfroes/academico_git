using SMC.Academico.Common.Enums;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class TurmaOrientacaoCabecalhoConfiguracoesViewModel : SMCViewModelBase
    {
        public long SeqConfiguracaoComponente { get; set; }

        public long SeqComponenteCurricular { get; set; }

        public string DescricaoConfiguracaoComponente { get; set; }
        
        public LegendaPrincipal ConfiguracaoPrincipal { get; set; }

        public string RaNome { get; set; }
    }
}