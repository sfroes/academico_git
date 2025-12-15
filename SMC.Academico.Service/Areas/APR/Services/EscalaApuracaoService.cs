using SMC.Academico.Domain.Areas.APR.DomainServices;
using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Academico.ServiceContract.Areas.APR.Data;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.APR.Services
{
    public class EscalaApuracaoService : SMCServiceBase, IEscalaApuracaoService
    {
        #region [ Serviços ]

        private EscalaApuracaoDomainService EscalaApuracaoDomainService
        {
            get { return this.Create<EscalaApuracaoDomainService>(); }
        }

        #endregion [ Serviços ]

        /// <summary>
        /// Cria uma Escala Apuração com um item aprovado e um item reprovado
        /// </summary>
        /// <returns>Escala de Apuração com dois itens</returns>
        public EscalaApuracaoData BuscarConfiguracaoEscalaApuracao()
        {
            return this.EscalaApuracaoDomainService
                .BuscarConfiguracaoEscalaApuracao()
                .Transform<EscalaApuracaoData>();
        }

        /// <summary>
        /// Busca uma escala de Apuração com seus Itens, Critérios de Aprovação e Flag de Utilização por Crietério de Aprovação
        /// </summary>
        /// <param name="seq">Sequencial da Escala de Apuração a ser recuperada</param>
        /// <returns>Retorna a escala</returns>
        public EscalaApuracaoData BuscarEscalaApuracao(long seq)
        {
            return this.EscalaApuracaoDomainService
                .BuscarEscalaApuracao(seq)
                .Transform<EscalaApuracaoData>();
        }

        /// <summary>
        /// Busca as escalas de Apuração marcadas apra Apuração Final
        /// </summary>
        /// <returns>Dados das escação de Apuração com ApuracaoFinal setado</returns>
        public List<SMCDatasourceItem> BuscarEscalaApuracaoFinalSelect()
        {
            return this.EscalaApuracaoDomainService.BuscarEscalaApuracaoFinalSelect();
        }

        /// <summary>
        /// Busca as escalas de Apuração que não sejam do tipo conceito e sejam do nível de ensino do curso do curriculo curso oferta informado
        /// </summary>
        /// <param name="seqCurriculoCursoOferta">Sequencial do currículo curso oferta do curso com o nível de ensino em questão</param>
        /// <returns>Dados das escação de Apuração</returns>
        public List<SMCDatasourceItem> BuscarEscalasApuracaoNaoConceitoNivelEnsinoSelect(long seqCurriculoCursoOferta)
        {
            return this.EscalaApuracaoDomainService.BuscarEscalasApuracaoNaoConceitoNivelEnsinoSelect(seqCurriculoCursoOferta);
        }

        /// <summary>
        /// Busca as escalas de Apuração que não sejam do tipo conceito e sejam do nível de ensino do configuração de componente
        /// </summary>
        /// <param name="seqConfiguracaoComponente">Sequencial da configuração do componente com o nível de ensino em questão</param>
        /// <returns>Dados das escação de Apuração</returns>
        public List<SMCDatasourceItem> BuscarEscalasApuracaoNivelEnsinoPorConfiguracaoComponenteSelect(long seqConfiguracaoComponente)
        {
            return this.EscalaApuracaoDomainService.BuscarEscalasApuracaoNivelEnsinoPorConfiguracaoComponenteSelect(seqConfiguracaoComponente);
        }

        /// <summary>
        /// Grava uma Escala de Apuração aplicando as validações
        /// </summary>
        /// <param name="escalaApuracao">Escala de Validação a ser gravada</param>
        /// <returns>Sequencial da Escala de Apuração gravada</returns>
        public long SalvarEscalaApuracao(EscalaApuracaoData escalaApuracao)
        {
            EscalaApuracao escalaApuracaoDomain = escalaApuracao.Transform<EscalaApuracao>();
            return this.EscalaApuracaoDomainService.SalvarEscalaApuracao(escalaApuracaoDomain);
        }
    }
}