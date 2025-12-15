using SMC.Academico.Common.Areas.APR.Constants;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.APR.Views.InstituicaoNivelCriterioAprovacao.App_LocalResources;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.APR.Models
{
    public class InstituicaoNivelCriterioAprovacaoDynamicModel : SMCDynamicViewModel
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelService), "BuscarNiveisEnsinoDaInstituicaoSelect")]
        public List<SMCDatasourceItem> NiveisEnsino { get; set; }

        [SMCDataSource("CriterioAprovacao")]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelCriterioAprovacaoService), "BuscarCriteriosAprovacaoDaInstituicaoNivelSelect",
            values: new[] { nameof(SeqInstituicaoNivel) })]
        public List<SMCDatasourceItem> CriteriosAprovacao { get; set; }

        #endregion [ DataSources ]

        [SMCKey]
        [SMCHidden]
        public override long Seq { get; set; }

        [SMCFilter(true, true)]
        [SMCHidden(SMCViewMode.List)]
        [SMCRequired]
        [SMCSelect("NiveisEnsino")]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        public long SeqInstituicaoNivel { get; set; }

        [SMCHidden(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCInclude("InstituicaoNivel.NivelEnsino")]
        [SMCMapProperty("InstituicaoNivel.NivelEnsino.Descricao")]
        [SMCSortable(true, true, "InstituicaoNivel.NivelEnsino.Descricao")]
        public string DescricaoInstituicaoNivel { get; set; }

        [SMCHidden(SMCViewMode.List)]
        [SMCDependency(nameof(SeqInstituicaoNivel), "BuscarCriteriosAprovacaoNivelEnsinoSelect", "InstituicaoNivelCriterioAprovacao", true)]
        [SMCRequired]
        [SMCSelect(nameof(CriteriosAprovacao), AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        public long SeqCriterioAprovacao { get; set; }

        [SMCHidden(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCInclude("CriterioAprovacao")]
        [SMCMapProperty("CriterioAprovacao.Descricao")]
        [SMCSortable(true, true, "CriterioAprovacao.Descricao")]
        public string DescricaoCriterioAprovacao { get; set; }

        #region [ Configuração ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .Messages(x => string.Format(UIResource.Listar_Excluir_Confirmacao,
                                ((InstituicaoNivelCriterioAprovacaoDynamicModel)x).DescricaoCriterioAprovacao,
                                ((InstituicaoNivelCriterioAprovacaoDynamicModel)x).DescricaoInstituicaoNivel))
                   .Tokens(tokenList: UC_APR_003_02_01.MANTER_CRITERIO_APROVACAO_INSTITUICAO_NIVEL,
                           tokenInsert: UC_APR_003_02_01.MANTER_CRITERIO_APROVACAO_INSTITUICAO_NIVEL,
                           tokenEdit: UC_APR_003_02_01.MANTER_CRITERIO_APROVACAO_INSTITUICAO_NIVEL,
                           tokenRemove: UC_APR_003_02_01.MANTER_CRITERIO_APROVACAO_INSTITUICAO_NIVEL);
        }

        #endregion [ Configuração ]
    }
}