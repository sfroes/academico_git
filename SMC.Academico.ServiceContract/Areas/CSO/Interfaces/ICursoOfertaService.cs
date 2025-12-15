using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CSO.Interfaces
{
    public interface ICursoOfertaService : ISMCService
    {
        /// <summary>
        /// Busca cursos oferta de acordo com o sequencial
        /// </summary>
        /// <param name="seq">Sequencial de curso oferta</param>
        /// <returns>Objeto curso oferta</returns>
        CursoOfertaData BuscarCursoOferta(long seq);

        /// <summary>
        /// Busca os cursos oferta de acordo com os sequenciais
        /// </summary>
        /// <param name="seqs">Sequenciais</param>
        /// <returns>Cursos oferta</returns>
        List<CursoOfertaData> BuscarCursosOferta(long[] seqs);

        /// <summary>
        /// Buscar os cursos ofertas que foram cadastrados para os cursos de mesma hierarquia
        /// </summary>
        /// <param name="filtros">Filtros da listagem de cursos ofertas</param>
        /// <returns>SMCPagerData de cursos fertas</returns>
        SMCPagerData<CursoOfertaData> BuscarCursoOfertasLookup(CursoOfertaFiltroData filtros);

        /// <summary>
        /// Buscar as ofertas de curso ativas para o curso informado
        /// </summary>
        /// <param name="seqCurso">Sequencial do curso</param>
        /// <returns>Select item com os valores de ofertas para o curso</returns>
        List<SMCDatasourceItem> BuscarCursoOfertasAtivasSelect(long seqCurso);

        /// <summary>
        /// Buscar as ofertas de curso para o processo informado
        /// </summary>
        /// <param name="seqProcesso">Sequencial do processo</param>
        /// <returns>Select item com os valores de ofertas para o processo</returns>
        List<SMCDatasourceItem> BuscarCursosOfertasPorProcessoSelect(long seqProcesso);

        /// <summary>
        /// Verficia se já foram cadastrados tipos de curso para o nível de ensino do curso informado
        /// </summary>
        /// <param name="seqCurso">Sequencial do curso</param>
        /// <returns>Modelo de curso oferta caso os pré-requisitos sejam atendidos</returns>
        CursoOfertaData VerificarDependenciasCursoOferta(long seqCurso);

        /// <summary>
        /// Exclui uma oferta de curso
        /// </summary>
        /// <param name="seq">Sequencial da oferta de curso</param>
        void ExcluirCursoOferta(long seq);

        /// <summary>
        /// Recpera a máscara de nome de uma formação específica segundo a regra RN_CSO_023 - Mascara - Oferta de Curso
        /// </summary>
        /// <param name="seqFormacaoEspecifica">Sequencial da formação específica selecionada</param>
        /// <param name="seqCurso">Sequencial do curso selecionado</param>
        /// <returns>Máscara do curso oferta segundo a regra RN_CSO_023</returns>
        string RecuperarMascaraCursoOferta(long seqFormacaoEspecifica, long seqCurso);

        /// <summary>
        /// Buscar as ofertas de curso para o aluno de acordo com o histórico escolar
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação aluno</param>
        /// <returns>Select item com os valores de ofertas que posssuem histórico escolar</returns>
        List<SMCDatasourceItem> BuscarCursosOfertasPorAlunoHistoricoEscolarSelect(long seqPessoaAtuacao);

        /// <summary>
        /// Salvar o curso oferta
        /// </summary>
        /// <param name="modelo">Modelo de dados que será salvo na base</param>
        /// <returns>Sequencial do curso oferta salvo</returns>
        long SalvarCursoOferta(CursoOfertaData modelo);
    }
}