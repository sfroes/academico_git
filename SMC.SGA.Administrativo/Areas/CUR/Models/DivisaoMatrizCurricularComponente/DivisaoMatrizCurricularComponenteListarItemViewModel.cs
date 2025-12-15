using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class DivisaoMatrizCurricularComponenteListarItemViewModel : SMCViewModelBase
    {
        /// <summary>
        /// Sequencial do grupo curricular componente
        /// </summary>
        public long Seq { get; set; }

        public long SeqComponenteCurricular { get; set; }

        public long SeqGrupoCurricularComponente { get; set; }

        [SMCOrder(0)]
        [SMCSize(SMCSize.Grid8_24)]
        public string DescricaoComponente { get; set; }

        [SMCOrder(1)]
        [SMCSize(SMCSize.Grid8_24)]
        public string DescricaoConfiguracaoComponente { get; set; }

        [SMCOrder(2)]
        [SMCSize(SMCSize.Grid6_24)]
        public string DescricaoDivisao { get; set; }

        [SMCIgnoreProp]
        public bool ExigeAssuntoComponente { get; set; }

    }
}