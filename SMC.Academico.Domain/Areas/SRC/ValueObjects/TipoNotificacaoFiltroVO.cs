using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class TipoNotificacaoFiltroVO : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }

        public string Descricao { get; set; }

        public bool? PermiteAgendamento { get; set; }
    }
}
