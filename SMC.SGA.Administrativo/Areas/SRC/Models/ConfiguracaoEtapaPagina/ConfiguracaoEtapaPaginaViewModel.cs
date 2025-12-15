using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.SRC.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ConfiguracaoEtapaPaginaViewModel : SMCViewModelBase
    {
        #region Datasource

        public List<SMCDatasourceItem> PaginasSGF { get; set; }   
        
        public List<SMCDatasourceItem> Formularios { get; set; }    
        
        public List<SMCDatasourceItem> VisoesFormulario { get; set; }

        #endregion

        [SMCHidden]
        public long SeqEtapaSgf { get; set; }

        [SMCHidden]
        public bool ExibeFormulario { get; set; }

        [SMCHidden]
        public bool CamposReadyOnly { get; set; }

        [SMCReadOnly]
        [SMCKey]
        [SMCRequired]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid3_24, SMCSize.Grid3_24)]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqConfiguracaoEtapa { get; set; }
        
        [SMCReadOnly]
        [SMCRequired]
        [SMCSelect(nameof(PaginasSGF))]
        [SMCSize(SMCSize.Grid21_24, SMCSize.Grid24_24, SMCSize.Grid21_24, SMCSize.Grid21_24)]
        public long SeqPaginaEtapaSgf { get; set; }

        [SMCHidden]
        public string DescricaoPagina { get; set; }

        [SMCReadOnly]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        public string TokenPagina { get; set; }

        [SMCReadOnly]
        [SMCRequired]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid3_24, SMCSize.Grid3_24)]
        public short Ordem { get; set; }

        [SMCSelect]
        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid9_24, SMCSize.Grid9_24)]
        public ConfiguracaoDocumento? ConfiguracaoDocumento { get; set; }

        [SMCRequired]
        [SMCConditionalReadonly(nameof(CamposReadyOnly), true, PersistentValue = true)]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24)]
        public string TituloPagina { get; set; }

        [SMCHidden]
        public bool ExibeMenu { get; set; }

        [SMCSelect(nameof(Formularios))]
        [SMCConditionalReadonly(nameof(CamposReadyOnly), true, PersistentValue = true)]
        [SMCConditionalDisplay(nameof(ExibeFormulario), SMCConditionalOperation.Equals, true)]
        [SMCConditionalRequired(nameof(ExibeFormulario), SMCConditionalOperation.Equals, true)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        public long? SeqFormulario { get; set; }

        [SMCSelect(nameof(VisoesFormulario), NameDescriptionField = nameof(DescricaoVisaoFormulario))]
        [SMCDependency(nameof(SeqFormulario), nameof(ConfiguracaoEtapaPaginaController.BuscarVisoesPorFormularioSelect), "ConfiguracaoEtapaPagina", true)]
        [SMCConditionalReadonly(nameof(CamposReadyOnly), true, PersistentValue = true)]
        [SMCConditionalDisplay(nameof(ExibeFormulario), SMCConditionalOperation.Equals, true)]
        [SMCConditionalRequired(nameof(ExibeFormulario), SMCConditionalOperation.Equals, true)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        public long? SeqVisaoFormulario { get; set; }

        public string DescricaoVisaoFormulario { get; set; }        
    }
}