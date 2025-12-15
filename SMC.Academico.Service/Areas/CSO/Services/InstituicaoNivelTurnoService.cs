using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CSO.ValueObjects;
using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CSO.Services
{
    public class InstituicaoNivelTurnoService : SMCServiceBase, IInstituicaoNivelTurnoService
    {
        #region [ DomainService ]

        private InstituicaoNivelTurnoDomainService InstituicaoNivelTurnoDomainService
        {
            get { return this.Create<InstituicaoNivelTurnoDomainService>(); }
        }

        #endregion

        /// <summary>
        /// Busca turnos para a listagem de acordo com o instituição nível
        /// </summary>       
        /// <param name="seqInstituicaoNivel">Sequencial da instituição nível</param>
        /// <returns>Lista de turnos</returns>
        public List<SMCDatasourceItem> BuscarInstituicaoNivelTurnoSelect(long seqInstituicaoNivel)
        {
            return this.InstituicaoNivelTurnoDomainService.BuscarInstituicaoNivelTurnoSelect(seqInstituicaoNivel);
        }

        /// <summary>
        /// Busca turnos para a listagem de acordo com o instituição
        /// </summary>       
        /// <returns>Lista de turnos</returns>
        public List<SMCDatasourceItem> BuscarTurnosPorInstituicaoSelect()
        {
            return this.InstituicaoNivelTurnoDomainService.BuscarTurnosPorInstituicaoSelect();
        }

        /// <summary>
        /// Salva a instituicao nivel turno 
        /// </summary>
        /// <param name="modelo">Modelo a ser salvo</param>
        /// <returns>Sequencial da instituicao nivel turno</returns>
        public long Salvar(InstituicaoNivelTurnoData modelo)
        {
            return this.InstituicaoNivelTurnoDomainService.SalvarInstituicaoNivelTurno(modelo.Transform<InstituicaoNivelTurnoVO>());
        }
    }
}
