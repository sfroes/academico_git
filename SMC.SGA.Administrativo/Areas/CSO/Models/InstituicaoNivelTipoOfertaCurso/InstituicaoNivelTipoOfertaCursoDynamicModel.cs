using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.CSO.Views.InstituicaoNivelTipoOfertaCurso.App_LocalResources;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class InstituicaoNivelTipoOfertaCursoDynamicModel : SMCDynamicViewModel
    {
        [SMCHidden]
        [SMCKey]
        [SMCOrder(0)]
        [SMCRequired]
        public override long Seq { get; set; }

        [SMCFilter(true, true)]
        [SMCHidden(SMCViewMode.List)]
        [SMCOrder(1)]
        [SMCRequired]
        [SMCSelect("InstituicaoNiveis")]
        [SMCSize(SMCSize.Grid8_24)]
        public long SeqInstituicaoNivel { get; set; }

        [SMCDataSource()]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelService), "BuscarNiveisEnsinoDaInstituicaoSelect")]
        public List<SMCDatasourceItem> InstituicaoNiveis { get; set; }

        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.Filter)]
        [SMCInclude("InstituicaoNivel.NivelEnsino")]
        [SMCMapProperty("InstituicaoNivel.NivelEnsino.Descricao")]
        [SMCOrder(1)]
        [SMCSortable(true, true, "InstituicaoNivel.NivelEnsino.Descricao")]
        public string DescricaoInstituicaoNivel { get; set; }

        [SMCFilter(true, true)]
        [SMCHidden(SMCViewMode.List)]
        [SMCOrder(2)]
        [SMCRequired]
        [SMCSelect("TiposOfertaCurso", "Seq", "Descricao", SortBy = SMCSortBy.Description)]
        [SMCServiceReference(typeof(ICSODynamicService))]
        [SMCSize(SMCSize.Grid8_24)]
        public long SeqTipoOfertaCurso { get; set; }

        [SMCDataSource("TipoOfertaCurso")]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ICSODynamicService))]
        public List<SMCDatasourceItem> TiposOfertaCurso { get; set; }

        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.Filter)]
        [SMCInclude("TipoOfertaCurso")]
        [SMCMapProperty("TipoOfertaCurso.Descricao")]
        [SMCOrder(2)]
        [SMCSortable(true, true, "TipoOfertaCurso.Descricao")]
        public string DescricaoTipoOfertaCurso { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .Messages(x => string.Format(UIResource.Listar_Excluir_Confirmacao,
                                ((InstituicaoNivelTipoOfertaCursoDynamicModel)x).DescricaoTipoOfertaCurso,
                                ((InstituicaoNivelTipoOfertaCursoDynamicModel)x).DescricaoInstituicaoNivel))
                   .Tokens(tokenEdit: UC_CSO_003_04_01.MANTER_TIPO_OFERTA_CURSO_INSTITUICAO_NIVEL,
                           tokenInsert: UC_CSO_003_04_01.MANTER_TIPO_OFERTA_CURSO_INSTITUICAO_NIVEL,
                           tokenList: UC_CSO_003_04_01.MANTER_TIPO_OFERTA_CURSO_INSTITUICAO_NIVEL,
                           tokenRemove: UC_CSO_003_04_01.MANTER_TIPO_OFERTA_CURSO_INSTITUICAO_NIVEL);
        }
    }
}