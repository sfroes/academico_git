using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Service.ORG.Data
{
    public class InstituicaoTipoEntidadeListaData : ISMCMappable
    {
        /// <summary>
        /// Sequencial da InstituicaoTipoEntidade
        /// </summary>
        public long Seq { get; set; }

        /// <summary>
        /// Sequencial do TipoEntidade associado
        /// </summary>
        [SMCMapProperty("TipoEntidade.Descricao")]
        public string DescricaoTipoEntidade { get; set; }
    }
}