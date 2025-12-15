using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.ORG.Controllers;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    [SMCGroupedPropertyConfiguration(GroupId = "EntidadeAssociada", Size = SMCSize.Grid24_24)]
    public class AtoNormativoFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region [DataSource]

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IAssuntoNormativoService), nameof(IAssuntoNormativoService.BuscarAssuntosNormativoSelect))]
        public List<SMCDatasourceItem> AssuntosNormativos { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoAtoNormativoService), nameof(ITipoAtoNormativoService.BuscarTiposAtoNormativoSelect))]
        public List<SMCDatasourceItem> TiposAtoNormativos { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoEntidadeService), nameof(ITipoEntidadeService.BuscarTiposEntidadeComInstituicao), values: new[] { nameof(PermiteAtoNormativo), nameof(SeqInstituicaoEnsino) })]
        public List<SMCDatasourceItem> TiposEntidades { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IGrauAcademicoService), nameof(IGrauAcademicoService.BuscarGrauAcademicoSelect))]
        public List<SMCDatasourceItem> GrauAcademico { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarTipoOrgaoReguladorInstituicao), values: new string[] { nameof(SeqInstituicaoEnsino) })]
        public List<SMCSelectListItem> TiposOrgaoRegulador { get; set; }

        #endregion [DataSource]

        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid2_24)]
        public long? Seq { get; set; }

        [SMCFilter(true, true)]
        [SMCSelect(nameof(AssuntosNormativos))]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid7_24)]
        public long? SeqAssuntoNormativo { get; set; }

        [SMCFilter(true, true)]
        [SMCSelect(nameof(TiposAtoNormativos))]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid7_24)]
        public long? SeqTipoAtoNormativo { get; set; }

        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        public string NumeroDocumento { get; set; }

        [SMCFilter]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        public DateTime? DataDocumento { get; set; }

        [SMCFilter]
        [SMCGroupedProperty("EntidadeAssociada")]
        [SMCSize(SMCSize.Grid2_24, SMCSize.Grid24_24, SMCSize.Grid2_24, SMCSize.Grid2_24)]
        public long? SeqEntidade { get; set; }

        [SMCFilter]
        [SMCSelect(nameof(TiposEntidades))]
        [SMCGroupedProperty("EntidadeAssociada")]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid5_24, SMCSize.Grid5_24)]
        public long? SeqTipoEntidade { get; set; }

        [SMCFilter]
        [SMCGroupedProperty("EntidadeAssociada")]
        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid9_24, SMCSize.Grid9_24)]
        public string NomeEntidade { get; set; }

        [SMCHidden]
        [SMCDependency(nameof(SeqTipoEntidade), nameof(AtoNormativoController.BuscarTokenTipoEntidade), "AtoNormativo", true)]
        public string Token { get; set; }

        [SMCFilter]
        [SMCSelect(nameof(GrauAcademico))]
        [SMCGroupedProperty("EntidadeAssociada")]
        [SMCConditional(SMCConditionalBehavior.Visibility, nameof(Token), SMCConditionalOperation.Equals, TOKEN_TIPO_ENTIDADE_EXTERNADA.CURSO_OFERTA_LOCALIDADE)]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid5_24, SMCSize.Grid5_24)]
        public long? SeqGrauAcademico { get; set; }

        [SMCFilter]
        [SMCSelect(nameof(TiposOrgaoRegulador))]
        [SMCGroupedProperty("EntidadeAssociada")]
        [SMCConditional(SMCConditionalBehavior.Visibility, nameof(Token), SMCConditionalOperation.Equals, TOKEN_TIPO_ENTIDADE_EXTERNADA.CURSO_OFERTA_LOCALIDADE)]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid3_24, SMCSize.Grid3_24)]
        public TipoOrgaoRegulador? TipoOrgaoRegulador { get; set; }

        [SMCFilter]
        [SMCGroupedProperty("EntidadeAssociada")]
        [SMCConditional(SMCConditionalBehavior.Visibility, nameof(Token), SMCConditionalOperation.Equals, TOKEN_TIPO_ENTIDADE_EXTERNADA.CURSO_OFERTA_LOCALIDADE)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        public int? CodigoCursoOferta { get; set; }

        [SMCFilter]
        [SMCGroupedProperty("EntidadeAssociada")]
        [SMCConditional(SMCConditionalBehavior.Visibility, nameof(Token), SMCConditionalOperation.Equals, TOKEN_TIPO_ENTIDADE_EXTERNADA.CURSO_OFERTA_LOCALIDADE)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        public int? CodigoOrgaoRegulador { get; set; }

        [SMCHidden]
        public bool PermiteAtoNormativo { get; set; } = true;

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }
    }
}
