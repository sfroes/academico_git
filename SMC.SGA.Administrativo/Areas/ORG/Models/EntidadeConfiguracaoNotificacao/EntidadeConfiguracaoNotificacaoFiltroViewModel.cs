using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.ORG.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class EntidadeConfiguracaoNotificacaoFiltroViewModel : SMCPagerViewModel, ISMCMappable
    {
        #region Datasource

        public List<SMCDatasourceItem> TiposNotificacao { get; set; }

        public List<SMCDatasourceItem> Entidades { get; set; }

        #endregion

        [SMCSelect(nameof(Entidades), SortDirection = SMCSortDirection.Ascending)]
        [SMCSize(SMCSize.Grid12_24)]
        public long? SeqEntidade { get; set; }

        [SMCSelect(nameof(TiposNotificacao), SortDirection = SMCSortDirection.Ascending)]
        [SMCDependency(nameof(SeqEntidade), nameof(EntidadeConfiguracaoNotificacaoController.BuscarTipoNotificacao), "EntidadeConfiguracaoNotificacao", false)]
        [SMCSize(SMCSize.Grid12_24)]
        public long? SeqTipoNotificacao { get; set; }
    }
}