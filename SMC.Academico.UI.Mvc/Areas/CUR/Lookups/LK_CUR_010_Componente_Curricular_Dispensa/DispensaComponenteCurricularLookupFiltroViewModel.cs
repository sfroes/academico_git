using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Lookups
{
    public class DispensaComponenteCurricularLookupFiltroViewModel : SMCLookupFilterViewModel
    {
        public DispensaComponenteCurricularLookupFiltroViewModel()
        {
            // Foi iniciado o pagesettings aqui com int.maxvalue pois este lookup nao tem paginação e não deve exibir os controles de alteração de registros por página, etc.
            this.PageSettings = this.PageSettings ?? new SMCPageSetting();
            this.PageSettings.PageSize = int.MaxValue;
        }

        #region [ DataSource ]

        [SMCDataSource(SMCStorageType.None)]
        [SMCIgnoreProp]
        public List<SMCDatasourceItem> OfertasCurso { get; set; }
                
        #endregion [ DataSource ]

        [SMCHidden]
        [SMCParameter]
        public long SeqPessoaAtuacao { get; set; }

        [SMCOrder(1)]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCSelect]
        public bool? DisciplinaIsolada { get; set; }

        [SMCConditionalReadonly(nameof(DisciplinaIsolada), SMCConditionalOperation.NotEqual, "false")]
        [SMCConditionalRequired(nameof(DisciplinaIsolada), SMCConditionalOperation.Equals, "false")]
        [SMCOrder(2)]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCSelect(nameof(OfertasCurso),ForceMultiSelect = true, AutoSelectSingleItem = true)]
        public long? SeqOfertaCurso { get; set; }

        
    }
}
