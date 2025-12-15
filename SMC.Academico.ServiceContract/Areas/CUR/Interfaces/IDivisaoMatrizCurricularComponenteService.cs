using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Interfaces
{
    public interface IDivisaoMatrizCurricularComponenteService : ISMCService
    {
        /// <summary>
        /// Busca o cabeçalho de um componente da matriz curricular
        /// </summary>
        /// <param name="seqMatrizCurricular">Sequencial da matriz curricular</param>
        /// <param name="seqGrupoCurricularComponente">Sequencial do grupo curricular</param>
        /// <returns>Dados da matriz e do componente para o cabeçalho</returns>
        DivisaoMatrizCurricularComponenteCabecalhoData DivisaoMatrizCurricularComponenteCabecalho(long seqMatrizCurricular, long seqGrupoCurricularComponente);

        /// <summary>
        /// Busca componentes de uma matriz curricular com suas configurações
        /// </summary>
        /// <param name="filtros">Filtros da configuração de componentes na matriz</param>
        /// <returns>Dados paginados dos componentes curriculares da matriz agrupados por grupo curricular componente</returns>
        SMCPagerData<ConfiguracaoComponeteMatrizListarData> BuscarDivisaoMatrizCurricularGruposComponentes(DivisaoMatrizCurricularComponenteFiltroData filtros);

        /// <summary>
        /// Buscar configuração para nova configuração de componente
        /// </summary>
        /// <param name="filtro">Dados do sequencial da matriz curricular, grupo componente curricular e currículo curso oferta</param>
        /// <returns>Dados da nova divisão matriz curricular componente</returns>
        DivisaoMatrizCurricularComponenteData BuscarConfiguracaoNovaDivisaoMatrizCurricularComponente(DivisaoMatrizCurricularComponenteFiltroData filtro);

        /// <summary>
        /// Busca uma divisão matriz curricular compoenete pelo seu grupo curricular compomente
        /// </summary>
        /// <param name="filtro">Dados do sequencial da matriz curricular e grupo componente curricular</param>
        /// <returns>Dados da divisão matriz curricular componente</returns>
        DivisaoMatrizCurricularComponenteData BuscarDivisaoMatrizCurricularComponente(DivisaoMatrizCurricularComponenteFiltroData filtro);

        /// <summary>
        /// Valida se vai exibir o assert ao salvar uma configuração
        /// </summary>
        /// <param name="divisaoMatrizCurricularComponente">Dados da divisão a ser validada</param>
        /// <returns>Retorno se vai exibir o assert, e lista de grupos curriculares do componente da configuração</returns>
        (bool ExibirAssert, List<GrupoCurricularComponenteData> ListaGruposCurricularesComponente) ValidarAssertSalvar(DivisaoMatrizCurricularComponenteData divisaoMatrizCurricularComponente);

        /// <summary>
        /// Grava a divisão matriz curricular componente com suas divisões e componentes subistitutos
        /// </summary>
        /// <param name="divisaoMatrizCurricularComponente">Dados da divisão a ser gravada</param>
        /// <returns>Sequencial da divisão matriz curricular gravada</returns>
        long SalvarDivisaoMatrizCurricularComponente(DivisaoMatrizCurricularComponenteData divisaoMatrizCurricularComponente);

        /// <summary>
        /// Busca a lista de componente curricular assunto de acordo com as ofertas de matriz e componentes selecionados
        /// </summary>
        /// <param name="seqsMatrizCurricularOferta">Sequenciais das matrizes curriculares oferta</param>
        /// <returns>Lista de componentes assunto</returns>
        List<SMCDatasourceItem> BuscarDivisaoComponenteAssuntoSelect(List<long> seqsMatrizCurricularOferta);

        /// <summary>
        /// Busca o sequencial da divisões de componente curricular de uma matriz com a descrição de seus componentes.
        /// </summary>
        List<SMCDatasourceItem> BuscarDivisaoComponenteCurricularSelect(DivisaoMatrizCurricularComponenteFiltroData filtro);

        /// <summary>
        /// Buscar os assuntos de componentes ativos que existem cadastrados em TODAS as ofertas de matizes associadas à turma. Ou seja, 
        /// se a turma for compartilhada, o assunto a ser escolhido deve estar cadastrado em todas as ofertas de matrizes.
        /// A descrição do assunto deverá ser conforme RN_CUR_040 - Exibição descrição componente curricular, em ordem alfabética.
        /// </summary>
        /// <param name="seqsMatrizCurricularOferta">Sequenciais das matrizes curriculares oferta</param>
        /// <param name="seqsConfiguracoesComponente">Sequenciais das configurações componentes da turma (Principal + Compartilhadas)</param>
        /// <returns>Lista de componentes assunto</returns>
        List<SMCDatasourceItem> BuscarAssuntosComponentesOfertasMatrizesTurma(List<long> seqsMatrizCurricularOferta, List<long> seqsConfiguracoesComponente);

        /// <summary>
        /// Busca o cabeçalho da associação assunto pela configuração de componente
        /// </summary>
        /// <param name="seqDivisaoMatrizCurricularComponente">Sequencial divisão matriz curricular componente</param>
        /// <returns>Dados matriz e configuração de compoente para o cabeçalho</returns>
        AssuntoComponeteMatrizCabecalhoData BusacarAssuntoComponenteMatrizCabecalho(long seqDivisaoMatrizCurricularComponente);

        /// <summary>
        /// Excluir configuracao de componnente
        /// </summary>
        /// <param name="seq">Sequencial da configuração de componente</param>
        void ExcluirConfiguracaoComponente(long seq);

        /// <summary>
        /// Listar grupos curriculares associados ao componente curricular da configuração de componente em questão, na
        /// matriz curricular em questão com seus devidos assuntos
        /// </summary>
        /// <param name="seqDivisaoMatrizCurricularComponente">Sequencial divisão matriz curricular componente</param>
        /// <returns>Lista de grupos com assunto</returns>
        List<AssuntoComponeteMatrizListarData> BuscarAssuntosComponenteMatrizPorConfiguracaoComponente(long seqDivisaoMatrizCurricularComponente);

        /// <summary>
        /// Salvar Assunto do grupo curricular
        /// </summary>
        /// <param name="seq">Sequencial do assunto</param>
        /// <param name="seqDivisaoMatrizCurricularComponente">Sequencial da Divisao matriz curricular componente</param>
        void SalvarAssunto(long seq, long seqDivisaoMatrizCurricularComponente);

        /// <summary>
        /// Excluir Assunto do grupo curricular
        /// </summary>
        /// <param name="seq">Sequencial do assunto</param>
        /// <param name="seqDivisaoMatrizCurricularComponente">Sequencial da Divisao matriz curricular componente</param>
        void ExcluirAssunto(long seq, long seqDivisaoMatrizCurricularComponente);

        /// <summary>
        /// Buscar lista do Enum Comprovante Artigo em ordem conferme regra
        /// </summary>
        /// <returns>Lista Enum ordenada</returns>
        List<SMCDatasourceItem> BuscarComprovacaoArtigoOrdenada();
        List<SMCDatasourceItem> BuscarDivisaoComponenteCurricularProjetoQualificacao(DivisaoMatrizCurricularComponenteFiltroData filtro);
    }
}