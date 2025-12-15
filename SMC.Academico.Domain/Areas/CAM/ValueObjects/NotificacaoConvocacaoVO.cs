using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CAM.ValueObjects
{
    public class NotificacaoConvocacaoVO : ISMCMappable
    {
        public long SeqIngressante { get; set; }

        public long SeqSolicitacaoServico { get; set; }

        public string Nome { get; set; }

        public string NomeSocial { get; set; }

        public List<string> DescricaoOfertaCampanha { get; set; }

        public List<string> Emails { get; set; }

        public long SeqConfiguracaoTipoNotificacao { get; set; }

        public long SeqProcessoEtapaConfiguracaoNotificacao { get; set; }
    }
}