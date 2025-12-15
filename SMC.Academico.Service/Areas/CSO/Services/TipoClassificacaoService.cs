using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CSO.Services
{
    public class TipoClassificacaoService : SMCServiceBase, ITipoClassificacaoService
    {
        #region Serviço de Dominio

        internal HierarquiaClassificacaoDomainService HierarquiaClassificacaoDomainService
        {
            get { return this.Create<HierarquiaClassificacaoDomainService>(); }
        }

        internal TipoClassificacaoDomainService TipoClassificacaoDomainService
        {
            get { return this.Create<TipoClassificacaoDomainService>(); }
        }

        internal ClassificacaoDomainService ClassificacaoDomainService
        {
            get { return this.Create<ClassificacaoDomainService>(); }
        }

        #endregion Serviço de Dominio

        /// <returns>Lista de tipos de classificação</returns>
        /// <summary>
        /// Busca a lista de tipos de classificação da hierarquia escolhida para popular um Select
        /// </summary>
        /// <param name="seqHierarquiaClassificacao">Sequencial de hierarquia de classificação</param>
        /// <param name="exclusivo">TRUE - Filtra o retorno de acordo com o nivel, FALSE - Retorna todos os tipos da hierarquia independente do nivel</param>
        /// <param name="seqClassificacaoSuperior">Sequencial que define o nivel na hierarquia</param>
        /// <returns>Lista de tipos de classificação</returns>
        public List<SMCDatasourceItem> BuscarTiposClassificacaoPorHierarquiaSelect(TipoClassificacaoPorHierarquiaFiltroData filtros)
        {
            if (filtros.SeqHierarquiaClassificacao == 0)
                return null;

            long codigoHierarquia = this.HierarquiaClassificacaoDomainService.SearchByKey<HierarquiaClassificacao, HierarquiaClassificacaoData>(filtros.SeqHierarquiaClassificacao).SeqTipoHierarquiaClassificacao;

            long? codigoClassificacao = null;
            if (filtros.SeqClassificacaoSuperior.HasValue)
                codigoClassificacao = this.ClassificacaoDomainService.SearchByKey<Classificacao, ClassificacaoData>(filtros.SeqClassificacaoSuperior.Value).SeqTipoClassificacao;

            IEnumerable<TipoClassificacao> listaTiposClassificacao;

            if (filtros.Exclusivo)
                listaTiposClassificacao = this.TipoClassificacaoDomainService.SearchBySpecification(new TipoClassificacaoFilterSpecification { SeqTipoHierarquiaClassificacao = codigoHierarquia, SeqTipoClassificacaoSuperior = codigoClassificacao, Exclusivo = true });
            else
                listaTiposClassificacao = this.TipoClassificacaoDomainService.SearchBySpecification(new TipoClassificacaoFilterSpecification { SeqTipoHierarquiaClassificacao = codigoHierarquia, Exclusivo = false });

            // Monta a lista para retorno
            List<SMCDatasourceItem> retorno = new List<SMCDatasourceItem>();
            foreach (var item in listaTiposClassificacao)
            {
                retorno.Add(new SMCDatasourceItem(item.Seq, item.Descricao));
            }
            return retorno;
        }
    }
}