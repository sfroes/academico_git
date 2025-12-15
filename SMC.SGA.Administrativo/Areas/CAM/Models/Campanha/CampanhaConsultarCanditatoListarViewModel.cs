using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.CAM.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.Util;
using System;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class CampanhaConsultarCandidatoListarViewModel : SMCViewModelBase
    {
        [SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2 smc-size-lg-2")]
        public long SeqInscricao { get; set; }

        [SMCSortable(true, true)]
        [SMCCssClass("smc-size-md-5 smc-size-xs-5 smc-size-sm-5 smc-size-lg-5")]
        public string Candidato { get; set; }

        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        public string ProcessoSeletivo { get; set; }

        [SMCSortable(true, false)]
        [SMCCssClass("smc-size-md-5 smc-size-xs-5 smc-size-sm-5 smc-size-lg-5")]
        public string Oferta { get; set; }

        [SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2 smc-size-lg-2")]
        public string Chamada
        {
            get
            {
                var retorno = string.Empty;

                if (NumeroChamada.HasValue)
                    retorno += $"{NumeroChamada}ª";
                if (TipoChamada.HasValue)
                {
                    if (string.IsNullOrEmpty(retorno))
                        retorno += $"{SMCEnumHelper.GetDescription(TipoChamada)}";
                    else
                        retorno += $" - {SMCEnumHelper.GetDescription(TipoChamada)}";
                }
                return retorno;
            }
        }

        [SMCHidden]
        public TipoChamada? TipoChamada { get; set; }

        [SMCHidden]
        public short? NumeroChamada { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        [SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2 smc-size-lg-2")]
        public DateTime? DataCadastroIngressante { get; set; }

        [SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2 smc-size-lg-2")]
        public SituacaoIngressante? SituacaoIngressante { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        [SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2 smc-size-lg-2")]
        public DateTime? DataSituacaoIngressante { get; set; }
    }
}