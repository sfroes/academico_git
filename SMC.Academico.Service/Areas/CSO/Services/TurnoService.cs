using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CSO.Services
{
    public class TurnoService : SMCServiceBase, ITurnoService
    {
        #region [ DomainService ]

        private TurnoDomainService TurnoDomainService
        {
            get { return this.Create<TurnoDomainService>(); }
        }

        #endregion [ DomainService ]

        /// <summary>
        /// Busca turnos para a listagem de acordo com o curso oferta localidade
        /// </summary>
        /// <param name="seqCursoOferta">Sequencial do curso oferta </param>
        /// <returns>Lista de turnos</returns>
        public List<SMCDatasourceItem> BuscarTurnosPorCursoOfertaSelect(long seqCursoOferta)
        {
            return this.TurnoDomainService.BuscarTurnosPorCursoOfertaSelect(seqCursoOferta);
        }

        /// <summary>
        /// Busca turnos para a listagem de acordo com o curso
        /// </summary>
        /// <param name="seqCurso">Sequencial do curso </param>
        /// <returns>Lista de turnos</returns>
        public List<SMCDatasourceItem> BuscarTurnosPorCursoSelect(long seqCurso)
        {
            return this.TurnoDomainService.BuscarTurnosPorCursoSelect(seqCurso);
        }

        /// <summary>
        /// Busca os turnos ativos para a listagem de acordo com o curso oferta localidade
        /// </summary>
        /// <param name="seqCursoOfertaLocalidade">Sequencial do curso oferta localidade</param>
        /// <returns>Lista de turnos</returns>
        public List<SMCDatasourceItem> BuscarTurnosPorCursoOfertaLocalidadeSelect(long seqCursoOfertaLocalidade)
        {
            return this.TurnoDomainService.BuscarTurnosPorCursoOfertaLocalidadeSelect(seqCursoOfertaLocalidade);
        }

        /// <summary>
        /// Busca turnos para a listagem de acordo com a localidade
        /// </summary>
        /// <param name="seqLocalidade">Sequencial da localidade</param>
        /// <returns>Lista de turnos</returns>
        public List<SMCDatasourceItem> BuscarTurnosPorLocalidadeSelect(long seqLocalidade)
        {
            return this.TurnoDomainService.BuscarTurnosPorLocalidadeSelect(seqLocalidade);
        }

        /// <summary>
        /// Busca os turnos que sejam do nível de ensino do curso do curriculo curso oferta informado
        /// </summary>
        /// <param name="seqCurriculoCursoOferta">Sequencial do currículo curso oferta do curso com o nível de ensino em questão</param>
        /// <returns>Dados dos turnos</returns>
        public List<SMCDatasourceItem> BuscarTurnosNivelEnsinoPorCurriculoCursoOfertaSelect(long seqCurriculoCursoOferta)
        {
            return this.TurnoDomainService.BuscarTurnosNivelEnsinoPorCurriculoCursoOfertaSelect(seqCurriculoCursoOferta);
        }

        public List<SMCDatasourceItem> BuscarTunos()
        {
            return this.TurnoDomainService.BuscarTunos();
        }

        /// <summary>
        /// Busca todos os turnos que atendam aos filtros informados
        /// </summary>
        /// <param name="filtroData">Dados dos filtros</param>
        /// <returns>Dados dos turnos que atendam aos filtros informados</returns>
        public List<SMCDatasourceItem> BuscarTurnosSelect(TurnoFiltroData filtroData)
        {
            return this.TurnoDomainService.BuscarTurnosSelect(filtroData.Transform<TurnoFilterSpecification>());
        }
    }
}