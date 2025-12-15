using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Domain;
using SMC.Framework.Model;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CUR.DomainServices
{
    public class TipoDivisaoCurricularDomainService : AcademicoContextDomain<TipoDivisaoCurricular>
    {

        /// <summary>
        /// Busca o select de Tipo de divisao curricular
        /// </summary>
        /// <param name="seqInstituicaoNivel">SeqInstituicaoNivel</param>
        /// <returns></returns>
        public List<SMCDatasourceItem> BuscarTiposDivisaoCurricularSelect()
        {
            return this.SearchAll().OrderBy(o => o.Descricao).Select(s => new SMCDatasourceItem() { Seq = s.Seq, Descricao = s.Descricao }).ToList();
        }


    }
}
