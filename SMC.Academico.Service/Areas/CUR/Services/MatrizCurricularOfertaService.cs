using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CUR.Services
{
    public class MatrizCurricularOfertaService : SMCServiceBase, IMatrizCurricularOfertaService
    {
        #region [ DomainService ]

        private MatrizCurricularOfertaDomainService MatrizCurricularOfertaDomainService
        {
            get { return this.Create<MatrizCurricularOfertaDomainService>(); }
        }

        #endregion [ DomainService ]

        /// <summary>
        /// Busca as matrizes que atendam os filtros informados
        /// </summary>
        /// <param name="filtros">Dados do filtro</param>
        /// <returns>KeyValue das matrizes com a chave e a descrição no formato
        /// [RN_CUR_016 - Exibição de descrição de oferta de matriz curricular] + [Situação atual da oferta de matriz]</returns>
        public List<SMCDatasourceItem> BuscarMatrizesCurricularesOfertasPorCampanhaSelect(MatrizCurricularOfertaFiltroData filtros)
        {
            return this.MatrizCurricularOfertaDomainService.BuscarMatrizesCurricularesOfertasPorCampanhaSelect(filtros.Transform<MatrizCurricularOfertaFiltroVO>());
        }

        /// <summary>
        /// Busca matriz curricular oferta
        /// </summary>
        /// <param name="seq">Sequencial da matriz curricular oferta</param>
        /// <returns>Matriz curricular oferta</returns>
        public MatrizCurricularOfertaData BuscarMatrizCurricularOferta(long seq)
        {
            return this.MatrizCurricularOfertaDomainService.BuscarMatrizCurricularOferta(seq).Transform<MatrizCurricularOfertaData>();
        }

        public List<MatrizCurricularOfertaData> BuscarMatrizesCurricularesOfertas(MatrizCurricularOfertaFiltroData filtro)
        {
            return this.MatrizCurricularOfertaDomainService.BuscarMatrizesCurricularesOfertas(filtro.Transform<MatrizCurricularOfertaFiltroVO>()).TransformList<MatrizCurricularOfertaData>();
        }

        /// <summary>
        /// Método que valida se alguma das matrizes de ofertas possuem vínculo com a hierarquia de entidade do usuário.
        /// </summary>
        /// <param name="seqs">Lista de seqs de MatrizCurricularOferta</param>
        /// <returns>True = Alguma das ofertas pode ser acessada pelo usuário | False = Nenhuma oferta pode ser acessada pelo usuário</returns>
        public bool ValidarMatrizCurricularOfertas(List<long> seqs)
        {
            return this.MatrizCurricularOfertaDomainService.ValidarMatrizCurricularOfertas(seqs);
        }

        /// <summary>
        /// Busca as matrizes curricular Ofertas que atendam os filtros informados e com a paginação correta
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns>SMCPagerData com a lista de matrizes curricular Ofertas</returns>
        public SMCPagerData<MatrizCurricularOfertaData> BuscarMatrizesCurricularLookupOferta(MatrizCurricularOfertaFiltroData filtros)
        {
            var matrizesCurricular = MatrizCurricularOfertaDomainService.BuscarMatrizesCurricularLookupOferta(filtros.Transform<MatrizCurricularOfertaFiltroVO>());

            return matrizesCurricular.Transform<SMCPagerData<MatrizCurricularOfertaData>>();
        }

        /// <summary>
        /// Busca a matrize curricular com a oferta selecionada para retorno do lookup
        /// </summary>
        /// <param name="SeqMatrizCurricularOferta">Sequencial da matriz curricular oferta</param>
        /// <returns>Objeto matriz curricular oferta</returns>
        public MatrizCurricularOfertaData BuscarMatrizesCurricularLookupOfertaSelecionado(long seqMatrizCurricularOferta)
        {
            var matrizesCurricular = MatrizCurricularOfertaDomainService.BuscarMatrizesCurricularLookupOfertaSelecionado(seqMatrizCurricularOferta);

            return matrizesCurricular.Transform<MatrizCurricularOfertaData>();
        }

        /// <summary>
        /// Valida se a matriz curricular oferta possui umas associação com a configuração de componente da turma
        /// </summary>
        /// <param name="seqMatrizCurricularOferta">Sequencial da matriz curricular oferta</param>
        /// <param name="seqConfiguracaoComponente">Sequencial da configuração de componente</param>
        /// <returns>True: Se a oferta estiver associada a configuração de componente, caso contrário False</returns>
        public bool ValidarMatrizCurricularOfertaConfiguracaoComponente(long seqMatrizCurricularOferta, long seqConfiguracaoComponente)
        {
            return MatrizCurricularOfertaDomainService.ValidarMatrizCurricularOfertaConfiguracaoComponente(seqMatrizCurricularOferta, seqConfiguracaoComponente);
        }
    }
}