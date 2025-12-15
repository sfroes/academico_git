using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.Domain.Areas.CSO.ValueObjects;
using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.CSO.Services
{
    public class ClassificacaoService : SMCServiceBase, IClassificacaoService
    {
        #region Serviço de Dominio

        private ClassificacaoDomainService ClassificacaoDomainService { get => this.Create<ClassificacaoDomainService>(); }

        #endregion Serviço de Dominio

        /// <summary>
        /// Busca as classificações de uma hierarquia
        /// </summary>
        /// <param name="classificacaoFiltroData">Dados dos filtros</param>
        /// <returns>Dados da hierarquia de classificação</returns>
        public ClassificacaoData[] BuscarClassificacaoPorHierarquiaClassificacao(ClassificacaoFiltroData classificacaoFiltroData)
        {
            return ClassificacaoDomainService.BuscarClassificacaoPorHierarquiaClassificacao(classificacaoFiltroData.Transform<ClassificacaoFiltroVO>()).TransformToArray<ClassificacaoData>();
        }
        /// <summary>
        /// Busca as classificações de uma hierarquia
        /// </summary>
        /// <param name="seqClassificacao"></param>
        /// <returns>Dados da hierarquia de classificação</returns>
        public ClassificacaoData[] BuscarClassificacaoPorHierarquiaClassificacaoLookup(long[] seqClassificacao)
        {
            return ClassificacaoDomainService.BuscarClassificacaoPorHierarquiaClassificacaoLookup(seqClassificacao)
                .TransformListToArray<ClassificacaoData>();
        }

        /// <summary>
        /// Busca as áreas de conhecimento para o BDP.
        /// </summary>
        public List<SMCDatasourceItem> BuscarClassificacoesBDP()
        {
            var spec = new ClassificacaoBDPSpecification();
            return ClassificacaoDomainService.SearchProjectionBySpecification(spec, x => new SMCDatasourceItem
            {
                Seq = x.Seq,
                Descricao = x.Descricao
            }).OrderBy(o => o.Descricao).ToList();
        }
    }
}