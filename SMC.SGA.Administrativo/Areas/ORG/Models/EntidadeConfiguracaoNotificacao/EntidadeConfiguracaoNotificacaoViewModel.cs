using System;
using System.Collections.Generic;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Notificacoes.UI.Mvc.Models;
using SMC.SGA.Administrativo.Areas.ORG.Controllers;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class EntidadeConfiguracaoNotificacaoViewModel : SMCWizardViewModel, ISMCStatefulView
    {
        #region Datasource

        public List<SMCDatasourceItem> TiposNotificacao { get; set; }

        public List<SMCDatasourceItem> Entidades { get; set; }

        public List<SMCDatasourceItem> LayoutEmail { get; set; }

        [SMCSelect("LayoutEmail")]
        [SMCSize(SMCSize.Grid24_24)]
        public long? SeqLayoutMensagemEmail { get; set; }

        #endregion

        [SMCKey]
        public long Seq { get; set; }

        public long SeqConfiguracaoTipoNotificacao { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(Entidades), SortDirection = SMCSortDirection.Ascending, NameDescriptionField = nameof(DescricaoEntidade))]
        [SMCConditionalReadonly(nameof(ExisteRegistroEnvioNotificacaoConfiguracao), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        public long SeqEntidade { get; set; }

        public string DescricaoEntidade { get; set; }

        [SMCRequired]   
        [SMCSelect(nameof(TiposNotificacao), SortDirection = SMCSortDirection.Ascending, NameDescriptionField = nameof(DescricaoTipoNotificacao))]
        [SMCDependency(nameof(SeqEntidade), nameof(EntidadeConfiguracaoNotificacaoController.BuscarTipoNotificacao), "EntidadeConfiguracaoNotificacao", true)]
        [SMCConditionalReadonly(nameof(ExisteRegistroEnvioNotificacaoConfiguracao), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        public long SeqTipoNotificacao { get; set; }

        public string DescricaoTipoNotificacao { get; set; }

        [SMCRequired]
        [SMCDateTimeMode(SMCDateTimeMode.Date)]   
        [SMCMinDateNow]
        [SMCConditionalReadonly(nameof(ExisteRegistroEnvioNotificacaoConfiguracao), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        public DateTime DataInicioValidade { get; set; }
       
        [SMCDateTimeMode(SMCDateTimeMode.Date)]       
        [SMCMinDate(nameof(DataInicioValidade))]        
        public DateTime? DataFimValidade { get; set; }

        public string TokenTipoNotificacao { get; set; }

        /// <summary>
        /// Sequencial de comparação caso exista alteração no Sequencial de Tipo Notificação
        /// </summary>        
        public long SeqTipoNotificacaoComparacao { get; set; }
                             
        public ConfiguracaoNotificacaoEmailViewModel ConfiguracaoNotificacao { get; set; }

        public string Observacao { get; set; }

        public bool ExisteRegistroEnvioNotificacaoConfiguracao { get; set; }

        
    }
}