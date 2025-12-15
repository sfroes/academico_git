using SMC.Academico.Domain.Areas.DCT.Models;
using SMC.Framework.Domain;
using SMC.Framework.Model;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.DCT.DomainServices
{
    public class CategoriaInstituicaoEnsinoDomainService : AcademicoContextDomain<CategoriaInstituicaoEnsino>
    {
        /// <summary>
        /// Busca os dados de todas as categorias de instituição de ensino cadastradas
        /// </summary>
        /// <returns>Lista de SMCDatasourceItem com o sequencial e descrição das categorias</returns>
        public List<SMCDatasourceItem> BuscarCategoriasInstituicaoEnsinoSelect()
        {
            return this.SearchProjectionAll(p => new SMCDatasourceItem() { Seq = p.Seq, Descricao = p.Descricao }, o => o.Descricao).ToList();
        }
    }
}