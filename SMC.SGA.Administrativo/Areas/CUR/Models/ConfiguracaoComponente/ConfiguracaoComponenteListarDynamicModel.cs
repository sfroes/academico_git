using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class ConfiguracaoComponenteListarDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCHidden]
        public override long Seq { get; set; }

        [SMCHidden]
        public string Codigo { get; set; }

        [SMCDescription]
        [SMCHidden]
        public string Descricao { get; set; }

        [SMCHidden]
        public string DescricaoComplementar { get; set; }

        [SMCHidden]
        public short? ComponenteCurricularCargaHoraria { get; set; }

        [SMCHidden]
        public short? ComponenteCurricularCredito { get; set; }

        [SMCHidden]
        public FormatoCargaHoraria? FormatoCargaHoraria { get; set; }

        /// <summary>
        /// Descrição formatada segundo a regra RN_CUR_042
        /// </summary>
        [SMCOrder(1)]
        [SMCSize(SMCSize.Grid24_24)]
        public string DescricaoFormatada { get; set; }

        [SMCDetail(SMCDetailType.Tabular, min: 1)]
        [SMCMapForceFromTo]
        [SMCOrder(2)]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<ConfiguracaoComponenteDivisaoListarViewModel> DivisoesComponente { get; set; }
    }
}