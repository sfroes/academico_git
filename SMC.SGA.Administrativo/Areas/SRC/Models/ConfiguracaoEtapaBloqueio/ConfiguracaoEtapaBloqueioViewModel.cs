using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.SRC.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ConfiguracaoEtapaBloqueioViewModel : SMCViewModelBase
    {
        #region Datasource    

        public List<SMCDatasourceItem> MotivosBloqueio { get; set; }

        #endregion

        [SMCReadOnly]
        [SMCKey]
        [SMCRequired]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid3_24, SMCSize.Grid3_24)]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqConfiguracaoEtapa { get; set; }

        [SMCHidden]
        public string DescricaoConfiguracaoEtapa { get; set; }

        [SMCHidden]
        public long SeqProcessoEtapa { get; set; }

        [SMCHidden]
        public string DescricaoEtapaSgf { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(MotivosBloqueio), NameDescriptionField = nameof(DescricaoMotivo))]
        [SMCSize(SMCSize.Grid21_24, SMCSize.Grid24_24, SMCSize.Grid21_24, SMCSize.Grid21_24)]
        [SMCConditionalReadonly(nameof(Seq), SMCConditionalOperation.NotEqual, 0, PersistentValue = true)]
        public long SeqMotivoBloqueio { get; set; }        
       
        public string DescricaoMotivo { get; set; }

        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        [SMCConditionalReadonly(nameof(GeraCancelamentoSolicitacao), true, RuleName = "R1", PersistentValue = false)]
        [SMCConditionalReadonly(nameof(CamposReadyOnly), true, RuleName = "R2", PersistentValue = true)]     
        [SMCConditionalRule("R1 || R2")]
        [SMCDependency(nameof(GeraCancelamentoSolicitacao), nameof(ConfiguracaoEtapaBloqueioController.PreencherCampoImpedeInicioEtapa), "ConfiguracaoEtapaBloqueio", false)]        
        public bool? ImpedeInicioEtapa { get; set; }

        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        [SMCConditionalReadonly(nameof(GeraCancelamentoSolicitacao), true, RuleName = "R1", PersistentValue = false)]
        [SMCConditionalReadonly(nameof(CamposReadyOnly), true, RuleName = "R2", PersistentValue = true)]
        [SMCConditionalRule("R1 || R2")]
        [SMCDependency(nameof(GeraCancelamentoSolicitacao), nameof(ConfiguracaoEtapaBloqueioController.PreencherCampoImpedeFimEtapa), "ConfiguracaoEtapaBloqueio", false)]
        public bool? ImpedeFimEtapa { get; set; }

        [SMCSelect]
        [SMCRequired]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        [SMCConditionalReadonly(nameof(CamposReadyOnly), true, PersistentValue = true)]
        public AmbitoBloqueio AmbitoBloqueio { get; set; }

        [SMCSelect]
        [SMCRequired]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        [SMCConditionalReadonly(nameof(CamposReadyOnly), true, PersistentValue = true)]
        public bool? GeraCancelamentoSolicitacao { get; set; }

        [SMCHidden]
        public bool SolicitacaoAssociada { get; set; }

        [SMCHidden]
        public bool CamposReadyOnly { get; set; }
    }
}