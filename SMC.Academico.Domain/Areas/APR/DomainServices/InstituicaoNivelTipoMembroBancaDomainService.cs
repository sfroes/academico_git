using System;
using System.Collections.Generic;
using System.Linq;
using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Academico.Domain.Areas.APR.Specifications;
using SMC.Framework.Domain;
using SMC.Framework.Model;
using SMC.Framework.Util;

namespace SMC.Academico.Domain.Areas.APR.DomainServices
{
    public class InstituicaoNivelTipoMembroBancaDomainService : AcademicoContextDomain<InstituicaoNivelTipoMembroBanca>
    {
        public List<SMCDatasourceItem> BuscarTiposMembroBancaSelect(InstituicaoNivelTipoMembroBancaFilterSpecification filtroSpec)
        {
            return SearchProjectionBySpecification(filtroSpec, p => p.TipoMembroBanca, true)
                .Select(s => new SMCDatasourceItem()
                {

                    Seq = (long)s,
                    Descricao = SMCEnumHelper.GetDescription(s)

                }).OrderBy(o => o.Descricao).ToList();

        }
    }
}