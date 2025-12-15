using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class NotificacaoSolicitacaoFiltroVO : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqSolicitacaoServico { get; set; }

        public List<long> SeqsNotificacoesEmailDestinatario { get; set; }
    }
}