using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class SolicitacaoDispensaSituacaoFuturaAlunoData : ISMCMappable
    {
        public string DescricaoSituacaoFutura { get; set; }

        public DateTime DataInicio { get; set; }

        public string ProtocoloSolicitacaoOrigem { get; set; }

        public string DescricaoProcesso { get; set; }
    }
}