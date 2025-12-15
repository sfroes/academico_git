using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System;

namespace SMC.Academico.Service.Areas.CUR.Services
{
    public class MatrizCurricularConfiguracaoComponenteService : SMCServiceBase, IMatrizCurricularConfiguracaoComponenteService
    {
        #region [ DomainService ]

        private DivisaoMatrizCurricularGrupoDomainService DivisaoMatrizCurricularGrupoDomainService
        {
            get { return this.Create<DivisaoMatrizCurricularGrupoDomainService>(); }
        }

        #endregion

        /// <summary>
        /// Buscar uma matriz curricular configuracao componente pelo sequencial 
        /// </summary>
        /// <param name="seq">Sequencia do matriz curricular configuracao componente a ser recuperada</param>
        /// <returns>Matriz curricular configuracao componente recuperado</returns>
        public MatrizCurricularConfiguracaoComponenteData BuscarMatrizCurricularConfiguracaoComponente(long seq)
        {
            return this.DivisaoMatrizCurricularGrupoDomainService
                .BuscarMatrizCurricularConfiguracaoComponente(seq)
                .Transform<MatrizCurricularConfiguracaoComponenteData>();
        }

        /// <summary>
        /// Buscar as matrizes curriculares configuracao componentes que atendam os filtros informados e com a paginação correta
        /// </summary>
        /// <param name="filtros">Filtros da listagem de matriz curricular configuracao componente</param>
        /// <returns>SMCPagerData com a lista de matriz curricular configuracao componente</returns>
        public SMCPagerData<MatrizCurricularConfiguracaoComponenteData> BuscarMatrizesCurricularesConfiguracoesComponente(MatrizCurricularConfiguracaoComponenteFiltroData filtros)
        {
            return this.DivisaoMatrizCurricularGrupoDomainService
                .BuscarMatrizesCurricularesConfiguracoesComponente(filtros.Transform<DivisaoMatrizCurricularGrupoFilterSpecification>())
                .Transform<SMCPagerData<MatrizCurricularConfiguracaoComponenteData>>();
        }

        /// <summary>
        /// Salva uma matriz curricular configuracao componente
        /// </summary>
        /// <param name="matrizCurricularConfiguracaoComponente">Dados da matriz curricular configuracao componente a gravada</param>
        /// <returns>Sequencial da matriz curricular configuracao componente gravada</returns>
        public long SalvarMatrizCurricularConfiguracaoComponente(MatrizCurricularConfiguracaoComponenteData matrizCurricularConfiguracaoComponente)
        {
            return this.DivisaoMatrizCurricularGrupoDomainService
                .SalvarMatrizCurricularConfiguracaoComponente(matrizCurricularConfiguracaoComponente.Transform<MatrizCurricularConfiguracaoComponenteVO>());
        }

        /// <summary>
        /// Exclui todas divisões matriz curriculares grupo de um curriculo curso oferta grupo
        /// </summary>
        /// <param name="seq">Sequencial do curriculo curso oferta grupo</param>
        public void ExcluirMatrizCurricularConfiguracaoComponente(long seq)
        {
            this.DivisaoMatrizCurricularGrupoDomainService.ExcluirMatrizCurricularConfiguracaoComponente(seq);
        }
    }
}
