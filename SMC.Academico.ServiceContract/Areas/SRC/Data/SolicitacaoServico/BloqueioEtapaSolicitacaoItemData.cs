using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class BloqueioEtapaSolicitacaoItemData : ISMCMappable
    {
        public long Seq { get; set; }

        public string TipoBloqueio { get; set; }

        public string MotivoBloqueio { get; set; }

        public string Referente { get; set; }

        public string Bloqueio { get; set; }

        public SituacaoBloqueio SituacaoBloqueio { get; set; }

        public string DescricaoSituacaoBloqueio { get; set; }

        public TipoDesbloqueio? TipoDesbloqueio { get; set; }

        public FormaBloqueio FormaDesbloqueioMotivo { get; set; }

        public DateTime DataBloqueio { get; set; }

        public string DataBloqueioFormatada { get { return this.DataBloqueio.ToString("dd/MM/yy"); } }

        public string TokenPermissaoDesbloqueio { get; set; }

        public bool HabilitaBotaoDesbloqueio { get; set; }

        public bool BloqueioImpedeInicioEtapa { get; set; }

        public bool BloqueioImpedeFimEtapa { get; set; }
    }
}