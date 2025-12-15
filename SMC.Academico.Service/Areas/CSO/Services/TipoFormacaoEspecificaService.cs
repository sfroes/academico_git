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
    public class TipoFormacaoEspecificaService : SMCServiceBase, ITipoFormacaoEspecificaService
    {
        #region Services

        private TipoFormacaoEspecificaDomainService TipoFormacaoEspecificaDomainService
        {
            get { return this.Create<TipoFormacaoEspecificaDomainService>(); }
        }

        private InstituicaoTipoEntidadeFormacaoEspecificaDomainService InstituicaoTipoEntidadeFormacaoEspecificaDomainService
        {
            get { return this.Create<InstituicaoTipoEntidadeFormacaoEspecificaDomainService>(); }
        }

        private FormacaoEspecificaDomainService FormacaoEspecificaDomainService
        {
            get { return this.Create<FormacaoEspecificaDomainService>(); }
        }

        #endregion Services

        /// <summary>
        /// Busca um tipo de formação específica
        /// </summary>
        /// <param name="seq">Sequencial do tipo de formação específica a ser recuperado</param>
        /// <returns>Dados do tipo de formação específica</returns>
        public TipoFormacaoEspecificaData BuscarTipoFormacaoEspecifica(long seq)
        {
            return TipoFormacaoEspecificaDomainService.BuscarTipoFormacaoEspecifica(seq).Transform<TipoFormacaoEspecificaData>();
        }

        /// <summary>
        /// Busca a lista de formações específicas para popular um Select
        /// </summary>
        /// <param name="classeTipoFormacao">Classe do tipo de formação específica</param>
        /// <returns>Lista de tipos de formação especifica</returns>
        public List<SMCDatasourceItem> BuscarTipoFormacaoEspecificaSelect(TipoFormacaoEspecificaFiltroData filtro)
        {
            TipoFormacaoEspecificaFilterSpecification spec = filtro.Transform<TipoFormacaoEspecificaFilterSpecification>();
            spec.SetOrderBy(o => o.Descricao);
            return TipoFormacaoEspecificaDomainService.SearchBySpecification(spec).TransformList<SMCDatasourceItem>();
        }

        /// <summary>
        /// Busca a lista de formações específicas para popular um Select por nível de ensino e instituição
        /// </summary>
        /// <param name="filtro">Filtro da Instituição nível por tipo de formação específica</param>
        /// <returns>Lista de tipos de formação especifica</returns>
        public List<SMCDatasourceItem> BuscarTipoFormacaoEspecificaPorNivelEnsinoSelect(TipoFormacaoEspecificaPorNivelEnsinoFiltroData filtro)
        {
            return TipoFormacaoEspecificaDomainService.BuscarTipoFormacaoEspecificaPorNivelEnsinoSelect(filtro.Transform<InstituicaoNivelTipoFormacaoEspecificaFilterSpecification>());
        }

        public List<SMCDatasourceItem> BuscarTipoFormacaoEspecificaEntidadeSelect(long seqTipoEntidade, long? seqFormacaoEspecificaSuperior)
        {
            //TODO: Ajustar ISMCTreeNode para passar os parâmetros do modelo para passar o seqInstituicaoTipoEntidadeFormacaoEspecificaSuperior
            //também na criação
            long? seqInstituicaoTipoEntidadeFormacaoEspecificaSuperior = null;
            if (seqFormacaoEspecificaSuperior.HasValue)
            {
                seqInstituicaoTipoEntidadeFormacaoEspecificaSuperior = FormacaoEspecificaDomainService.SearchProjectionByKey(
                     new FormacaoEspecificaFilterSpecification() { Seq = seqFormacaoEspecificaSuperior.Value },
                     p => p.SeqTipoFormacaoEspecifica);
            }

            var specification = new InstituicaoTipoEntidadeFormacaoEspecificaFilterSpecification()
            {
                Ativo = true,
                ApenasFilhos = true,
                SeqTipoFormacaoEspecificaPai = seqInstituicaoTipoEntidadeFormacaoEspecificaSuperior,
                SeqTipoEntidade = seqTipoEntidade
            };

            specification.SetOrderBy(x => x.TipoFormacaoEspecifica.Descricao);

            return InstituicaoTipoEntidadeFormacaoEspecificaDomainService
                .SearchProjectionBySpecification(specification, x => new SMCDatasourceItem()
                {
                    Seq = x.TipoFormacaoEspecifica.Seq,
                    Descricao = x.TipoFormacaoEspecifica.Descricao
                }).ToList();
        }

        public long Salvar(TipoFormacaoEspecificaData modelo)
        {
            return TipoFormacaoEspecificaDomainService.SalvarTipoFormacaoEspecifica(modelo.Transform<TipoFormacaoEspecificaVO>());
        }
    }
}