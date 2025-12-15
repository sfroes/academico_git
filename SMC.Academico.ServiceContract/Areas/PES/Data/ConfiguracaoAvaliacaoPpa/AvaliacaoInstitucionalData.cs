using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.PES.Data.ConfiguracaoAvaliacaoPpa
{
    public class AvaliacaoInstitucionalData
    {
        /// <summary>
        /// Codigo da Avaliacao
        /// </summary>
        public int CodigoAvaliacao { get; set; }
        /// <summary>
        /// Nome Avaliacao
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Data de inicio da avaliação
        /// </summary>
        public DateTime? DataInicio { get; set; }

        /// <summary>
        /// Data fim da avaliação
        /// </summary>
        public DateTime? DataFim { get; set; }
    }
}
