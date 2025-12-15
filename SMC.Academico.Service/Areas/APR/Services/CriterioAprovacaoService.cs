using SMC.Academico.Domain.Areas.APR.DomainServices;
using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Academico.Domain.Areas.APR.ValueObjects;
using SMC.Academico.ServiceContract.Areas.APR.Data;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.APR.Services
{
    public class CriterioAprovacaoService : SMCServiceBase, ICriterioAprovacaoService
    {
        #region [ DomainService ]

        private CriterioAprovacaoDomainService CriterioAprovacaoDomainService
        {
            get { return this.Create<CriterioAprovacaoDomainService>(); }
        }

        #endregion [ DomainService ]

        /// <summary>
        /// Busca o critério de aprovação selecionado
        /// </summary>
        /// <param name="seq">Sequencial do critério de aprovação</param>
        /// <returns>Dados dos critério de aprovação</returns>
        public CriterioAprovacaoData BuscarCriterioAprovacao(long seq)
        {
            return this.CriterioAprovacaoDomainService.BuscarCriterioAprovacao(seq).Transform<CriterioAprovacaoData>();
        }

        /// <summary>
        /// Busca os critérios de aprovação que sejam do nível de ensino do curso do curriculo curso oferta informado
        /// </summary>
        /// <param name="seqCurriculoCursoOferta">Sequencial do currículo curso oferta do curso com o nível de ensino em questão</param>
        /// <returns>Dados dos critérios de aprovação</returns>
        public List<SMCDatasourceItem> BuscarCriteriosAprovacaoNivelEnsinoPorCurriculoCursoOfertaSelect(long seqCurriculoCursoOferta)
        {
            return this.CriterioAprovacaoDomainService.BuscarCriteriosAprovacaoNivelEnsinoPorCurriculoCursoOfertaSelect(seqCurriculoCursoOferta);
        }

        /// <summary>
        /// Busca os critérios de aprovação que sejam do nível de ensino da configuração do componente
        /// </summary>
        /// <param name="seqConfiguracaoComponente">Sequencial da configuração do componente com o nível de ensino em questão</param>
        /// <returns>Dados dos critérios de aprovação</returns>
        public List<SMCDatasourceItem> BuscarCriteriosAprovacaoNivelEnsinoPorConfiguracaoComponenteSelect(long seqConfiguracaoComponente)
        {
            return this.CriterioAprovacaoDomainService.BuscarCriteriosAprovacaoNivelEnsinoPorConfiguracaoComponenteSelect(seqConfiguracaoComponente);
        }

        public long SalvarCriterioAprovacao(CriterioAprovacaoData criterioAprovacao)
        {
            var criterioAprovacaoDominio = criterioAprovacao.Transform<CriterioAprovacao>();
            return this.CriterioAprovacaoDomainService.SalvarCriterioAprovacao(criterioAprovacaoDominio);
        }

        /// <summary>
        /// Recupera os critérios de aprovação pelos filtros informados
        /// </summary>
        /// <param name="filtroData">Dados dos filtros</param>
        /// <returns>Dados dos critérios de aprovação</returns>
        public List<SMCDatasourceItem> BuscarCriteriosAprovacaoSelect(CriterioAprovacaoFiltroData filtroData)
        {
            //FIX: Remover ao corrigir a falha de inicialização
            if (filtroData.SeqComponenteCurricular.GetValueOrDefault() == 0)
            {
                return null;
            }
            return this.CriterioAprovacaoDomainService.BuscarCriteriosAprovacaoSelect(filtroData.Transform<CriterioAprovacaoFiltroVO>());
        }
    }
}