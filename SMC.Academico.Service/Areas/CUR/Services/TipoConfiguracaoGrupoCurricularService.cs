using SMC.Academico.Common.Areas.CUR.Includes;
using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CUR.Services
{
    public class TipoConfiguracaoGrupoCurricularService : SMCServiceBase, ITipoConfiguracaoGrupoCurricularService
    {
        #region DomainService

        private TipoConfiguracaoGrupoCurricularDomainService TipoConfiguracaoGrupoCurricularDomainService
        {
            get { return this.Create<TipoConfiguracaoGrupoCurricularDomainService>(); }
        }

        #endregion

        /// <summary>
        /// Salva o registro na base
        /// </summary>
        /// <param name="tipoConfiguracaoGrupoCurricularData">Dados do registro a ser gravado</param>
        /// <returns>Sequencial do registro gravado</returns>
        public long SalvarTipoConfiguracaoGrupoCurricular(TipoConfiguracaoGrupoCurricularData tipoConfiguracaoGrupoCurricularData)
        {
            return TipoConfiguracaoGrupoCurricularDomainService.SalvarTipoConfiguracaoGrupoCurricular(tipoConfiguracaoGrupoCurricularData.Transform<TipoConfiguracaoGrupoCurricularVO>());
        }

        /// <summary>
        /// Recupera um tipo de configuração pelo seu sequencial
        /// </summary>
        /// <param name="seq">Sequencial do tipo de configuração</param>
        /// <returns>Dados do tipo de configuração</returns>
        public TipoConfiguracaoGrupoCurricularData BuscarTipoConfiguracaoGrupoCurricular(long seq)
        {
            return this.TipoConfiguracaoGrupoCurricularDomainService
                .SearchByKey(new SMCSeqSpecification<TipoConfiguracaoGrupoCurricular>(seq))
                .Transform<TipoConfiguracaoGrupoCurricularData>();
        }

        /// <summary>
        /// Exclui o registro de acordo com a seq fornecida
        /// </summary>
        /// <param name="seq">Sequencial do registro a ser excluído</param>
        public void ExcluirTipoConfiguracaoGrupoCurricular(long seq)
        {
            TipoConfiguracaoGrupoCurricular tipoConfiguracaoGrupoCurricular = TipoConfiguracaoGrupoCurricularDomainService
                                                                              .SearchByKey<TipoConfiguracaoGrupoCurricular, TipoConfiguracaoGrupoCurricular>(seq, IncludesTipoConfiguracaoGrupoCurricular.TiposConfiguracoesGrupoCurricularFilhos);
            TipoConfiguracaoGrupoCurricularDomainService.ExcluirTipoConfiguracaoGrupoCurricular(tipoConfiguracaoGrupoCurricular);
        }

        /// <summary>
        /// Lista todos os grupos curriculares filhos do grupo curricular informado
        /// </summary>
        /// <param name="seqTipoConfiguracaoGrupoCurricularSuperior">Sequencial do grupo curricular superior, caso não seja informado serão listados grupos raiz</param>
        /// <returns>Grupos cadastrados como subgrupo do grupo informado ou grupos marcados como raiz caso o grupo superior não seja informado</returns>
        public List<SMCDatasourceItem> BuscarTiposConfiguracaoGrupoCurricularSelect(long? seqTipoConfiguracaoGrupoCurricularSuperior)
        {
            return this.TipoConfiguracaoGrupoCurricularDomainService.BuscarTiposConfiguracaoGrupoCurricularSelect(seqTipoConfiguracaoGrupoCurricularSuperior);
        }
    }
}
