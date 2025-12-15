using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class ConfiguracaoEtapaBloqueioData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqConfiguracaoEtapa { get; set; }

        public string DescricaoConfiguracaoEtapa { get; set; }

        public long SeqProcessoEtapa { get; set; }

        public string DescricaoEtapaSgf { get; set; }

        public long SeqMotivoBloqueio { get; set; }

        public string DescricaoMotivo { get; set; }

        public bool? ImpedeInicioEtapa { get; set; }

        public bool? ImpedeFimEtapa { get; set; }

        public AmbitoBloqueio AmbitoBloqueio { get; set; }

        public bool? GeraCancelamentoSolicitacao { get; set; }

        public bool SolicitacaoAssociada { get; set; }

        public bool CamposReadyOnly { get; set; }
    }
}
