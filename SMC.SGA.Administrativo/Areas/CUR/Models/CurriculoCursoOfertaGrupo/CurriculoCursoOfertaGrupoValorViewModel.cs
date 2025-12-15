using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class CurriculoCursoOfertaGrupoValorViewModel : SMCViewModelBase
    {
        /// <summary>
        /// Prefixo que será aplicado aos nomes dos campos para que o binder possa preencher
        /// a propriedade correta do dynamic principal
        /// </summary>
        [SMCIgnoreProp]
        public string Prefixo { get; set; }

        [SMCHidden]
        public int? QuantidadeCreditoObrigatorio { get; set; }

        [SMCHidden]
        public int? QuantidadeCreditoOptativo { get; set; }

        [SMCHidden]
        public int? QuantidadeHoraAulaObrigatoria { get; set; }

        [SMCHidden]
        public int? QuantidadeHoraAulaOptativa { get; set; }

        [SMCHidden]
        public int? QuantidadeHoraRelogioObrigatoria { get; set; }

        [SMCHidden]
        public int? QuantidadeHoraRelogioOptativa { get; set; }
    }
}