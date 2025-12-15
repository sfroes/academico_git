using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class EntidadeConfiguracaoNotificacaoListarViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long? SeqEntidade { get; set; }

        [SMCSize(SMCSize.Grid12_24)]
        [SMCSortable(true, true)]
        public string DescricaoEntidade { get; set; }

        [SMCHidden]
        public long? SeqTipoNotificacao { get; set; }

        [SMCSize(SMCSize.Grid12_24)]
        [SMCSortable(true)]
        public string DescricaoTipoNotificacao { get; set; }

        [SMCSortable(true)]   
        public DateTime? DataInicioValidade { get; set; }

        [SMCSortable(true)]
        public DateTime? DataFimValidade { get; set; }
    }
}