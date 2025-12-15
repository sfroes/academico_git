using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.SRC.Controllers;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class GrupoDocumentoRequeridoViewModel : SMCViewModelBase
    {
        #region Datasource    

        public List<SMCDatasourceItem> DocumentosRequeridos { get; set; }

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

        [SMCRequired]
        [SMCDescription]
        [SMCSize(SMCSize.Grid21_24, SMCSize.Grid24_24, SMCSize.Grid21_24, SMCSize.Grid21_24)]
        public string Descricao { get; set; }

        [SMCRequired]
        [SMCMinValue(1)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        public short MinimoObrigatorio { get; set; }

        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        public bool? UploadObrigatorio { get; set; }

        [SMCDetail]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<GrupoDocumentoRequeridoItemViewModel> Itens { get; set; }

        public string Mensagem { get; set; }       
    }
}