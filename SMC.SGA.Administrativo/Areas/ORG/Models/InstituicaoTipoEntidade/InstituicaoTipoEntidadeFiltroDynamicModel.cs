using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class InstituicaoTipoEntidadeFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region DataSources

        [SMCDataSource("TipoEntidade", "Seq", "Descricao", SortBy = SMCSortBy.Description)]
        [SMCServiceReference(typeof(IORGDynamicService))]
        public List<SMCDatasourceItem> TiposEntidade { get; set; }

        #endregion DataSources

        [SMCSelect(nameof(TiposEntidade))]
        [SMCSize(SMCSize.Grid6_24)]
        public long? SeqTipoEntidade { get; set; }

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }
    }
}