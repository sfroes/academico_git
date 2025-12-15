using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Framework.Model;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.PES.DomainServices
{
    public class TipoFuncionarioDomainService : AcademicoContextDomain<TipoFuncionario>
    {
        /// <summary>
        /// Buscar todos os tipos de funcionários para select
        /// </summary>
        /// <returns>Lista de todos os funcionários</returns>
        public List<SMCDatasourceItem> BuscarTiposFuncionarioSelect()
        {
            var listaTiposFuncionario = SearchAll().ToList();
            var retorno = new List<SMCDatasourceItem>();

            foreach (var item in listaTiposFuncionario)
            {
                retorno.Add(new SMCDatasourceItem() { Seq = item.Seq, Descricao = item.DescricaoMasculino });
            }

            return retorno.OrderBy(o => o.Descricao).ToList();
        }
    }
}
