using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.Util;
using SMC.SGA.Administrativo.Areas.FIN.Controllers;
using SMC.SGA.Administrativo.Areas.FIN.Views.PessoaAtuacaoBeneficio.App_LocalResources;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.FIN.Models
{
    public class PessoaAtuacaoBeneficioBeneficioEnvioNotificacaoViewModel : SMCViewModelBase
    {

        public long Seq { get; set; }

        public string DescricaoTipoNotificacao { get; set; }

        public string Assunto { get; set; }

        public bool? SucessoEnvio { get; set; }

        public DateTime? DataProcessamento { get; set; }
    }
}