using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Interfaces
{
    public interface ICurriculoService : ISMCService
    {
        CurriculoData BuscarConfiguracoesCurriculo(long seqCurso);

        CurriculoData BuscarCurriculo(long seq);

        /// <summary>
        /// Busca todos Currículos com suas Ofertas de Cursos e Grupos Curriculares
        /// </summary>
        /// <param name="filtros">Filtros dos Currículos</param>
        /// <returns>Lista paginada dos Currículos com Ofertas de Cursos e Grupos Curriculares que atendam aos filtros informados</returns>
        SMCPagerData<CurriculoListaData> BuscarCurriculos(CurriculoFiltroData filtros);

        /// <summary>
        /// Busca a lista de currículos de acordo com o curso para popular um Select
        /// </summary>
        /// <param name="seqCurso">Sequencial do curso</param>
        /// <returns>Lista de curriculos</returns>
        List<SMCDatasourceItem> BuscarCurriculoPorCursoSelect(long seqCurso);

        /// <summary>
        /// Grava um currículo e seus vínculos com ofertas
        /// </summary>
        /// <param name="curriculo">Dados do currículo a ser gravado</param>
        /// <returns>Sequencial do currículo gravado</returns>
        long SalvarCurriculo(CurriculoData curriculo);

        /// <summary>
        /// Criar uma integração para gerar o currículo digital no formato XML
        /// </summary>
        /// <param name="filtro">Dados do agendamento e código do currículo</param>
        void ConstruirCurriculoDigital(EmissaoCurriculoDigitalSATData filtro);
    }
}