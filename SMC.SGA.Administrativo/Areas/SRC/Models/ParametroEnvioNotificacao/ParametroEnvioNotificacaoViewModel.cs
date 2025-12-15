using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ParametroEnvioNotificacaoViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long SeqProcesso { get; set; }

        [SMCHidden]
        public long SeqServico { get; set; }

        [SMCHidden]
        public long SeqEtapaSgf { get; set; }

        [SMCHidden]
        public long SeqProcessoEtapaConfiguracaoNotificacao { get; set; }

        [SMCHidden]
        public bool ProcessoEncerrado { get; set; }

        [SMCHidden]
        public bool BotoesDesabilitados { get; set; }

        [SMCHidden]
        public bool ExigeEscalonamento { get; set; }

        [SMCHidden]
        public long SeqProcessoEtapa { get; set; }

        [SMCHidden]
        [SMCConditionalReadonly(nameof(BotoesDesabilitados), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        public long SeqTipoNotificacao { get; set; }

        [SMCConditionalReadonly(nameof(BotoesDesabilitados), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        public string TipoNotificacao { get; set; }

        [SMCConditionalReadonly(nameof(BotoesDesabilitados), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        public string EntidadeResponsavel { get; set; }        

        [SMCConditionalReadonly(nameof(BotoesDesabilitados), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCDataSource(SMCStorageType.Session)]
        public List<SMCDatasourceItem> AtributosDisponiveis { get; set; }

        [SMCConditionalReadonly(nameof(BotoesDesabilitados), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCDetail(SMCDetailType.Modal, windowSize: SMCModalWindowSize.Largest)]
        public SMCMasterDetailList<ParametroEnvioNotificacaoItemViewModel> ParametroEnvioNotificacaoItem { get; set; }

        [SMCConditionalReadonly(nameof(BotoesDesabilitados), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCDetail(SMCDetailType.Modal, windowSize: SMCModalWindowSize.Largest)]
        public SMCMasterDetailList<ParametroEnvioNotificacaoItemSemGrupoEscalonamentoViewModel> ParametroEnvioNotificacaoItemSemGrupoEscalonamento { get; set; }
    }
}