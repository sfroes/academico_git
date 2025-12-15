using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class TipoNotificacaoListarVO : ISMCMappable
    {
        public long? Seq { get; set; }

        public string Descricao { get; set; }

        public bool? PermiteAgendamento { get; set; }
    }
}
