using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class CicloLetivoSituacaoData : ISMCMappable
    {
        public DateTime DataInicio { get; set; }

        public string Situacao { get; set; }

        public string NumeroProtocolo { get; set; }

        public string Observacao { get; set; }

        public string Inclusao { get; set; }

        public string NumeroProtocoloExclusao { get; set; }

        public string ObservacaoExclusao { get; set; }

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