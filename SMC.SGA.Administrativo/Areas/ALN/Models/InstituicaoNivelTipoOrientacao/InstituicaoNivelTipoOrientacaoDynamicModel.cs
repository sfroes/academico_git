using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORT.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class InstituicaoNivelTipoOrientacaoDynamicModel : SMCDynamicViewModel
    {
        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoOrientacaoService), nameof(ITipoOrientacaoService.BuscarTipoOrientacaoNaoOrientacaoTurmaSelect))]
        public List<SMCDatasourceItem> TiposOrientacao { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelTipoTermoIntercambioService), nameof(IInstituicaoNivelTipoTermoIntercambioService.BuscarInstituicaoNivelTipoTermoIntercambioSelect), values: new string[] { nameof(SeqInstituicaoNivelTipoVinculoAluno) })]
        public List<SMCDatasourceItem> TiposTermosIntercambio { get; set; }

        [SMCHidden]
        public override long Seq { get; set; }

        [SMCParameter]
        [SMCHidden]
        public long SeqInstituicaoNivelTipoVinculoAluno { get; set; }

        [SMCSelect("TiposTermosIntercambio")]
        [SMCSize(Framework.SMCSize.Grid12_24)]
        public long? SeqInstituicaoNivelTipoTermoIntercambio { get; set; }

        [SMCSelect("TiposOrientacao")]
        [SMCRequired]
        [SMCSize(Framework.SMCSize.Grid12_24)]
        public long SeqTipoOrientacao { get; set; }

        [SMCSelect]
        [SMCRequired]
        [SMCSize(Framework.SMCSize.Grid8_24)]
        public CadastroOrientacao CadastroOrientacaoIngressante { get; set; }

        [SMCSelect]
        [SMCRequired]
        [SMCSize(Framework.SMCSize.Grid8_24)]
        public CadastroOrientacao CadastroOrientacaoAluno { get; set; }

        [SMCMask("9999")]
        [SMCRequired]
        [SMCSize(Framework.SMCSize.Grid8_24)]
        public short? QuantidadeMaximaAluno { get; set; }

        [SMCSize(Framework.SMCSize.Grid24_24)]
        [SMCDetail(min: 1)]
        public SMCMasterDetailList<InstituicaoNivelTipoOrientacaoParticipacaoViewModel> TiposParticipacao { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options
                .EditInModal()
                .ModalSize(SMCModalWindowSize.Largest)
                .ButtonBackIndex("Index", "InstituicaoNivelTipoVinculoAluno")
                .Service<IInstituicaoNivelTipoOrientacaoService>(save: nameof(IInstituicaoNivelTipoOrientacaoService.SalvarInstituicaoNivelTipoOrientacao))
                .Tokens(tokenEdit: UC_ALN_003_01_03.ASSOCIAR_TIPO_ORIENTACAO,
                        tokenInsert: UC_ALN_003_01_03.ASSOCIAR_TIPO_ORIENTACAO,
                        tokenRemove: UC_ALN_003_01_03.ASSOCIAR_TIPO_ORIENTACAO,
                        tokenList: UC_ALN_003_01_03.ASSOCIAR_TIPO_ORIENTACAO);
        }
    }
}