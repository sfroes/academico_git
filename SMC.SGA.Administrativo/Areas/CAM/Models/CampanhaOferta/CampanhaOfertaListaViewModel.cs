using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class CampanhaOfertaListaViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        public string TipoOferta { get; set; }

        [SMCCssClass("smc-size-md-14 smc-size-xs-14 smc-size-sm-14 smc-size-lg-14")]
        public string Oferta { get; set; }

        [SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2 smc-size-lg-2")]
        public int Vagas { get; set; }

        [SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2 smc-size-lg-2")]
        public int Ocupadas { get; set; }

        /// <summary>
        /// RN_CAM_069 - Usar vagas disponíveis na campanha
        /// VagasDisponiveis = (VagasBase - Ocupadas)
        /// Subtração das vagas da oferta com a quantidade de ingressantes
        /// cuja situação é diferente de "Desistente" e "Cancelado (Prouni)
        /// </summary>
        [SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2 smc-size-lg-2")]
        public int Disponiveis { get; set; }

        [SMCHidden]
        public bool PossuiVinculoProcessoSeletivo { get; set; }

        /// <summary>
        /// Vagas registradas na base de dados
        /// </summary>
        [SMCHidden]
        public int VagasBase { get; set; }

        /// <summary>
        /// Resultado da diferença das vagas inseridas com a quantidade de 
        /// vagas registrada na base
        /// </summary>
        [SMCHidden]
        public int VagasDiferenca { get; set; }

    }
}