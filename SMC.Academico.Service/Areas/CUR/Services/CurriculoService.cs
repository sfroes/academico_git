using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CUR.Services
{
    public class CurriculoService : SMCServiceBase, ICurriculoService
    {
        #region [ Serviços ]

        private CurriculoDomainService CurriculoDomainService
        {
            get { return this.Create<CurriculoDomainService>(); }
        }

        #endregion [ Serviços ]

        public CurriculoData BuscarConfiguracoesCurriculo(long seqCurso)
        {
            return this.CurriculoDomainService.BuscarConfiguracoesCurriculo(seqCurso).Transform<CurriculoData>();
        }

        public CurriculoData BuscarCurriculo(long seq)
        {            
            return this.CurriculoDomainService.BuscarCurriculo(seq).Transform<CurriculoData>();
        }

        /// <summary>
        /// Busca todos Currículos com suas Ofertas de Cursos e Grupos Curriculares
        /// </summary>
        /// <param name="filtros">Filtros dos Currículos</param>
        /// <returns>Lista paginada dos Currículos com Ofertas de Cursos e Grupos Curriculares que atendam aos filtros informados</returns>
        public SMCPagerData<CurriculoListaData> BuscarCurriculos(CurriculoFiltroData filtros)
        {
            return this.CurriculoDomainService
                .BuscarCurriculos(filtros.Transform<CurriculoFilterSpecification>())
                .Transform<SMCPagerData<CurriculoListaData>>();
        }

        /// <summary>
        /// Busca a lista de currículos de acordo com o curso para popular um Select
        /// </summary>
        /// <param name="seqCurso">Sequencial do curso</param>
        /// <returns>Lista de curriculos</returns>
        public List<SMCDatasourceItem> BuscarCurriculoPorCursoSelect(long seqCurso)
        {
            return this.CurriculoDomainService.BuscarCurriculoPorCursoSelect(seqCurso);
        }

        /// <summary>
        /// Grava um currículo e seus vínculos com ofertas
        /// </summary>
        /// <param name="curriculo">Dados do currículo a ser gravado</param>
        /// <returns>Sequencial do currículo gravado</returns>
        public long SalvarCurriculo(CurriculoData curriculo)
        {
            var curriculoDominio = curriculo.Transform<Curriculo>();
            return this.CurriculoDomainService.SalvarCurriculo(curriculoDominio);
        }

        public void ConstruirCurriculoDigital(EmissaoCurriculoDigitalSATData filtro)
        {
            CurriculoDomainService.ConstruirCurriculoDigital(filtro.Transform<EmissaoCurriculoDigitalSATVO>());
        }

    }
}