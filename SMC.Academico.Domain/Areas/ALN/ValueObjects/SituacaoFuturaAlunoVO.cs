using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class SituacaoFuturaAlunoVO : ISMCMappable
    {
        public long SeqAlunoHistoricoSituacao { get; set; }

        public string DescricaoSituacaoFutura { get; set; }

        public DateTime DataInicio { get; set; }

        public string ProtocoloSolicitacaoOrigem { get; set; }

        public string DescricaoProcesso { get; set; }

        public string TokenSituacaoFutura { get; set; }
    }
}