using SMC.Academico.Common.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.TUR.ValueObjects
{
    public class TurmaCabecalhoConfiguracoesVO : ISMCMappable
    {
        public long SeqConfiguracaoComponente { get; set; }

        public long SeqComponenteCurricular { get; set; }

        public string DescricaoConfiguracaoComponente { get; set; }

        public LegendaPrincipal ConfiguracaoPrincipal { get; set; }

        public string DescricaoConfiguracaoComponenteOrdem { get; set; }
               
    }
}
