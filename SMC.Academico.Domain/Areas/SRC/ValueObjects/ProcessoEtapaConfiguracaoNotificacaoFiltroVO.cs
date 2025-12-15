using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class ProcessoEtapaConfiguracaoNotificacaoFiltroVO : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqProcesso { get; set; }

        public long? SeqProcessoUnidadeResponsavel { get; set; }

        public long? SeqTipoNotificacao { get; set; }

        public bool? PermiteAgendamento { get; set; }

        public bool ExigeEscalonamento { get; set; }

        public List<long> SeqsGrupoEscalonamento { get; set; }
    }
}
