using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Framework.Domain;
using SMC.Framework.Model;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ORG.DomainServices
{
    public class AssuntoNormativoDomainService : AcademicoContextDomain<AssuntoNormativo>
    {
        /// <summary>
        /// Buscar os assuntos normativos
        /// </summary>
        /// <param name="spec">Filtros</param>
        /// <returns>Assuntos normativos select</returns>
        public List<SMCDatasourceItem> BuscarAssuntosNormativoSelect(AssuntoNormativoFilterSpecification spec)
        {
            return this.SearchProjectionBySpecification(spec, p => new SMCDatasourceItem()
            {
                Seq = p.Seq,
                Descricao = p.Descricao,

            }).OrderBy(o => o.Descricao).ToList();
        }

        /// <summary>
        /// Buscar assunto normativo
        /// </summary>
        /// <param name="seqAssuntoNormativo">Sequencial ato normativo</param>
        /// <returns>Assunto normativo</returns>
        public AssuntoNormativo BuscarAssuntoNormativo(long seqAssuntoNormativo)
        {
            return this.SearchByKey(seqAssuntoNormativo);
        }
    }
}