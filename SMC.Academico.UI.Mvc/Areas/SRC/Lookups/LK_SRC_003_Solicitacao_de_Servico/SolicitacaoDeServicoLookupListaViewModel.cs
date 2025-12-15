using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Globalization;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Lookups
{
    public class SolicitacaoDeServicoLookupListaViewModel : SMCViewModelBase, ISMCLookupData, ISMCLookupViewModel
    {
        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; }

        [SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2 smc-size-lg-2")]
        public string NumeroProtocolo { get; set; }

        [SMCSortable(false, true, "PessoaAtuacao.DadosPessoais.Nome")]
        [SMCCssClass("smc-size-md-6 smc-size-xs-6 smc-size-sm-6 smc-size-lg-6")]
        public string Solicitante { get; set; }

        [SMCSortable(false, true, "ConfiguracaoProcesso.Processo.Descricao")]
        [SMCCssClass("smc-size-md-6 smc-size-xs-6 smc-size-sm-6 smc-size-lg-6")]
        public string Processo { get; set; }

        [SMCCssClass("smc-size-md-6 smc-size-xs-6 smc-size-sm-6 smc-size-lg-6")]
        public string EtapaAtualCompleta { get; set; }

        [SMCCssClass("smc-size-md-6 smc-size-xs-6 smc-size-sm-6 smc-size-lg-6")]
        public string SituacaoAtual { get; set; }

        [SMCHidden]
        [SMCDateTimeMode(SMCDateTimeMode.Date)]
        public DateTime DataInclusao { get; set; }

        [SMCCssClass("smc-size-md-6 smc-size-xs-6 smc-size-sm-6 smc-size-lg-6")]
        public string DataInclusaoFormatada
        {
            get
            {
                CultureInfo cult = new CultureInfo("pt-BR");
                return DataInclusao.ToString("dd/MM/yy", cult);
            }
        }
    }
}
