using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CSO.Models
{
    public partial class InstituicaoNivelModalidade
    {
        /// <summary>
        /// Recupera as descrições dos tipos de entidade localidade desta modalidade
        /// </summary>
        /// <returns>Descrições dos tipos de entidade localidade</returns>
        public List<string> RecuperarTiposEntidadeLocalidadeDescricao()
        {
            return this.TiposEntidadeLocalidade.OrderBy(o => o.TipoEntidadeLocalidade.Descricao).Select(s => s.TipoEntidadeLocalidade.Descricao).ToList();
        }
    }
}