using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using SMC.Localidades.UI.Mvc.DataAnnotation;
using SMC.Localidades.UI.Mvc.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Models
{
    public class DependencyViewModel : SMCViewModelBase
    {
        [SMCDataSource]
        public List<SMCDatasourceItem> Primario { get; set; }

        [SMCDataSource]
        public List<SMCDatasourceItem> Secundario { get; set; }

        [SMCDataSource]
        public List<SMCDatasourceItem> Geral { get; set; }

        [SMCRequired]
        [SMCSize(Framework.SMCSize.Grid4_24 )]
        public long? SeqFora { get; set; }

        [SMCSize(Framework.SMCSize.Grid24_24)]
        [SMCDetail(SMCDetailType.Tabular, 10, 1)]
        public SMCMasterDetailList<DependencyItemViewModel> MasterDetail { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        [SMCRequired]
        [SMCCheckBoxList(nameof(Geral), MaxItems = 5, MinItems = 2)]
        public List<long> Teste { get; set; }

        [SMCHtml(nameof(Tags))]
        [SMCSize(Framework.SMCSize.Grid24_24)]
        [SMCRequired]
        [SMCConditionalDisplay(nameof(SeqFora), SMCConditionalOperation.NotEqual, 1)]
        public string TextEditor { get; set; }

        [SMCSize(Framework.SMCSize.Grid4_24)]
        [SMCMath("{0} + 10", 0, SMCSize.Grid4_24, nameof(SeqFora))]
        public long Math { get; set; }

        [SMCRequired]
        [SMCSize(Framework.SMCSize.Grid4_24)]
        [SMCRadioButtonList]
        public bool? Radio { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCFile]
        public SMCUploadFile Arquivo { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCImage(160,160)]
        public SMCUploadFile Imagem { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCFile]
        //[SMCFile(MaxFiles = 5)]
        public SMCUploadFile[] Arquivos { get; set; }

        [Address]
        public AddressList Address { get; set; }

        public List<string> Tags => new List<string> { "{{Renan}}", "{{Abacaxi}}", "{{Teste}}" };
    }
}