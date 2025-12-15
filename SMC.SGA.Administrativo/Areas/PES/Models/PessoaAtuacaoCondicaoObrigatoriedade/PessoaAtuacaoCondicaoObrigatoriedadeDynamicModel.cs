using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.PES.Controllers;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class PessoaAtuacaoCondicaoObrigatoriedadeDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCHidden]
        public override long Seq { get; set; }

        [SMCHidden]
        public long SeqPessoaAtuacao { get; set; }

        [SMCHidden]
        public bool PossuiSituacaoImpeditivaIngressante { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        [SMCDetail(HideMasterDetailButtons = true)]
        [SMCInclude(nameof(CondicoesObrigatoriedade))]
        public SMCMasterDetailList<PessoaAtuacaoCondicaoObrigatoriedadeViewModel> CondicoesObrigatoriedade { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.RequiredIncomingParameters(new[] { nameof(SeqPessoaAtuacao) })
                   .Header(nameof(PessoaAtuacaoCondicaoObrigatoriedadeController.CabecalhoIngressante))
                   .EditInModal(allowSaveNew: false)
                   .IgnoreFilterGeneration()
                   .ModalSize(SMCModalWindowSize.Large)
                   .Tokens(tokenInsert: UC_ALN_002_08_01.ASSOCIAR_CONDICAO_OBRIGATORIEDADE,
                           tokenEdit: UC_ALN_002_08_01.ASSOCIAR_CONDICAO_OBRIGATORIEDADE,
                           tokenRemove: UC_ALN_002_08_01.ASSOCIAR_CONDICAO_OBRIGATORIEDADE,
                           tokenList: UC_ALN_002_08_01.ASSOCIAR_CONDICAO_OBRIGATORIEDADE)
                   .Service<IPessoaAtuacaoCondicaoObrigatoriedadeService>(insert: nameof(IPessoaAtuacaoCondicaoObrigatoriedadeService.AlterarPessoaAtuacaoCondicaoObrigatoriedade),
                                                                          save: nameof(IPessoaAtuacaoCondicaoObrigatoriedadeService.SalvarPessoaAtuacaoCondicaoObrigatoriedade));
        }
    }
}