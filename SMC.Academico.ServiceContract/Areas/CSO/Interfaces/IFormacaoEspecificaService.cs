using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CSO.Interfaces
{
    public interface IFormacaoEspecificaService : ISMCService
    {
        /// <summary>
        /// Buscar as formações específicas de uma entidade
        /// </summary>
        /// <param name="seqEntidadeResponsavel">Sequencia da entidade que têm as formações específicas</param>
        /// <returns>Array com dados dos nós das formações específicas com marcação das folhas</returns>
        FormacaoEspecificaNodeData[] BuscarFormacoesEspecificas(long seqEntidadeResponsavel);

        /// <summary>
        /// Buscar as formações específicas de uma entidade para o lookup
        /// </summary>
        /// <param name="formacaoEspecificaFiltro">Sequencia da entidade que têm as formações específicas</param>
        /// <returns>Array com dados dos nós das formações específicas com marcação das folhas</returns>
        FormacaoEspecificaNodeData[] BuscarFormacoesEspecificasLookup(FormacaoEspecificaFiltroData formacaoEspecificaFiltro);

        /// <summary>
        /// Buscar as formações epecíficas selecionadas numa entidade
        /// </summary>
        /// <param name="seqFormacoesEspecificas">seq das formações específias seleconadas</param>
        /// <returns>Array com hierarquias das formações específicas informadas</returns>
        FormacaoEspecificaHierarquiaData[] BuscarFormacoesEspecificasGridLookup(long[] seqFormacoesEspecificas);

        /// <summary>
        /// Recupera as formações específicas do tipo "linha de pesquisa" associadas aos programas filhos de um grupo de programas
        /// </summary>
        /// <param name="seqGrupoPrograma">Sequencial do grupo de progrmas</param>
        /// <returns>Dados das formações específias que atendam aos critérios</returns>
        List<SMCDatasourceItem> BuscarLinhasDePesquisaGrupoPrograma(long seqGrupoPrograma);

        /// <summary>
        /// Recupera uma formação específica pelo sequencial
        /// </summary>
        /// <param name="seq">Sequencial da formação específica</param>
        /// <returns>Dados da formação específica</returns>
        FormacaoEspecificaListaData BuscarFormacaoEspecifica(long seq);

        /// <summary>
        /// Verifica se a formação específica exige grau acadêmico, baseado no tipo formação específica
        /// </summary>
        /// <param name="filtro">Filtro a ser cosiderado</param>
        /// <returns>Retorna se a formacao especifica exige grau acadêmico</returns>
        bool FormacaoEspefificaExigeGrau(long seqFormacaoEspecifica);

        /// <summary>
        /// Verifica se o lookup de formação específica pode ou não apareceer no filtro da tela de
        /// consulta de condidatos de campanha
        /// </summary>
        /// <param name="seqTipoOferta">Seq do tipo de oferta selecionado</param>
        /// <param name="seqsEntidadesResponsaveis">Seqs das entidades responsáveis selecionados </param>
        /// <param name="seqCursoOferta">Seq do curso oferta selecionado</param>
        /// <returns>permissão para exibir o lookup de formação específica</returns>
        bool BloquearCampoFormacaoEspecifica(long? seqTipoOferta, long? seqCursoOferta, List<long> seqsEntidadesResponsaveis);

        /// <summary>
        /// Salva a uma formação específica
        /// </summary>
        /// <param name="model">Dados da formação específica para serem gravados</param>
        /// <returns>Sequencial da formação específica gravada</returns>
        long SalvarFormacaoEspecifica(FormacaoEspecificaData model);


        /// <summary>
        /// Buscar as formações específicas pelo sequencial do documento de conclusao
        /// </summary>
        /// <param name="seqDocumentoConclusao">Sequencial do documento de conclusao</param>
        /// <returns>Lista de formações específicas</returns>
        List<FormacaoEspecificaData> BuscarFormacoesEspecificasPorDocumentoConclusao(long seqDocumentoConclusao);

        /// <summary>
        /// Verifica se a formação específica permite titulação, baseado no tipo formação específica
        /// </summary>
        /// <param name="seqFormacaoEspecifica">Sequencial formação especifica</param>
        /// <returns>Retorna se a formacao especifica permite titulação</returns>
        bool FormacaoEspefificaExibeTitulacao(long seqFormacaoEspecifica);
    }
}