using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class AjustePlanoEstudoVO : ISMCMappable
    {
        public long SeqPessoaAtuacao { get; set; }
        public long? SeqCicloLetivoReferencia { get; set; }
        public long? SeqSolicitacaoServico { get; set; }
        public DateTime? DataFimOrientacao { get; set; }
        public string Observacao { get; set; }

        public bool? Atual { get; set; }
    }
}