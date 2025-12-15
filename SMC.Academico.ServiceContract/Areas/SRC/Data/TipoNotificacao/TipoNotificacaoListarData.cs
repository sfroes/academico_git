using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class TipoNotificacaoListarData : ISMCMappable
    {
        public long? Seq { get; set; }

        public string Descricao { get; set; }

        public bool? PermiteAgendamento { get; set; }
    }
}
