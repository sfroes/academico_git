using SMC.Academico.Common.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.TUR.Data
{
    public class TurmaCabecalhoConfiguracoesData : ISMCMappable
    {
        public long SeqConfiguracaoComponente { get; set; }

        public long SeqComponenteCurricular { get; set; }

        public string DescricaoConfiguracaoComponente { get; set; }

        public LegendaPrincipal ConfiguracaoPrincipal { get; set; }
    }
}
