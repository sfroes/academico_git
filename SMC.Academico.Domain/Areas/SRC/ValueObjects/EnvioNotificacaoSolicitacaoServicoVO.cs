using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class EnvioNotificacaoSolicitacaoServicoVO : ISMCMappable
    {
        /// <summary>
        /// Sequencial da solicitação de serviço
        /// </summary>
        public long SeqSolicitacaoServico { get; set; }

        /// <summary>
        /// Token de identificação do tipo de notificação a ser enviado
        /// </summary>
        public string TokenNotificacao { get; set; }

        /// <summary>
        /// Dados para merge da mensagem
        /// </summary>
        public Dictionary<string, string> DadosMerge { get; set; }

        /// <summary>
        /// Flag para informar se deve enviar a notificação para o solicitante da solicitação. Por default envia.
        /// </summary>
        public bool EnvioSolicitante { get; set; }

        /// <summary>
        /// Flag para trazer sempre a configuração de notificação da primeiroa etapa. Por default traz da etapa atual.
        /// </summary>
        public bool ConfiguracaoPrimeiraEtapa { get; set; }

        /// <summary>
        /// Lista de destinatários da solicitação
        /// </summary>
        public List<string> Destinatarios { get; set; }
    }
}