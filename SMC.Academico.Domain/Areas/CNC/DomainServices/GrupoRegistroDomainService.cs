using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Academico.Domain.Areas.CNC.Specifications;
using SMC.Academico.Domain.Areas.CNC.ValueObjects;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CNC.DomainServices
{
    public class GrupoRegistroDomainService : AcademicoContextDomain<GrupoRegistro>
    {
        /// <summary>
        /// Busca as grupors de registro que atendam os filtros informados
        /// </summary>
        /// <param name="filtros">Filtros da listagem dos grupos de registro</param>
        /// <returns>Lista de grupos de registros</returns>
        public SMCPagerData<GrupoRegistroVO> BuscarGruposRegistros(GrupoRegistroFiltroVO filtros)
        {
            GrupoRegistroFilterSpecification spec = filtros.Transform<GrupoRegistroFilterSpecification>();

            List<GrupoRegistroVO> gruposRegistro = this.SearchProjectionBySpecification(spec, p => new GrupoRegistroVO()
            {
                Seq = p.Seq,
                Descricao = p.Descricao,
                NumeroUltimoRegistro = p.NumeroUltimoRegistro,
                Prefixo = p.Prefixo,
                SeqInstituicaoEnsino = p.SeqInstituicaoEnsino

            }, out int total).ToList();

            /* NV02
            Se o grupo de registro possui parametrização de prefixo, exibir no seguinte formato: · [Prefixo][Último registro], por exemplo, SFE1078
            Senão, exibir[Último registro] */
            gruposRegistro.ForEach(grupoRegistro =>
            {
                if (!string.IsNullOrEmpty(grupoRegistro.Prefixo))
                {
                    grupoRegistro.PrefixoNumeroUltimoRegistro = $"{grupoRegistro.Prefixo}{grupoRegistro.NumeroUltimoRegistro}";
                }
                else
                {
                    grupoRegistro.PrefixoNumeroUltimoRegistro = grupoRegistro.NumeroUltimoRegistro.ToString();
                }
            });

            return new SMCPagerData<GrupoRegistroVO>(gruposRegistro, total);
        }

        /// <summary>
        /// Busca a lista de grupos de registros da instituição de ensino logada para popular um Select
        /// </summary>
        /// <returns>Lista de grupos de registros/returns>
        public List<SMCDatasourceItem> BuscarGruposRegistroSelect()
        {
            List<SMCDatasourceItem> retorno = SearchProjectionAll(x => new SMCDatasourceItem()
            {
                Seq = x.Seq,
                Descricao = x.Descricao
            },i => i.Descricao).ToList();

            return retorno;
        }
    }
}
