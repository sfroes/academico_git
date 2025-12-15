using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Framework.UI.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.FIN.Models
{
    public class PessoaAtuacaoBeneficioConfiguracaoViewModel:SMCViewModelBase
    {
        public long Seq { get; set; }

        public long SeqInstituicaoNivelBeneficio { get; set; }

        public TipoDeducao TipoDeducao { get; set; }

        public FormaDeducao? FormaDeducao { get; set; }

        public decimal? ValorDeducao { get; set; }

        public DateTime DataInicioValidade { get; set; }

        public DateTime? DataFimValidade { get; set; }

        public bool FlagUltimaConfiguracao { get; set; }

        public bool AssociacaoPessoaBeneficio { get; set; }

        public DateTime? DataBanco { get; set; }

        public bool IncluirDesbloqueioTemporario { get; set; }

        public string JustificativaDesbloqueio { get; set; }
    }
}