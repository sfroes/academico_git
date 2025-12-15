using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.SRC.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ConfiguracaoEtapaViewModel : SMCViewModelBase
    {
        #region Datasource    

        public List<SMCDatasourceItem> ConfiguracoesProcesso { get; set; }

        #endregion
                
        [SMCKey]
        [SMCRequired]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid3_24, SMCSize.Grid3_24)]
        public long Seq { get; set; }

        [SMCDescription]
        [SMCRequired]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24)]
        [SMCConditionalReadonly(nameof(Seq), SMCConditionalOperation.NotEqual, 0, PersistentValue = true)]
        [SMCDependency(nameof(SeqConfiguracaoProcesso), nameof(ConfiguracaoEtapaController.ManipularDescricao), "ConfiguracaoEtapa", true, new string[] { nameof(SeqProcessoEtapa) })]
        public string Descricao { get; set; }

        [SMCHidden]
        public long SeqProcesso { get; set; }

        [SMCHidden]
        public long SeqProcessoEtapa { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(ConfiguracoesProcesso))]
        [SMCSize(SMCSize.Grid21_24, SMCSize.Grid24_24, SMCSize.Grid21_24, SMCSize.Grid21_24)]        
        [SMCConditionalReadonly(nameof(Seq), SMCConditionalOperation.NotEqual, 0, PersistentValue = true)]
        public long SeqConfiguracaoProcesso { get; set; }

        [SMCMultiline(Rows = 3)]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24)]
        [SMCConditionalReadonly(nameof(CampoOrientacaoReadOnly), requiredValue: false, PersistentValue = true)]
        public string OrientacaoEtapa { get; set; }

        [SMCMultiline(Rows = 3)]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24)]
        [SMCConditionalReadonly(nameof(CamposReadyOnly), requiredValue: true, PersistentValue = true)]
        public string DescricaoTermoEntregaDocumentacao { get; set; }

        [SMCHidden]
        public bool CamposReadyOnly { get; set; }

        [SMCHidden]
        public bool SolicitacaoAssociada { get; set; }

        [SMCHidden]
        public bool CampoOrientacaoReadOnly { get; set; }
    }
}