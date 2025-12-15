using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.DCT.Lookups
{
    public class InstituicaoExternaLookupFiltroViewModel : SMCLookupFilterViewModel
    {
        #region [ DataSources ]

        public List<SMCDatasourceItem> Paises { get; set; }

        public List<SMCDatasourceItem> CategoriasInstituicaoEnsino { get; set; }

        #endregion [ DataSources ]

        [SMCHidden]
        public bool RetornarInstituicaoEnsinoLogada { get; set; }

        [SMCHidden]
        public bool ListarSomenteInstituicoesEnsino { get; set; }

        [SMCHidden]
        public long SeqInstituicaoEnsino { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long? SeqColaborador { get; set; }

        [SMCKey]
        [SMCHidden]
        public long? Seq { get; set; }

        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid3_24)]
        public string Sigla { get; set; }

        [SMCDescription]
        [SMCSize(SMCSize.Grid15_24, SMCSize.Grid24_24, SMCSize.Grid20_24, SMCSize.Grid15_24)]
        public string Nome { get; set; }

        [SMCSelect(nameof(Paises))]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid16_24, SMCSize.Grid6_24)]
        public long? CodigoPais { get; set; }

        [SMCSelect]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid3_24)]
        public bool? Ativo { get; set; }

        [SMCSelect]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid3_24)]
        public TipoInstituicaoEnsino? TipoInstituicaoEnsino { get; set; }

        [SMCSelect(nameof(CategoriasInstituicaoEnsino))]
        [SMCSize(SMCSize.Grid14_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid14_24)]
        public long? SeqCategoriaInstituicaoEnsino { get; set; }
    }
}