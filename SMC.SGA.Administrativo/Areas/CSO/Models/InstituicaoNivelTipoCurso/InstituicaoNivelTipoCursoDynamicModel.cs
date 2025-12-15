using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.Common.Areas.CSO.Enums;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.CSO.Views.InstituicaoNivelTipoCurso.App_LocalResources;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class InstituicaoNivelTipoCursoDynamicModel : SMCDynamicViewModel
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
        [SMCOrder(2)]
        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid8_24)]
        public TipoCurso? TipoCurso { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .Messages(x => string.Format(UIResource.Listar_Excluir_Confirmacao,
                                ((InstituicaoNivelTipoCursoDynamicModel)x).TipoCurso.SMCGetDescription(),
                                ((InstituicaoNivelTipoCursoDynamicModel)x).DescricaoInstituicaoNivel))
                   .Tokens(tokenInsert: UC_CSO_003_03_01.MANTER_TIPO_CURSO_INSTITUICAO_NIVEL,
                           tokenEdit: UC_CSO_003_03_01.MANTER_TIPO_CURSO_INSTITUICAO_NIVEL,
                           tokenList: UC_CSO_003_03_01.MANTER_TIPO_CURSO_INSTITUICAO_NIVEL,
                           tokenRemove: UC_CSO_003_03_01.MANTER_TIPO_CURSO_INSTITUICAO_NIVEL);
        }
    }
}