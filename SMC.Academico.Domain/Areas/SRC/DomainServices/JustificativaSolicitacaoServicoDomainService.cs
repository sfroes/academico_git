using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Framework.Domain;
using SMC.Framework.Model;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.SRC.DomainServices
{
    public class JustificativaSolicitacaoServicoDomainService : AcademicoContextDomain<JustificativaSolicitacaoServico>
    {
        public List<SMCDatasourceItem> BuscarJustificativasSolicitacaoServicoSelect(JustificativaSolicitacaoServicoFilterSpecification filter)
        {
            filter.SetOrderBy(x => x.Descricao);
            return this.SearchProjectionBySpecification(filter, x => new SMCDatasourceItem
            {
                Seq = x.Seq,
                Descricao = x.Descricao
            }).ToList();
        }
    }
}