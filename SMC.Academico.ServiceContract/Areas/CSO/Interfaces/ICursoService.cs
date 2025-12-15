using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CSO.Interfaces
{
    public interface ICursoService : ISMCService
    {
        /// <summary>
        /// Buscar um curso pelo sequencial
        /// </summary>
        /// <param name="seq">Sequencia do curso a ser recuperado</param>
        /// <returns>Curso recuperado</returns>
        CursoData BuscarCurso(long seq);

        /// <summary>
        /// Buscar um curso com as configurações do tipo entidade curso
        /// </summary>
        /// <param name="seq">Sequencia do curso a ser recuperado</param>
        /// <returns>Curso com as configurações do tipo de entidade curso</returns>
        CursoData BuscarCursoComConfiguracao(long seq);

        /// <summary>
        /// Buscar a configuração do curso para um novo cadastro
        /// </summary>
        /// <returns>A configuração da entidade externada curso</returns>
        EntidadeData BuscarConfiguracaoDoCurso();

        /// <summary>
        /// Buscar as possíveis entidades superiories na visão organizacional de um tipo de entidade curso
        /// para montagem de select
        /// </summary>
        /// <param name="apenasAtivas">Considerar todas as entidades com situação ativas ou em ativação</param>
        /// <param name="usarNomeReduzido">Considerar o nome reduzido da entidade</param>
        /// <param name="usarSeqEntidade">Define se será retornada o seq da entidade ou o seq da hierarquia item que representa esta entidade</param>
        /// <param name="considerarGrupoPrograma">Considerar o grupo de programa</param>
        /// <returns>Lista de possíveis entidades superiores do curso</returns>
        List<SMCDatasourceItem> BuscarHierarquiaSuperiorCursoSelect(bool apenasAtivas = false, bool usarNomeReduzido = false, bool usarSeqEntidade = false, bool considerarGrupoPrograma = false);

        /// <summary>
        /// Buscar as situações do tipo de entidade externada curso
        /// </summary>
        /// <returns>Lista de situações de um curso</returns>
        List<SMCDatasourceItem> BuscarSituacoesCursoSelect();

        /// <summary>
        /// Buscar os cursos que atendam os filtros informados e com a paginação correta
        /// </summary>
        /// <param name="filtros">Filtros da listagem de cursos</param>
        /// <returns>SMCPagerData com a lista de cursos</returns>
        SMCPagerData<CursoListaData> BuscarCursos(CursoFiltroData filtros);

        /// <summary>
        /// Buscar os cursos que atendam os filtros informados
        /// </summary>
        /// <param name="seqs">Sequencial do curso</param>
        /// <returns>Lista de cursos
        List<CursoListaData> BuscarCursosLookupSelect(long[] seqs);

        /// <summary>
        /// Recupera um curso pelo componente curricular
        /// </summary>
        /// <param name="seqGrupoCurricularComponente">Sequencial do componente curricular</param>
        /// <returns>Dados do curso</returns>
        CursoData BuscarCursoPorGrupoCurricularComponente(long seqGrupoCurricularComponente);

        /// <summary>
        /// Salva um curso com suas hierarquias de entidade
        /// </summary>
        /// <param name="curso">Dados do curso a ser salvo</param>
        /// <returns>Sequencial do curso salvo</returns>
        long SalvarCurso(CursoData curso);

        /// <summary>
        /// Recupera os cursos para replica da formação específica de programa
        /// </summary>
        /// <param name="filtros">Fintros para pesquina dos cursos</param>
        /// <returns>Lista de cursos encontrados</returns>
        List<SMCDatasourceItem> BuscarCursosReplicarFormacaoEspecificaProgramaSelect(CursoReplicaFormacaoEspecificaProgramaFiltroData filtros);
    }
}