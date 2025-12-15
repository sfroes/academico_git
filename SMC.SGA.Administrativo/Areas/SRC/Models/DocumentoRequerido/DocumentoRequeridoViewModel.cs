using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.SRC.Controllers;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class DocumentoRequeridoViewModel : SMCViewModelBase
    {
        #region Datasource    

        public List<SMCDatasourceItem> TiposDocumento { get; set; }

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
        public bool BloqueiaCampoPrazoEntrega { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(TiposDocumento))]
        [SMCConditionalReadonly(nameof(Seq), SMCConditionalOperation.NotEqual, 0, PersistentValue = true)]
        [SMCSize(SMCSize.Grid21_24, SMCSize.Grid24_24, SMCSize.Grid21_24, SMCSize.Grid21_24)]
        public long SeqTipoDocumento { get; set; }

        [SMCRequired]
        [SMCSelect]
        [SMCConditionalReadonly(nameof(CamposReadyOnly), true, PersistentValue = true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public bool? Obrigatorio { get; set; }

        [SMCRequired]
        [SMCSelect]
        [SMCConditionalReadonly(nameof(CamposReadyOnly), true, PersistentValue = true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public bool? PermiteUploadArquivo { get; set; }

        [SMCSelect]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        [SMCConditionalRequired(nameof(PermiteUploadArquivo), SMCConditionalOperation.NotEqual, false, RuleName = "OBG1")]
        [SMCConditionalRequired(nameof(Obrigatorio), SMCConditionalOperation.NotEqual, false, RuleName = "OBG2")]
        [SMCConditionalReadonly(nameof(CamposReadyOnly), true, RuleName = "R1", PersistentValue = true)]
        [SMCConditionalReadonly(nameof(PermiteUploadArquivo), SMCConditionalOperation.NotEqual, true, RuleName = "R2", PersistentValue = false)]
        [SMCConditionalReadonly(nameof(Obrigatorio), SMCConditionalOperation.NotEqual, true, RuleName = "R3", PersistentValue = true)]
        [SMCDependency(nameof(PermiteUploadArquivo), nameof(DocumentoRequeridoController.PreencherCampoUploadObrigatorio), "DocumentoRequerido", false, new string[] { nameof(Obrigatorio) })]
        [SMCDependency(nameof(Obrigatorio), nameof(DocumentoRequeridoController.PreencherCampoUploadObrigatorio), "DocumentoRequerido", false, new string[] { nameof(PermiteUploadArquivo) })]
        [SMCConditionalRule("R1 || ( R2 || R3 )")]
        [SMCConditionalRule("OBG1 && OBG2")]
        public bool? ObrigatorioUpload { get; set; }

        [SMCRequired]
        [SMCSelect]
        [SMCConditionalReadonly(nameof(CamposReadyOnly), true, PersistentValue = true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public VersaoDocumento VersaoDocumento { get; set; }

        [SMCSelect]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        [SMCConditionalReadonly(nameof(CamposReadyOnly), true, PersistentValue = true)]
        public Sexo? Sexo { get; set; }

        [SMCRequired]
        [SMCSelect]
        [SMCConditionalReadonly(nameof(CamposReadyOnly), true, PersistentValue = true)]
        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid9_24, SMCSize.Grid9_24)]
        public bool? PermiteEntregaPosterior { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.Date)]
        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid9_24, SMCSize.Grid9_24)]
        [SMCConditionalReadonly(nameof(CamposReadyOnly), true, RuleName = "R1", PersistentValue = true)]        
        [SMCConditionalReadonly(nameof(BloqueiaCampoPrazoEntrega), true, RuleName = "R4", PersistentValue = true)]
        [SMCConditionalReadonly(nameof(PermiteEntregaPosterior), SMCConditionalOperation.NotEqual, true, RuleName ="R5", PersistentValue = true)]
        [SMCDependency(nameof(PermiteEntregaPosterior), nameof(DocumentoRequeridoController.LimparCampoDataLimiteEntrega), "DocumentoRequerido", false, new string[] { nameof(DataLimiteEntrega) })]
        [SMCConditionalRule("R1 || ( R4 || R5 )")]
        public DateTime? DataLimiteEntrega { get; set; }

        [SMCRequired]
        [SMCSelect]
        [SMCConditionalReadonly(nameof(CamposReadyOnly), true, PersistentValue = true)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        public bool? ValidacaoOutroSetor { get; set; }

        [SMCRequired]
        [SMCSelect]
        [SMCConditionalReadonly(nameof(CamposReadyOnly), true, PersistentValue = true)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid11_24, SMCSize.Grid12_24)]
        public bool? PermiteVarios { get; set; }

        [SMCHidden]
        public bool CamposReadyOnly { get; set; }
    }
}