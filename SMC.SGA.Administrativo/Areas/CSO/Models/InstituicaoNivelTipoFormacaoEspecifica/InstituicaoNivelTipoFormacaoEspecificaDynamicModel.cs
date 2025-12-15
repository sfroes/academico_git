using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.Common.Areas.CSO.Enums;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.CSO.Views.InstituicaoNivelTipoFormacaoEspecifica.App_LocalResources;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class InstituicaoNivelTipoFormacaoEspecificaDynamicModel : SMCDynamicViewModel
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoDaInstituicaoSelect))]
        public List<SMCDatasourceItem> InstituicaoNiveis { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoFormacaoEspecificaService),
                             nameof(ITipoFormacaoEspecificaService.BuscarTipoFormacaoEspecificaSelect),
                             values: new[] { nameof(Ativo), nameof(ClasseTipoFormacao) })]
        public List<SMCDatasourceItem> TiposFormacaoEspecifica { get; set; }

        #endregion [ DataSources ]

        [SMCHidden]
        [SMCKey]
        [SMCOrder(0)]
        [SMCRequired]
        public override long Seq { get; set; }

        [SMCFilter(true, true)]
        [SMCOrder(1)]
        [SMCRequired]
        [SMCSelect(nameof(InstituicaoNiveis), NameDescriptionField = nameof(DescricaoInstituicaoNivel))]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCSortable(true, true, "InstituicaoNivel.NivelEnsino.Descricao")]
        public long SeqInstituicaoNivel { get; set; }

        [SMCFilter(true, true)]
        [SMCOrder(2)]
        [SMCRequired]
        [SMCSelect(nameof(TiposFormacaoEspecifica), SortBy = SMCSortBy.Description, NameDescriptionField = nameof(DescricaoTipoFormacaoEspecifica))]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCSortable(true, true, "TipoFormacaoEspecifica.Descricao")]
        public long SeqTipoFormacaoEspecifica { get; set; }

        #region [ Parâmetros ]

        [SMCIgnoreProp]
        public bool Ativo { get; set; } = true;

        [SMCIgnoreProp]
        public ClasseTipoFormacao ClasseTipoFormacao { get; set; } = ClasseTipoFormacao.Curso;

        #endregion [ Parâmetros ]

        #region [ Campos mensagem ]

        [SMCIgnoreProp]
        [SMCInclude("InstituicaoNivel.NivelEnsino")]
        [SMCMapProperty("InstituicaoNivel.NivelEnsino.Descricao")]
        public string DescricaoInstituicaoNivel { get; set; }

        [SMCIgnoreProp]
        [SMCInclude("TipoFormacaoEspecifica")]
        [SMCMapProperty("TipoFormacaoEspecifica.Descricao")]
        public string DescricaoTipoFormacaoEspecifica { get; set; }

        #endregion [ Campos mensagem ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .Messages(x => string.Format(UIResource.Listar_Excluir_Confirmacao,
                                ((InstituicaoNivelTipoFormacaoEspecificaDynamicModel)x).DescricaoTipoFormacaoEspecifica,
                                ((InstituicaoNivelTipoFormacaoEspecificaDynamicModel)x).DescricaoInstituicaoNivel))
                   .Tokens(tokenInsert: UC_CSO_003_05_01.MANTER_ASSOCIACAO_TIPO_FORMACAO_INSTITUICAO_NIVEL,
                       tokenEdit: UC_CSO_003_05_01.MANTER_ASSOCIACAO_TIPO_FORMACAO_INSTITUICAO_NIVEL,
                       tokenRemove: UC_CSO_003_05_01.MANTER_ASSOCIACAO_TIPO_FORMACAO_INSTITUICAO_NIVEL,
                       tokenList: UC_CSO_003_05_01.MANTER_ASSOCIACAO_TIPO_FORMACAO_INSTITUICAO_NIVEL);
        }
    }
}