using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.DomainServices
{
    public class ClassificacaoPeriodicoDomainService : AcademicoContextDomain<ClassificacaoPeriodico>
    {
        public List<SMCDatasourceItem> BuscarClassificacaoPeriodicoSelect()
        {
            var itens = this.SearchAll(s => s.Descricao);
            List<SMCDatasourceItem> retorno = new List<SMCDatasourceItem>();

            foreach (var item in itens)
            {
                retorno.Add(new SMCDatasourceItem(item.Seq, item.Descricao));
            }

            return retorno;
        }

        public ClassificacaoPeriodicoVO BuscarClassificacaoPeriodicoAtual()
        {
            var spec = new ClassificacaoPeriodicoFilterSpecification() { Atual = true };
            return this.SearchByKey(spec).Transform<ClassificacaoPeriodicoVO>();
        }
    }
}