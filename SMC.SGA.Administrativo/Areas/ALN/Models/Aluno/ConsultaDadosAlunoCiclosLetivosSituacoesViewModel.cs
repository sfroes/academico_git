using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class ConsultaDadosAlunoCiclosLetivosSituacoesViewModel : SMCViewModelBase
    {
        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        public DateTime DataInicio { get; set; }

        public string Situacao { get; set; }

        [SMCValueEmpty("-")]
        public string NumeroProtocolo { get; set; }

        [SMCValueEmpty("-")]
        public string Observacao { get; set; }

        public string Inclusao { get; set; }

        [SMCValueEmpty("-")]
        public string NumeroProtocoloExclusao { get; set; }

        [SMCValueEmpty("-")]
        public string ObservacaoExclusao { get; set; }

        [SMCValueEmpty("-")]
        public string Exclusao { get; set; }

        public bool ExisteDataExclusao { get; set; }

        public bool EmDestaque { get; set; }

        public bool FlagVerDadosIntercambio { get; set; }

        public long SeqCicloLetivoSituacao { get; set; }

        public long? SeqSolicitacaoServico { get; set; }

        public long? SeqSolicitacaoServicoExclusao { get; set; }

        public long SeqPeriodoIntercambio { get; set; }
        public long? SeqArquivoAnexado { get; set; }

        public SMCUploadFile ArquivoAnexado { get; set; }
    }
}