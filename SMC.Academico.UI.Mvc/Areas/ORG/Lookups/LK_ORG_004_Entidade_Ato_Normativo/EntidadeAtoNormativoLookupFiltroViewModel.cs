using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Constants;
using SMC.EstruturaOrganizacional.UI.Mvc.Areas.ESO.Lookups;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.ORG.Lookups
{
    public class EntidadeAtoNormativoLookupFiltroViewModel : SMCLookupFilterViewModel
    {
        #region [Datasource]

        public List<SMCDatasourceItem> TiposEntidade { get; set; }

        public List<SMCDatasourceItem> TiposOrgaoRegulador { get; set; }

        #endregion

        [SMCKey]
        [SMCSize(SMCSize.Grid4_24)]
        public long? Seq { get; set; }

        [SMCDescription]
        [SMCSize(SMCSize.Grid8_24)]
        public string Nome { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        [SMCSelect(nameof(TiposEntidade), AutoSelectSingleItem = true)]
        public long? SeqTipoEntidade { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        [SMCInclude(true)] // O Dynamic gera include automático dos lookups, ignorado por ser uma entidade externa
        [UnidadeLookup]
        public UnidadeLookupViewModel CodigoUnidadeSeo { get; set; }

        [SMCHidden]
        [SMCDependency(nameof(SeqTipoEntidade), "BuscarTokenTipoEntidade", "AtoNormativo", "ORG", true)]
        public string Token { get; set; }

        [SMCSelect(nameof(TiposOrgaoRegulador))]
        [SMCConditional(SMCConditionalBehavior.Visibility, nameof(Token), SMCConditionalOperation.Equals, TOKEN_TIPO_ENTIDADE_EXTERNADA.CURSO_OFERTA_LOCALIDADE)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        public TipoOrgaoRegulador? TipoOrgaoRegulador { get; set; }

        [SMCConditional(SMCConditionalBehavior.Visibility, nameof(Token), SMCConditionalOperation.Equals, TOKEN_TIPO_ENTIDADE_EXTERNADA.CURSO_OFERTA_LOCALIDADE)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        public int? CodigoOrgaoRegulador { get; set; }

        [SMCHidden]
        public long SeqInstituicaoEnsino { get; set; }

        [SMCHidden]
        public bool PermiteAtoNormativo { get; set; }

        [SMCHidden]
        public bool LookupEntidadeAtoNormativo { get; set; }
    }
}
