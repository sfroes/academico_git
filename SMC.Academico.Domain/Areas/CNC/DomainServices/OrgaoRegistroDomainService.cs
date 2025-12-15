using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Framework.Model;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CNC.DomainServices
{
    public class OrgaoRegistroDomainService : AcademicoContextDomain<OrgaoRegistro>
    {
        public List<SMCDatasourceItem> BuscarOrgaosRegistroSelect()
        {
            List<SMCDatasourceItem> retorno = SearchProjectionAll(x => new SMCDatasourceItem()
            {
                Seq = x.Seq,
                Descricao = x.Descricao
            }, i => i.Descricao).ToList();

            return retorno;
        }

    }
}
