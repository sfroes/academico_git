using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.DCT.Models;
using SMC.Academico.Domain.Areas.DCT.Specifications;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.Domain.Helpers;
using SMC.Framework.DataFilters;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.DCT.DomainServices
{
    public class TipoVinculoColaboradorDomainService : AcademicoContextDomain<TipoVinculoColaborador>
    {
        #region Domanin Service

        private InstituicaoTipoEntidadeVinculoColaboradorDomainService InstituicaoTipoEntidadeVinculoColaboradorDomainService => Create<InstituicaoTipoEntidadeVinculoColaboradorDomainService>();

        private EntidadeDomainService EntidadeDomainService => Create<EntidadeDomainService>();

        #endregion Domanin Service

        /// <summary>
        /// Buscar todos os tipos de vinculo baseado nas configurações da instituição.
        /// </summary>
        /// <param name="seqEntidadeVinculo">Sequencial da entidade para filtrar apenas os vinculos configurados para o tipo desta</param>
        /// <param name="criaVinculoInstitucional">Retorna apenas os tipos de vínculo que permitem criar vínculo institucional</param>
        /// <returns>Dados dos vínculos configurados na instituição ou apenas os vinculos do tipo de entidade informado</returns>
        public List<SMCDatasourceItem> BuscarTipoVinculoColaboradorDeEntidadesVinculoSelect(long? seqEntidadeVinculo = null, bool? criaVinculoInstitucional = null)

        {
            var spec = new InstituicaoTipoEntidadeVinculoColaboradorFilterSpecification()
            {
                CriaVinculoInstitucional = criaVinculoInstitucional
            };
            if (seqEntidadeVinculo.HasValue)
            {
                FilterHelper.AtivarApenasFiltros(EntidadeDomainService, FILTER.INSTITUICAO_ENSINO);
                spec.SeqTipoEntidade = EntidadeDomainService.SearchProjectionByKey(new SMCSeqSpecification<Entidade>(seqEntidadeVinculo.Value), p => p.SeqTipoEntidade);
                FilterHelper.AtivarFiltros(EntidadeDomainService);
            }
            else if (SMCDataFilterHelper.UserRequiresDataFilters(false))
            {
                // Caso o usuário requeira filtro aplica a regra RN_USG_005 - Filtro por Entidade Responsável
                spec.SeqsTipoEntidade = EntidadeDomainService.SearchProjectionAll(p => p.SeqTipoEntidade, isDistinct: true).ToArray();
            }

            var tiposVinculos = this.InstituicaoTipoEntidadeVinculoColaboradorDomainService
                                                   .SearchProjectionBySpecification(spec, p => p.TipoVinculoColaborador, isDistinct: true)
                                                   .ToArray();

            List<SMCDatasourceItem> retorno = new List<SMCDatasourceItem>();

            foreach (var item in tiposVinculos.OrderBy(o => o.Descricao))
            {
                retorno.Add(new SMCDatasourceItem()
                {
                    Seq = item.Seq,
                    Descricao = item.Descricao,
                    DataAttributes = new List<SMCKeyValuePair>()
                    {
                        new SMCKeyValuePair() { Key = "data-supervisor", Value = item.RequerAcompanhamentoSupervisor ? "true" : "false" },
                        new SMCKeyValuePair() { Key = "data-formacao", Value = item.ExigeFormacaoAcademica ? "true" : "false" }
                    }
                });
            }

            return retorno;
        }

        /// <summary>
        /// Retornar se tipo vinculo colaborador necessita acompanhamento de supervisor
        /// </summary>
        /// <param name="seqTipoVinculoColaborador">Sequencial tipo vinculo colaborador</param>
        /// <returns>Retornar boleano necessita acompanhamento de supervisor </returns>
        public bool RetornarTipoVinculoNecessitaAcompanhamento(long seqTipoVinculoColaborador)
        {
            return this.SearchProjectionByKey(new SMCSeqSpecification<TipoVinculoColaborador>(seqTipoVinculoColaborador), p => p.RequerAcompanhamentoSupervisor);
        }


        public List<SMCDatasourceItem> BuscarTipoVinculoColaboradorPorEntidadesSelect(List<long> seqsEntidadeResponsaveis )

        {
            var spec = new InstituicaoTipoEntidadeVinculoColaboradorFilterSpecification();

            if (seqsEntidadeResponsaveis.Count() > 0)
            {
                FilterHelper.AtivarApenasFiltros(EntidadeDomainService, FILTER.INSTITUICAO_ENSINO);
                var specEntidade = new EntidadeFilterSpecification() { Seqs = seqsEntidadeResponsaveis.ToArray() };
                spec.SeqsTipoEntidade = EntidadeDomainService.SearchProjectionBySpecification(specEntidade, p => p.SeqTipoEntidade).ToArray();
                FilterHelper.AtivarFiltros(EntidadeDomainService);
            }
            else if (SMCDataFilterHelper.UserRequiresDataFilters(false))
            {
                // Caso o usuário requeira filtro aplica a regra RN_USG_005 - Filtro por Entidade Responsável
                spec.SeqsTipoEntidade = EntidadeDomainService.SearchProjectionAll(p => p.SeqTipoEntidade, isDistinct: true).ToArray();
            }

            var tiposVinculos = this.InstituicaoTipoEntidadeVinculoColaboradorDomainService
                                                   .SearchProjectionBySpecification(spec, p => p.TipoVinculoColaborador, isDistinct: true)
                                                   .ToArray();

            List<SMCDatasourceItem> retorno = new List<SMCDatasourceItem>();

            foreach (var item in tiposVinculos.OrderBy(o => o.Descricao))
            {
                retorno.Add(new SMCDatasourceItem()
                {
                    Seq = item.Seq,
                    Descricao = item.Descricao,
                    DataAttributes = new List<SMCKeyValuePair>()
                    {
                        new SMCKeyValuePair() { Key = "data-supervisor", Value = item.RequerAcompanhamentoSupervisor ? "true" : "false" },
                        new SMCKeyValuePair() { Key = "data-formacao", Value = item.ExigeFormacaoAcademica ? "true" : "false" }
                    }
                });
            }

            return retorno;
        }
    }
}