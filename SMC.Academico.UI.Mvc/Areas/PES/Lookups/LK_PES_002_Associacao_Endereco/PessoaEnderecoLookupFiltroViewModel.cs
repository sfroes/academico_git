using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Localidades.Common.Areas.LOC.Enums;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.PES.Lookups
{
    public class PessoaEnderecoLookupFiltroViewModel : SMCLookupFilterViewModel
    {
        #region [ DataSource ]

        [SMCDataSource(storageType: SMCStorageType.TempData)]
        public List<SMCDatasourceItem> EnderecosCorrespondencia { get; set; }

        #endregion [ DataSource ]

        [SMCHidden]
        public long SeqPessoa { get; set; }

        [SMCHidden]
        public TipoAtuacao TipoAtuacao { get; set; }

        [SMCSize(Framework.SMCSize.Grid6_24)]
        [SMCSelect]
        public TipoEndereco? TipoEndereco { get; set; }
    }
}