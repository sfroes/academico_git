using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using System;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class CampanhaCopiaProcessoSeletivoListaViewModel : SMCViewModelBase, ISMCStatefulView
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long? SeqProcessoGpi { get; set; }

        public string Descricao { get; set; }

        public string TipoProcessoSeletivo { get; set; }

        public bool? CopiarProcessoGPI { get; set; }

        [SMCConditionalRequired(nameof(CopiarProcessoGPI), true)]
        public string DescricaoProcessoGPI { get; set; }

        [SMCSelect("CiclosLetivos", AutoSelectSingleItem = true)]
        [SMCConditionalRequired(nameof(CopiarProcessoGPI), true)]
        public long? SeqCicloLetivoReferenciaProcessoGPI { get; set; }

        [SMCConditionalRequired(nameof(CopiarProcessoGPI), true)]
        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        public DateTime? DataInicioInscricaoProcessoGPI { get; set; }

        [SMCConditionalRequired(nameof(CopiarProcessoGPI), true)]
        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        [SMCMinDate(nameof(DataInicioInscricaoProcessoGPI))]
        public DateTime? DataFimInscricaoProcessoGPI { get; set; }

        [SMCConditionalRequired(nameof(CopiarProcessoGPI), true)]
        [SMCRadioButtonList]
        public bool? CopiarNotificacoesGPI { get; set; }

        [SMCDetail(HideMasterDetailButtons = true)]
        public SMCMasterDetailList<CampanhaCopiaEtapaProcessoGPIItemViewModel> EtapasGPI { get; set; }

        [SMCDetail(HideMasterDetailButtons = true)]
        public SMCMasterDetailList<CampanhaCopiaConvocacaoProcessoSeletivoItemViewModel> Convocacoes { get; set; }
    }
}