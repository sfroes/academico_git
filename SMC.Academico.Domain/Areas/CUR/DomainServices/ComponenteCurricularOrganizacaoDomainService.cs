using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Framework.Domain;
using SMC.Framework.Model;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CUR.DomainServices
{
    public class ComponenteCurricularOrganizacaoDomainService : AcademicoContextDomain<ComponenteCurricularOrganizacao>
    {
        /// <summary>
        /// Busca as organizações ativas de um componete curricular
        /// </summary>
        /// <param name="seqComponenteCurricular">Sequencial do componte curricular</param>
        /// <returns>Organizações ativas do componete curricular</returns>
        public List<SMCDatasourceItem> BuscarOrganizacoesComponenteCurricularSelect(long seqComponenteCurricular)
        {
            var spec = new ComponenteCurricularOrganizacaoFilterSpecification()
            {
                SeqComponenteCurricular = seqComponenteCurricular,
                Ativo = true
            };
            spec.SetOrderBy(o => o.Descricao);

            return this.SearchProjectionBySpecification(spec, p => new SMCDatasourceItem()
            {
                Seq = p.Seq,
                Descricao = p.Descricao
            }).ToList();
        }
    }
}