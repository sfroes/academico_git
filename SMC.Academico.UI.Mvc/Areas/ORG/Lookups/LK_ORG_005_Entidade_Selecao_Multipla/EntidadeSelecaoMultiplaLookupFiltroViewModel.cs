using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.ORG.Lookups
{
    public class EntidadeSelecaoMultiplaLookupFiltroViewModel : SMCLookupFilterViewModel
    {
        #region DataSources

        public List<SMCDatasourceItem> TiposEntidade { get; set; }

        #endregion DataSources

        #region Campos Auxiliares

        [SMCHidden]
        public long? SeqInstituicaoEnsino { get; set; }

        #endregion Campos Auxiliares

        [SMCKey]
        [SMCSize(SMCSize.Grid4_24)]
        public long? Seq { get; set; }

        [SMCSelect(nameof(TiposEntidade), AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid6_24)]
        public List<long> SeqsTipoEntidade { get; set; }

        [SMCDescription]
        [SMCMaxLength(100)]
        [SMCSortable(true, true)]
        [SMCSize(SMCSize.Grid14_24)]
        public string Nome { get; set; }
       
        [SMCHidden]
        public List<long> SeqsEntidadesCompartilhadas { get; set; }

        [SMCHidden]
        public List<long> SeqsEntidadesResponsaveis { get; set; }
    }
}
