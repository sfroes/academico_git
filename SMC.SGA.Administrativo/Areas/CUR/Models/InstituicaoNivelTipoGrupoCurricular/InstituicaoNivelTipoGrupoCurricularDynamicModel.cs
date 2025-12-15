using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.CUR.Views.InstituicaoNivelTipoGrupoCurricular.App_LocalResources;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class InstituicaoNivelTipoGrupoCurricularDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCHidden(SMCViewMode.All)]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }

        #region Instituicao Nivel

        [SMCFilter(true, true)]
        [SMCOrder(0)]
        [SMCRequired]
        [SMCSelect("InstituicaoNiveis", autoSelectSingleItem: true)]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCHidden(SMCViewMode.List)]
        public long SeqInstituicaoNivel { get; set; }

        [SMCIgnoreProp]
        [SMCDataSource()]
        [SMCServiceReference(typeof(IInstituicaoNivelService), "BuscarNiveisEnsinoDaInstituicaoSelect")]
        public List<SMCDatasourceItem> InstituicaoNiveis { get; set; }

        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.Filter)]
        [SMCInclude("InstituicaoNivel.NivelEnsino")]
        [SMCMapProperty("InstituicaoNivel.NivelEnsino.Descricao")]
        [SMCSortable(true, true, "InstituicaoNivel.NivelEnsino.Descricao")]
        public string DescricaoInstituicaoNivel { get; set; }

        #endregion Instituicao Nivel

        #region Tipo de Grupo Curricular

        [SMCFilter(true)]
        [SMCRequired]
        [SMCSelect("TiposGrupoCurricular", "Seq", "Descricao")]
        [SMCHidden(SMCViewMode.List)]
        [SMCOrder(2)]
        [SMCSize(SMCSize.Grid8_24)]
        public long SeqTipoGrupoCurricular { get; set; }

        [SMCSortable(true, false, "TipoGrupoCurricular.Descricao")]
        [SMCInclude("TipoGrupoCurricular")]
        [SMCMapProperty("TipoGrupoCurricular.Descricao")]
        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.Filter)]
        public string DescricaoTipoGrupoCurricular { get; set; }

        [SMCIgnoreProp]
        [SMCDataSource(dataSource: "TipoGrupoCurricular")]
        [SMCServiceReference(typeof(ICURDynamicService))]
        public List<SMCDatasourceItem> TiposGrupoCurricular { get; set; }

        #endregion Tipo de Grupo Curricular

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .Tokens(tokenInsert: UC_CUR_004_03_01.MANTER_TIPO_GRUPO_CURRICULAR_INSTITUICAO_NIVEL_ENSINO,
                           tokenEdit: UC_CUR_004_03_01.MANTER_TIPO_GRUPO_CURRICULAR_INSTITUICAO_NIVEL_ENSINO,
                           tokenList: UC_CUR_004_03_01.MANTER_TIPO_GRUPO_CURRICULAR_INSTITUICAO_NIVEL_ENSINO,
                           tokenRemove: UC_CUR_004_03_01.MANTER_TIPO_GRUPO_CURRICULAR_INSTITUICAO_NIVEL_ENSINO)
                   .Messages(x => string.Format(UIResource.Listar_Excluir_Confirmacao,
                                ((InstituicaoNivelTipoGrupoCurricularDynamicModel)x).DescricaoTipoGrupoCurricular,
                                ((InstituicaoNivelTipoGrupoCurricularDynamicModel)x).DescricaoInstituicaoNivel));
        }
    }
}