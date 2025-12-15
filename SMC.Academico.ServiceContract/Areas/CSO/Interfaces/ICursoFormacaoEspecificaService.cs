using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CSO.Interfaces
{
    public interface ICursoFormacaoEspecificaService : ISMCService
    {
        /// <summary>
        /// Buscar a lista de formações específicas relacionadas ao curso para popular um Select
        /// </summary>
        /// <param name="seqCurso">Sequencial do curso</param>
        /// <returns>Lista de formação especifica</returns>
        List<SMCDatasourceItem> BuscarCursoFormacaoEspecificaSelect(long seqCurso);

        /// <summary>
        /// Buscar a lista de formações específicas das entidades superior ao curso para popular um Select
        /// </summary>
        /// <param name="seqCurso">Sequencial do curso</param>
        /// <returns>Lista de formação especifica</returns>
        List<SMCDatasourceItem> BuscarTipoFormacaoEspecificaResponsavelSelect(long seqCurso);

        /// <summary>
        /// Faz a configuração para inclusão de um novo CursoFormacaoEspecifica
        /// </summary>
        /// <returns>Dados da inclusão de formação especifíca</returns>
        CursoFormacaoEspecificaData ConfigurarCursoFormacaoEspecifica(long seqCurso);

        /// <summary>
        /// Faz a busca dos CursoFormacaoEspecifica cadastrados para um curso
        /// </summary>
        /// <param name="seqCurso">Sequencial do curso</param>
        /// <returns>Retorna a lista de CursoFormacaoEspecifica cadastrados com suas superiores</returns>
        List<CursoFormacaoEspecificaNodeData> BuscarCursoFormacoesEspecificas(CursoFormacaoEspecificaFiltroData filtro);

        /// <summary>
        /// Retorna um CursoFormacaoEspecifica para edição
        /// </summary>
        /// <param name="seq">Sequencial do CursoFormacaoEspecifica</param>
        /// <returns>CursoFormacaoEspecifica para edição</returns>
        CursoFormacaoEspecificaData BuscarCursoFormacaoEspecifica(long seq);

        /// <summary>
        /// Salvar a relação curso formação específica de acordo com o tipo de formação utilizado
        /// Não retorna o seq é uma gravação mestre detalhe
        /// <param name="formacao">Objeto com os dados da formação específica de acordo com o tipo</param>
        /// </summary>
        long SalvarCursoFormacaoEspecifica(CursoFormacaoEspecificaData formacao);

        /// <summary>
        /// Exclui uma formação específica de curso
        /// </summary>
        /// <param name="seq">Sequencial do curso formação específica</param>
        void ExcluirCursoFormacaoEspecifica(long seq);

        /// <summary>
        /// Verificar se existe pelo menos uma Formação Especifica para o Curso Selecionado
        /// <param name="seqCurso">Sequencial do curso</param>
        /// <returns>True or false</returns>
        /// </summary>
        bool VerificarExisteCursoFormacaoEspecifica(long seqCurso);

        /// <summary>
        /// Busa a descrição da formação específica segundo a regra RN_CSO_024 - Mascara - Formação Específica
        /// </summary>
        /// <param name="seqTipoFormacaoEspecifica">Sequencial do tipo da formação específica</param>
        /// <param name="seqGrauAcademico">Sequencial do grau da formação específica</param>
        /// <returns>Descrição da formação específica segundo a regra RN_CSO_024</returns>
        string BuscarDescricaoFormacaoEspecifica(long? seqTipoFormacaoEspecifica, long? seqGrauAcademico);

        /// <summary>
        /// Salva a replicação da formação específica do curso
        /// </summary>
        /// <param name="modelo">Dados para serem persistidos</param>
        void SalvarReplicarCursoFormacaoEspecifica(ReplicarCursoFormacaoEspecificaData modelo);

        /// <summary>
        /// Verifica se a formação específica possui grau acadêmico
        /// </summary>
        /// <param name="seqCurso">Sequencial do curso</param>
        /// <param name="seqFormacaoEspecifica">Sequencial da formação específica</param>
        /// <returns>Retorna se a formacao especifica possui grau acadêmico</returns>
        bool CursoFormacaoEspefificaPossuiGrau(long? seqCurso, long? seqFormacaoEspecifica);
    }
}