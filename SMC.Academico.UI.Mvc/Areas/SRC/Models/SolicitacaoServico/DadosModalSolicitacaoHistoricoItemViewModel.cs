using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class DadosModalSolicitacaoHistoricoItemViewModel : SMCViewModelBase
    {
        [SMCSize(SMCSize.Grid1_24)]
        [SMCCssClass("smc-size-md-1 smc-size-xs-1 smc-size-sm-1 smc-size-lg-1")]
        public SituacaoFinalEtapa SituacaoFinalEtapa { get; set; }

        [SMCSize(SMCSize.Grid2_24)]
        [SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2 smc-size-lg-2")]
        public string Etapa { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        [SMCCssClass("smc-size-md-8 smc-size-xs-8 smc-size-sm-8 smc-size-lg-8")]
        public string Situacao { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        [SMCCssClass("smc-size-md-34 smc-size-xs-3 smc-size-sm-3 smc-size-lg-3")]
        public DateTime DataInicio { get; set; }

        //[SMCSize(SMCSize.Grid4_24)]
        //[SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        //[SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        //public DateTime? DataFim { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        public string UsuarioResponsavel { get; set; }

        [SMCSize(SMCSize.Grid3_24)]
        [SMCCssClass("smc-size-md-7 smc-size-xs-7 smc-size-sm-7 smc-size-lg-7")]
        public string Observacao { get; set; }
    }
}