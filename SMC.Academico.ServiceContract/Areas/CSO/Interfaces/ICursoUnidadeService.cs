using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CSO.Interfaces
{
    public interface ICursoUnidadeService : ISMCService
    {
        /// <summary>
        /// Busca as configurações de um CursoUnidade
        /// </summary>
        /// <returns>Dados das configurações de um CursoUnidade</returns>
        EntidadeData BuscarConfiguracoesCursoUnidade();

        /// <summary>
        /// Busca as possíveis entidades superiores de Curso Unidade na visão Unidade
        /// </summary>
        /// <returns>SelectItem dos HierarquiaItem que representam as entidades encontradas</returns>
        List<SMCDatasourceItem> BuscarUnidadesSelect();

        /// <summary>
        /// Busca as possíveis entidades superiores de Curso Unidade na visão Unidade, e não tenha entidades filhas que também possa ser entidade-pai do tipo CURSO_UNIDADE
        /// </summary>
        /// <returns>SelectItem dos HierarquiaItem que representam as entidades encontradas</returns>
        List<SMCDatasourceItem> BuscarUnidadesSemEntidadePaiSelect(bool removeEntidadePai = false);

        /// <summary>
        /// Recuperar os CursoUnidade com seus níveis de ensino, ofertas e turnos
        /// </summary>
        /// <param name="filtros">Filtros para os CursoUnidade</param>
        /// <returns>Lista páginada com os dados dos CursoUnidade com seus níveis de ensino, ofertas e turnos</returns>
        SMCPagerData<CursoUnidadeListaData> BuscarCursosUnidade(CursoUnidadeFiltroData filtros);

        /// <summary>
        /// Recupera um CursoUnidade com suas dependências e configurações
        /// </summary>
        /// <param name="seq">Sequencial do CursoUnidade a ser recuperado</param>
        /// <param name="desativarFiltroHierarquia">Flag para desativar filtro de dados</param>
        /// <returns>Dados do CursoUnidade, dependências e configurações</returns>
        CursoUnidadeData BuscarCursoUnidade(long seq, bool desativarFiltroHierarquia = false);

        /// <summary>
        /// Recupera um CursoUnidade com suas dependências e configurações com o filtro de dados desativado
        /// </summary>
        /// <param name="seq">Sequencial do CursoUnidade a ser recuperado</param>
        /// <returns>Dados do CursoUnidade, dependências e configurações</returns>
        CursoUnidadeData BuscarCursoUnidadeFiltroDesativado(long seq);

        /// <summary>
        /// Recupera a mascara de curso unidade segundo a regra RN_CSO_026
        /// </summary>
        /// <param name="seqCurso">Sequencial do curso</param>
        /// <param name="seqUnidade">Sequencial do item de hierarquia da unidade responsável</param>
        /// <returns>Mascara segundo a regra RN_CSO_026</returns>
        string RecuperarMascaraCursoUnidade(long seqCurso, long seqUnidade);

        /// <summary>
        /// Grava um CursoUnidade e suas dependências
        /// </summary>
        /// <param name="cursoUnidadeVo">Dados do CursoUnidade</param>
        /// <returns>Sequencial do CursoUnidade gravado</returns>
        long SalvarCursoUnidade(CursoUnidadeData cursoUnidade);
    }
}
