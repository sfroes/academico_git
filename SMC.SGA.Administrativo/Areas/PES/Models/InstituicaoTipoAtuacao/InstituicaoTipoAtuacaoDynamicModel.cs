using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.PES.Views.InstituicaoTipoAtuacao.App_LocalResources;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class InstituicaoTipoAtuacaoDynamicModel : SMCDynamicViewModel
    {
        [SMCHidden]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }

        [SMCDescription]
        [SMCHidden(SMCViewMode.Filter)]
        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid24_24)]
        public TipoAtuacao TipoAtuacao { get; set; }

        [SMCGroupedProperty("Filiacao")]
        [SMCHidden(SMCViewMode.Filter)]
        [SMCMask("99")]
        [SMCMinValue(0)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid10_24)]
        public short? QuantidadeMinimaFiliacaoObrigatoria { get; set; }

        [SMCGroupedProperty("Filiacao")]
        [SMCHidden(SMCViewMode.Filter)]
        [SMCMask("99")]
        [SMCMinValue(nameof(QuantidadeMinimaFiliacaoObrigatoria))]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid10_24)]
        public short? QuantidadeMaximaFiliacao { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .ModalSize(SMCModalWindowSize.Medium)
                   .IgnoreFilterGeneration()
                   .Messages(x => string.Format(UIResource.Listar_Excluir_Confirmacao,
                                ((InstituicaoTipoAtuacaoDynamicModel)x).TipoAtuacao.SMCGetDescription()))
                   .Service<IInstituicaoTipoAtuacaoService>(index: nameof(IInstituicaoTipoAtuacaoService.BuscarInstituicoesTiposAtuacoes),
                                                             save: nameof(IInstituicaoTipoAtuacaoService.SalvarInstituicaoTipoAtuacao),
                                                             edit: nameof(IInstituicaoTipoAtuacaoService.BuscarInstituicaoTipoAtuacao),
                                                           delete: nameof(IInstituicaoTipoAtuacaoService.ExcluirInstituicaoTipoAtuacao))
                   .Tokens(tokenEdit: UC_PES_003_01_01.MANTER_TIPO_ATUACAO_INSTITUICAO,
                           tokenInsert: UC_PES_003_01_01.MANTER_TIPO_ATUACAO_INSTITUICAO,
                           tokenRemove: UC_PES_003_01_01.MANTER_TIPO_ATUACAO_INSTITUICAO,
                           tokenList: UC_PES_003_01_01.MANTER_TIPO_ATUACAO_INSTITUICAO);
        }
    }
}