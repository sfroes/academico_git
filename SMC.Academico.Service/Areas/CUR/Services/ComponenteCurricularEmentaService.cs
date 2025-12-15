using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Service;

namespace SMC.Academico.Service.Areas.CUR.Services
{
    public class ComponenteCurricularEmentaService : SMCServiceBase, IComponenteCurricularEmentaService
    {

        #region [ DomainService ]

        private ComponenteCurricularEmentaDomainService ComponenteCurricularEmentaDomainService => Create<ComponenteCurricularEmentaDomainService>(); 

        #endregion [ DomainService ]

        /// <summary>
        /// Buscar os dados de um componente curricular ementa
        /// </summary>
        /// <param name="seqComponenteCurricular">Sequencial do componente curricular</param>
        /// <param name="seqCicloLetivo">Sequencial do ciclo letivo para validar o período de vigência</param>
        /// <returns>Informações do componente curricular ementa</returns>
        public ComponenteCurricularEmentaData BuscarComponenteCurricularEmentaPorComponenteCiclo(long seqComponenteCurricular, long seqCicloLetivo, long seqCursoOfertaLocalidadeTurno)
        {            
            var registro = ComponenteCurricularEmentaDomainService.BuscarComponenteCurricularEmentaPorComponenteCiclo(seqComponenteCurricular, seqCicloLetivo, seqCursoOfertaLocalidadeTurno);

            return registro.Transform<ComponenteCurricularEmentaData>();
        }
    }
}
