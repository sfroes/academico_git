using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Model;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ORG.DomainServices
{
    public class TipoAtoNormativoDomainService : AcademicoContextDomain<TipoAtoNormativo>
    {
        /// <summary>
        /// Buscar os tipos de atos normativos
        /// </summary>
        /// <param name="spec">Filtros</param>
        /// <returns>Tipos atos normativos select</returns>
        public List<SMCDatasourceItem> BuscarTiposAtoNormativoSelect(TipoAtoNormativoFilterSpecification spec)
        {
            return this.SearchProjectionBySpecification(spec, p => new SMCDatasourceItem()
            {
                Seq = p.Seq,
                Descricao = p.Descricao,

            }).OrderBy(o => o.Descricao).ToList();
        }

        /// <summary>
        /// Buscar tipo ato normativo
        /// </summary>
        /// <param name="seqTipoAtoNormativo">Sequencial tipo ato normativo</param>
        /// <returns>Tipo ato normativo</returns>
        public TipoAtoNormativo BuscarTipoAtoNormativo(long seqTipoAtoNormativo)
        {
            return this.SearchByKey(seqTipoAtoNormativo);
        }
    }
}