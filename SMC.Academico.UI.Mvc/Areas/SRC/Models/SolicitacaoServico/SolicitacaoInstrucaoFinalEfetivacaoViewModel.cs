using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class SolicitacaoInstrucaoFinalEfetivacaoViewModel : SolicitacaoServicoPaginaViewModelBase
    {

        [SMCHidden]
        public override string Token => TOKEN_SOLICITACAO_SERVICO.CONCLUSAO_MATRICULA;

        [SMCHidden]
        public long SeqIngressante { get; set; }

        [SMCHidden]
        public long SeqProcessoEtapa { get; set; }
    }
}
