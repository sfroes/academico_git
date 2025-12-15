using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.ALN.Services
{
    public class TipoTermoIntercambioService : SMCServiceBase, ITipoTermoIntercambioService
    {
        #region [ Domain services ]

        private TipoTermoIntercambioDomainService TipoTermoIntercambioDomainService
        {
            get { return Create<TipoTermoIntercambioDomainService>(); }
        }

        private InstituicaoNivelTipoVinculoAlunoDomainService InstituicaoNivelTipoVinculoAlunoDomainService
        {
            get { return Create<InstituicaoNivelTipoVinculoAlunoDomainService>(); }
        }

        #endregion [ Domain services ]

        public List<SMCDatasourceItem> BuscarTiposTermosIntercambiosSelect()
        {
            return TipoTermoIntercambioDomainService.BuscarTodosTiposTermosIntercambioSelect();
        }

        public List<SMCDatasourceItem> BuscarTiposTermosIntercambiosInstituicaoNivelSelect()
        {
            return TipoTermoIntercambioDomainService.BuscarTiposTermosIntercambiosInstituicaoNivelSelect();
        }

        /// <summary>
        /// Busca somente os Tipos de Termo Intercambio que permitem associação pro aluno
        /// </summary>
        public List<SMCDatasourceItem> BuscarTiposTermosIntercambiosInstituicaoNivelPermiteAssociarAlunoSelect()
        {
            return TipoTermoIntercambioDomainService.BuscarTiposTermosIntercambiosInstituicaoNivelPermiteAssociarAlunoSelect();
        }

        /// <summary>
        /// Tipos de Termos que estão associados a uma Parceria de Intercâmbio.
        /// </summary>
        /// <param name="seqParceriaIntercambio">Sequencial da Parceria de Intercâmbio.</param>
        /// <returns>Lista dos Tipos de Termos de intercâmbio.</returns>
        public List<SMCDatasourceItem> BuscarTiposTermosIntercambiosDaParceriaSelect(long seqParceriaIntercambio, long seqNivelEnsino)
        {
            return TipoTermoIntercambioDomainService.BuscarTiposTermosIntercambiosDaParceriaSelect(seqParceriaIntercambio, seqNivelEnsino);
        }

        /// <summary>
        /// Busca todos os tipo de termo de intercambio de um nível de ensino.
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do nível de ensino.</param>
        /// <param name="seqParceriaIntercambio">Sequencial da parceria de intercâmbio</param>
        /// <returns>Tipos de termo de intercâmbio.</returns>
        public List<SMCDatasourceItem> BuscarTiposTermoIntercambioPorNivelEnsino(long seqNivelEnsino, long seqParceriaIntercambio)
        {
            return InstituicaoNivelTipoVinculoAlunoDomainService.BuscarTiposTermoIntercambioPorNivelEnsino(seqNivelEnsino, seqParceriaIntercambio);
        }

        /// <summary>
        /// Buscar tipos termo intercambio por nivel de ensino e parceria de intercambio
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial nivel de ensino</param>
        /// <param name="seqParceriaIntercambio">Sequencial parceria intercambio</param>
        /// <param name="ativo">Indicativo situacao</param>
        /// <returns>Tipos de termo de intercambio</returns>
        public List<SMCDatasourceItem> BuscaTiposTermoIntercabioPorParceriaIntercambioTipoTermoSelect(long? seqNivelEnsino, long seqParceriaIntercambio, bool? ativo = null)
        {
            return this.TipoTermoIntercambioDomainService.BuscaTiposTermoIntercabioPorParceriaIntercambioTipoTermoSelect(seqNivelEnsino, seqParceriaIntercambio, ativo);
        }

        /// <summary>
        /// Valida se o tipo de termo intercambio é Cotutela
        /// </summary>
        /// <param name="seq">Sequencial tipo termo intercambio</param>
        /// <returns>Boleano caso</returns>
        public bool ValidarTipoTermoIntercambioCoutela(long seq)
        {
            return this.TipoTermoIntercambioDomainService.ValidarTipoTermoIntercambioCoutela(seq);
        }
    }
}