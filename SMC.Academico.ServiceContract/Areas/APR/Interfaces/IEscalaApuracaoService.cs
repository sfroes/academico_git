using SMC.Academico.ServiceContract.Areas.APR.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.APR.Interfaces
{
    public interface IEscalaApuracaoService : ISMCService
    {
        /// <summary>
        /// Cria uma Escala Apuração com um item aprovado e um item reprovado
        /// </summary>
        /// <returns>Escala de Apuração com dois itens</returns>
        EscalaApuracaoData BuscarConfiguracaoEscalaApuracao();

        /// <summary>
        /// Busca uma escala de Apuração com seus Itens, Critérios de Aprovação e Flag de Utilização por Crietério de Aprovação
        /// </summary>
        /// <param name="seq">Sequencial da Escala de Apuração a ser recuperada</param>
        /// <returns>Retorna a escala</returns>
        EscalaApuracaoData BuscarEscalaApuracao(long seq);

        /// <summary>
        /// Busca as escalas de Apuração marcadas apra Apuração Final
        /// </summary>
        /// <returns>Dados das escação de Apuração com ApuracaoFinal setado</returns>
        List<SMCDatasourceItem> BuscarEscalaApuracaoFinalSelect();

        /// <summary>
        /// Busca as escalas de Apuração que não sejam do tipo conceito e sejam do nível de ensino do curso do curriculo curso oferta informado
        /// </summary>
        /// <param name="seqCurriculoCursoOferta">Sequencial do currículo curso oferta do curso com o nível de ensino em questão</param>
        /// <returns>Dados das escação de Apuração</returns>
        List<SMCDatasourceItem> BuscarEscalasApuracaoNaoConceitoNivelEnsinoSelect(long seqCurriculoCursoOferta);

        /// <summary>
        /// Busca as escalas de Apuração que não sejam do tipo conceito e sejam do nível de ensino do configuração de componente
        /// </summary>
        /// <param name="seqConfiguracaoComponente">Sequencial da configuração do componente com o nível de ensino em questão</param>
        /// <returns>Dados das escação de Apuração</returns>
        List<SMCDatasourceItem> BuscarEscalasApuracaoNivelEnsinoPorConfiguracaoComponenteSelect(long seqConfiguracaoComponente);

        /// <summary>
        /// Grava uma Escala de Apuração aplicando as validações
        /// </summary>
        /// <param name="escalaApuracao">Escala de Validação a ser gravada</param>
        /// <returns>Sequencial da Escala de Apuração gravada</returns>
        long SalvarEscalaApuracao(EscalaApuracaoData escalaApuracao);
    }
}
