using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Academico.UI.Mvc.Areas.PES.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.PES.Controllers;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class EnvioNotificacaoFiltroViewModel : SMCPagerViewModel, ISMCMappable
    {
        [SMCIgnoreProp]
        public long Seq { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        public string Assunto { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        public string Remetente { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        [SMCSelect(IgnoredEnumItems = new object[] { TipoAtuacao.Ingressante, TipoAtuacao.Funcionario })]
        public TipoAtuacao TipoAtuacao { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public DateTime? DataEnvio { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public string UsuarioEnvio { get; set; }

        [SMCIgnoreProp]
        public long? SeqNotificacaoEmail { get; set; }
    }
}