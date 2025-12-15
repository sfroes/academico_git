using SMC.Academico.Common.Areas.APR.Constants;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.APR.Views.InstituicaoNivelEscalaApuracao.App_LocalResources;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.APR.Models
{
    public class InstituicaoNivelEscalaApuracaoDynamicModel : SMCDynamicViewModel
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelService), "BuscarNiveisEnsinoDaInstituicaoSelect")]
        public List<SMCDatasourceItem> NiveisEnsino { get; set; }

        [SMCDataSource("EscalaApuracao")]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IAPRDynamicService))]
        public List<SMCDatasourceItem> EscalasApuracao { get; set; }

        #endregion [ DataSources ]

        [SMCKey]
        [SMCHidden]
        public override long Seq { get; set; }

        [SMCFilter(true, true)]
        [SMCHidden(SMCViewMode.List)]
        [SMCRequired]
        [SMCSelect("NiveisEnsino")]
        [SMCSize(SMCSize.Grid9_24)]
        public long SeqInstituicaoNivel { get; set; }

        [SMCHidden(SMCViewMode.Edit | SMCViewMode.Filter | SMCViewMode.Insert)]
        [SMCInclude("InstituicaoNivel.NivelEnsino")]
        [SMCMapProperty("InstituicaoNivel.NivelEnsino.Descricao")]
        [SMCSortable(true, true, "InstituicaoNivel.NivelEnsino.Descricao")]
        public string DescricaoInstituicaoNivel { get; set; }

        [SMCFilter(true, true)]
        [SMCHidden(SMCViewMode.List)]
        [SMCRequired]
        [SMCSelect("EscalasApuracao", AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid9_24)]
        public long SeqEscalaApuracao { get; set; }

        [SMCHidden(SMCViewMode.Edit | SMCViewMode.Filter | SMCViewMode.Insert)]
        [SMCMapProperty("EscalaApuracao.Descricao")]
        [SMCSortable(true, true, "EscalaApuracao.Descricao")]
        public string DescricaoEscalaApuracao { get; set; }

        #region [ Configuração ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .Messages(x => string.Format(UIResource.Listar_Excluir_Confirmacao,
                                ((InstituicaoNivelEscalaApuracaoDynamicModel)x).DescricaoEscalaApuracao,
                                ((InstituicaoNivelEscalaApuracaoDynamicModel)x).DescricaoInstituicaoNivel))
                   .Tokens(tokenList: UC_APR_003_01_01.MANTER_ESCALA_APURACAO_INSTITUICAO_NIVEL,
                           tokenInsert: UC_APR_003_01_01.MANTER_ESCALA_APURACAO_INSTITUICAO_NIVEL,
                           tokenEdit: UC_APR_003_01_01.MANTER_ESCALA_APURACAO_INSTITUICAO_NIVEL,
                           tokenRemove: UC_APR_003_01_01.MANTER_ESCALA_APURACAO_INSTITUICAO_NIVEL);
        }

        #endregion [ Configuração ]
    }
}