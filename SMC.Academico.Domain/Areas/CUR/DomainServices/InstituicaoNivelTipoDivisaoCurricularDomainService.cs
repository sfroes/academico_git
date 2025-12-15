using SMC.Academico.Common.Areas.CUR.Includes;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Framework.Domain;
using SMC.Framework.Model;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CUR.DomainServices
{
    public class InstituicaoNivelTipoDivisaoCurricularDomainService : AcademicoContextDomain<InstituicaoNivelTipoDivisaoCurricular>
    {

        /// <summary>
        /// Busca a lista de tipo de divisao curricular de acordo com o nível de ensino
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do nível de ensino</param>
        /// <returns>Lista de tipos de divisão curricular</returns>
        public List<SMCDatasourceItem> BuscarTiposDivisaoCurricularNivelEnsinoSelect(long seqNivelEnsino)
        {
            List<SMCDatasourceItem> listaRetorno = new List<SMCDatasourceItem>();

            InstituicaoNivelTipoDivisaoCurricularSpecification spec = new InstituicaoNivelTipoDivisaoCurricularSpecification() {
                seqNivelEnsino = seqNivelEnsino
            };

            this.SearchBySpecification(spec, IncludesInstituicaoNivelTipoDivisaoCurricular.TipoDivisaoCurricular).ToList().ForEach(item => 
            {
                TipoDivisaoCurricular tipoDivisaoCurricular = item.TipoDivisaoCurricular;

                listaRetorno.Add(new SMCDatasourceItem() {
                      Seq = tipoDivisaoCurricular.Seq
                    , Descricao = tipoDivisaoCurricular.Descricao

                });
            });

            return listaRetorno.OrderBy(o => o.Descricao).ToList();
        }


    }
}
