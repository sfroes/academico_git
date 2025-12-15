using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CAM.Services
{
    public class RegimeLetivoService : SMCServiceBase, IRegimeLetivoService
    {
        #region DomainService

        private RegimeLetivoDomainService RegimeLetivoDomainService
        {
            get { return this.Create<RegimeLetivoDomainService>(); }
        }

        private InstituicaoNivelRegimeLetivoDomainService InstituicaoNivelRegimeLetivoDomainService
        {
            get { return this.Create<InstituicaoNivelRegimeLetivoDomainService>(); }
        }

        #endregion Serviços

        /// <summary>
        /// Salva um regime letivo
        /// </summary>
        /// <param name="regimeLetivo">Dados do regime letivo a ser salva</param>
        /// <returns>Sequencial do regime letivo salva</returns>
        public long SalvarRegimeLetivo(RegimeLetivoData regimeLetivo)
        {
            var regimeLetivoDominio = regimeLetivo.Transform<RegimeLetivo>();
            return this.RegimeLetivoDomainService.SalvarRegimeLetivo(regimeLetivoDominio);
        }

        /// <summary>
        /// Busca os regimes letivos disponíveis para programas stricto sensu
        /// </summary>
        /// <returns>Lista de regimes letivos</returns>
        public List<SMCDatasourceItem> BuscarRegimesLetivosStrictoSelect()
        {
            return this.InstituicaoNivelRegimeLetivoDomainService.BuscarRegimesLetivosStrictoSelect();
        }

        /// <summary>
        /// Busca a lista de regime letivo de acordo com o nível de ensino
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do nível de ensino</param>
        /// <returns>Lista de regime letivo</returns>
        public List<SMCDatasourceItem> BuscarRegimesLetivoPorNivelEnsinoSelect(long seqNivelEnsino)
        {
            return this.InstituicaoNivelRegimeLetivoDomainService.BuscarRegimesLetivoPorNivelEnsinoSelect(seqNivelEnsino);
        }
        
        /// <summary>
        /// Busca todos os regimes letivos da intituição atual
        /// </summary>
        /// <returns>Lista com os dados de select dos regimes da instituição atual</returns>
        public List<SMCDatasourceItem> BuscarRegimesLetivosInstituicaoSelect()
        {
            return this.InstituicaoNivelRegimeLetivoDomainService.BuscarRegimesLetivosInstituicaoSelect();
        }
     
    }
}
