using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.ORG.Services
{
    public class TipoEntidadeService : SMCServiceBase, ITipoEntidadeService
    {
        #region Serviço de Dominio

        internal TipoEntidadeDomainService TipoEntidadeDomainService => this.Create<TipoEntidadeDomainService>();

        #endregion Serviço de Dominio

        public TipoEntidadeData BuscarTipoEntidadeNaInstituicao(string tokenTipoEntidade)
        {
            return this.TipoEntidadeDomainService.BuscarTipoEntidadeNaInstituicao(tokenTipoEntidade).Transform<TipoEntidadeData>();
        }

        /// <summary>
        /// Busca o tipos de entidade
        /// </summary>
        /// <param name="tokenTipoEntidade">Filtro</param>
        /// <returns>Tipos de entidade</returns>
        public List<TipoEntidadeData> BuscarTiposEntidade(TipoEntidadeFiltroData filtro)
        {
            return this.TipoEntidadeDomainService.BuscarTiposEntidade(filtro.Transform<TipoEntidadeFilterSpecification>()).TransformList<TipoEntidadeData>();
        }

        /// <summary>
        /// Busca o tipos de entidade com a inclusão do tipo Instituição de Ensino
        /// </summary>
        /// <param name="permiteAtoNormativo">Permite Ato Normativo</param>
        /// <param name="seqInstituicaoEnsino">Sequencial da Instituição de Ensino logada</param>
        /// <returns>Tipos de entidade</returns>
        public List<SMCDatasourceItem> BuscarTiposEntidadeComInstituicao(bool permiteAtoNormativo, long seqInstituicaoEnsino)
        {
            return TipoEntidadeDomainService.BuscarTiposEntidadeComInstituicao(permiteAtoNormativo, seqInstituicaoEnsino);
        }

        /// <summary>
        /// Busca o token de acordo com o filtro informado
        /// </summary>
        /// <param name="seqTipoEntidade">Sequencial tipo entidade</param>
        /// <returns>Retorna token</returns>
        public string BuscarTokenTipoEntidade(long seqTipoEntidade)
        {
            var filtro = new TipoEntidadeFilterSpecification { Seq = seqTipoEntidade };
            return this.TipoEntidadeDomainService.BuscarTokenTipoEntidade(filtro);
        }

        /// <summary>
        /// Buscar os tipos de entidade que são responsáveis em uma visão
        /// </summary>
        /// <param name="seqInstituicaoEnsino">Sequencial da instituição de ensino</param>
        /// <param name="visao">Visão a ser pesquisada</param>
        /// <returns>Lista de tipos de entidade que são responsáveis na visão e instituição de ensino do usuário logado</returns>
        public List<SMCDatasourceItem> BuscarTipoEntidadeResponsavelPorVisao(long seqInstituicaoEnsino, TipoVisao visao)
        {
            return this.TipoEntidadeDomainService.BuscarTipoEntidadeResponsavelPorVisao(seqInstituicaoEnsino, visao);
        }

    }
}