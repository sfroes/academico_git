using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class SolicitacaoHistoricoNavegacaoData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqSolicitacaoServicoEtapa { get; set; }

        public long SeqConfiguracaoEtapaPagina { get; set; }

        public DateTime DataEntrada { get; set; }

        public DateTime? DataSaida { get; set; }
    }
}