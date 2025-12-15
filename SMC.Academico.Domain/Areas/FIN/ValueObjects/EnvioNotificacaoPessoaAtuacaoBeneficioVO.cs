using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.FIN.ValueObjects
{
    public class EnvioNotificacaoPessoaAtuacaoBeneficioVO : ISMCMappable
    {
        /// <summary>
        /// Sequencial da solicitação de serviço
        /// </summary>
        public long SeqPessoaAtuacaoBeneficio { get; set; }

        /// <summary>
        /// Token de identificação do tipo de notificação a ser enviado
        /// </summary>
        public string TokenTipoNotificacao { get; set; }

        /// <summary>
        /// Dados para merge da mensagem
        /// </summary>
        public Dictionary<string, string> DadosMerge { get; set; }
    }
}
