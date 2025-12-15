using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class TipoNotificacaoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqTipoNotificacao { get; set; }

        public string Token { get; set; }

        public PermiteReenvio PermiteReenvio { get; set; }

        public bool PermiteAgendamento { get; set; }

        public string Descricao { get; set; }

        public string Observacao { get; set; }

        public List<TipoNotificacaoAtributoAgendamentoVO> AtributosAgendamento { get; set; }

        public long SeqAuxiliar { get; set; }

        public bool ModoEdicao { get; set; }
    }
}