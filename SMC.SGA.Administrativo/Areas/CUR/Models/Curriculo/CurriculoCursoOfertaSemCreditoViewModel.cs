using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    /// <summary>
    /// View model criada para ocultar as quantidades de crédito obrigatório para níveis de ensino sem crédito
    /// </summary>
    public class CurriculoCursoOfertaSemCreditoViewModel : CurriculoCursoOfertaViewModel
    {
        [SMCHidden]
        public short? QuantidadeCreditoObrigatorio { get; set; }

        [SMCHidden]
        public short? QuantidadeCreditoOptativo { get; set; }
    }
}