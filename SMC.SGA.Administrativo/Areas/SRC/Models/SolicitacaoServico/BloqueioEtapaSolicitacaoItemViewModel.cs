using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.UI.Mvc;
using System;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class BloqueioEtapaSolicitacaoItemViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }

        public string TipoBloqueio { get; set; }

        public string MotivoBloqueio { get; set; }

        public string Referente { get; set; }

        public string Bloqueio { get; set; }

        public SituacaoBloqueio SituacaoBloqueio { get; set; }

        public TipoDesbloqueio? TipoDesbloqueio { get; set; }

        public FormaBloqueio FormaDesbloqueioMotivo { get; set; }

        public DateTime DataBloqueio { get; set; }

        public string TokenPermissaoDesbloqueio { get; set; }

        public bool HabilitaBotaoDesbloqueio { get; set; }

        public bool BloqueioImpedeInicioEtapa { get; set; }

        public bool BloqueioImpedeFimEtapa { get; set; }

        public string BloqueioImpede { 
            get 
            { 
                if(BloqueioImpedeInicioEtapa && BloqueioImpedeFimEtapa)
                {
                    return "Início e fim da etapa";
                }
                else if (BloqueioImpedeInicioEtapa)
                {
                    return "Início da etapa";
                }
                else if (BloqueioImpedeFimEtapa)
                {
                    return "Fim da etapa";
                }
                else
                {
                    return "";
                }
            } 
        }
    }
}