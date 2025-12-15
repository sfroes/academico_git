using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.CUR.Views.InstituicaoNivelTipoDivisaoCurricular.App_LocalResources;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class InstituicaoNivelTipoDivisaoCurricularDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCHidden]
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
        [SMCSelect("TipoDivisaoCurriculares", "Seq", "Descricao", SortBy = SMCSortBy.Description)]
        [SMCSize(SMCSize.Grid8_24)]
        public long SeqTipoDivisaoCurricular { get; set; }

        [SMCDataSource("TipoDivisaoCurricular")]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ICURDynamicService))]
        public List<SMCDatasourceItem> TipoDivisaoCurriculares { get; set; }

        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.Filter)]
        [SMCInclude("TipoDivisaoCurricular")]
        [SMCMapProperty("TipoDivisaoCurricular.Descricao")]
        [SMCOrder(2)]
        [SMCSortable(true, true, "TipoDivisaoCurricular.Descricao")]
        public string DescricaoTipoDivisaoCurricular { get; set; }

        #region Configurações

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .Messages(x => string.Format(UIResource.Listar_Excluir_Confirmacao,
                                ((InstituicaoNivelTipoDivisaoCurricularDynamicModel)x).DescricaoTipoDivisaoCurricular,
                                ((InstituicaoNivelTipoDivisaoCurricularDynamicModel)x).DescricaoInstituicaoNivel))
                   .Tokens(tokenList: UC_CUR_004_02_01.MANTER_TIPO_DIVISAO_CURRICULAR_INSTITUICAO_NIVEL_ENSINO,
                           tokenInsert: UC_CUR_004_02_01.MANTER_TIPO_DIVISAO_CURRICULAR_INSTITUICAO_NIVEL_ENSINO,
                           tokenEdit: UC_CUR_004_02_01.MANTER_TIPO_DIVISAO_CURRICULAR_INSTITUICAO_NIVEL_ENSINO,
                           tokenRemove: UC_CUR_004_02_01.MANTER_TIPO_DIVISAO_CURRICULAR_INSTITUICAO_NIVEL_ENSINO);
        }

        #endregion Configurações
    }
}