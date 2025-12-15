using SMC.Academico.Common.Areas.APR.Constants;
using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.APR.Views.InstituicaoNivelTipoOrigemAvaliacao.App_LocalResources;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.APR.Models
{
    public class InstituicaoNivelTipoOrigemAvaliacaoDynamicModel : SMCDynamicViewModel
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoDaInstituicaoSelect))]
        public List<SMCDatasourceItem> NiveisEnsino { get; set; }

        #endregion [ DataSources ]

        [SMCKey]
        [SMCHidden]
        public override long Seq { get; set; }

        [SMCFilter(true, true)]
        [SMCHidden(SMCViewMode.List)]
        [SMCRequired]
        [SMCSelect(nameof(NiveisEnsino))]
        [SMCSize(SMCSize.Grid9_24)]
        public long? SeqInstituicaoNivel { get; set; }

        [SMCHidden(SMCViewMode.Edit | SMCViewMode.Filter | SMCViewMode.Insert)]
        [SMCInclude("InstituicaoNivel.NivelEnsino")]
        [SMCMapProperty("InstituicaoNivel.NivelEnsino.Descricao")]
        [SMCSortable(true, true, "InstituicaoNivel.NivelEnsino.Descricao")]
        public string DescricaoInstituicaoNivel { get; set; }

        [SMCFilter(true, true)]
        [SMCRequired]
        [SMCSelect()]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCSortable(true, true, "TipoOrigemAvaliacao")]
        public TipoOrigemAvaliacao? TipoOrigemAvaliacao { get; set; }

        #region [ Configuração ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .Messages(x => string.Format(UIResource.Listar_Excluir_Confirmacao,
                                ((InstituicaoNivelTipoOrigemAvaliacaoDynamicModel)x).DescricaoInstituicaoNivel,
                              SMC.Framework.Util.SMCEnumHelper.GetDescription(((InstituicaoNivelTipoOrigemAvaliacaoDynamicModel)x).TipoOrigemAvaliacao)))
                   .Tokens(tokenList: UC_APR_003_03_01.MANTER_TIPO_ORIGEM_AVALIACAO_INSTITUICAO_NIVEL,
                           tokenInsert: UC_APR_003_03_01.MANTER_TIPO_ORIGEM_AVALIACAO_INSTITUICAO_NIVEL,
                           tokenEdit: UC_APR_003_03_01.MANTER_TIPO_ORIGEM_AVALIACAO_INSTITUICAO_NIVEL,
                           tokenRemove: UC_APR_003_03_01.MANTER_TIPO_ORIGEM_AVALIACAO_INSTITUICAO_NIVEL);
        }

        #endregion [ Configuração ]
    }
}