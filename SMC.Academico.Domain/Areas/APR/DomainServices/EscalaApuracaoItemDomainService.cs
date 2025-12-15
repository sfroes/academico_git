using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Academico.Domain.Areas.APR.Specifications;
using SMC.Framework.Domain;
using SMC.Framework.Model;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.APR.DomainServices
{
    public class EscalaApuracaoItemDomainService : AcademicoContextDomain<EscalaApuracaoItem>
    {
        /// <summary>
        /// Busca os itens de uma escala de apuração
        /// </summary>
        /// <param name="filtro">Dados de filtro</param>
        /// <returns>Dados dos itens da escala de apuração</returns>
        public List<SMCDatasourceItem> BuscarEscalaApuracaoItensSelect(EscalaApuracaoItemFilterSpecification filtro)
        {
            filtro.SetOrderBy(o => o.Descricao);
            return SearchProjectionBySpecification(filtro, p => new SMCDatasourceItem()
            {
                Seq = p.Seq,
                Descricao = p.Descricao,
                DataAttributes = new List<SMCKeyValuePair>()
                {
                    new SMCKeyValuePair() { Key = "aprovado", Value = p.Aprovado.ToString()}
                }
            }).ToList();
        }
    }
}