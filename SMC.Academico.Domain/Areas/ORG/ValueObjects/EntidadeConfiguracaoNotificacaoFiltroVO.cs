using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.Domain.Areas.ORG.ValueObjects
{
    public class EntidadeConfiguracaoNotificacaoFiltroVO : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqEntidade { get; set; }

        public long? SeqTipoNotificacao { get; set; }
    }
}
