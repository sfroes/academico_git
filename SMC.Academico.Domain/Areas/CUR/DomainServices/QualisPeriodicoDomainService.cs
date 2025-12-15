using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Domain;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.CUR.DomainServices
{
    public class QualisPeriodicoDomainService : AcademicoContextDomain<QualisPeriodico>
    {
        public List<SMCDatasourceItem> BuscarAreaAvaliacaoSelect()
        {
            var itens = this.SearchAll(s => s.DescricaoAreaAvaliacao);

            List<SMCDatasourceItem> retorno = new List<SMCDatasourceItem>();

            foreach (var item in itens)
            {
                retorno.Add(new SMCDatasourceItem(item.Seq, item.DescricaoAreaAvaliacao));
            }

            return retorno;

        }
    }
}
