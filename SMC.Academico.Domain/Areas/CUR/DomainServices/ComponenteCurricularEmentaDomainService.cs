using SMC.Academico.Common.Areas.CUR.Includes;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.CUR.DomainServices
{
    public class ComponenteCurricularEmentaDomainService : AcademicoContextDomain<ComponenteCurricularEmenta>
    {
        #region [ DomainService ]

        private ConfiguracaoEventoLetivoDomainService ConfiguracaoEventoLetivoDomainService => Create<ConfiguracaoEventoLetivoDomainService>();

        #endregion [ DomainService ]

        /// <summary>
        /// Buscar os dados de um componente curricular ementa
        /// </summary>
        /// <param name="seqComponenteCurricular">Sequencial do componente curricular</param>
        /// <param name="seqCicloLetivo">Sequencial do ciclo letivo para validar o período de vigência</param>
        /// <returns>Informações do componente curricular ementa</returns>
        public ComponenteCurricularEmentaVO BuscarComponenteCurricularEmentaPorComponenteCiclo(long seqComponenteCurricular, long seqCicloLetivo, long seqCursoOfertaLocalidadeTurno)
        {
            var spec = new ComponenteCurricularEmentaFilterSpecification();
            spec.SeqComponenteCurricular = seqComponenteCurricular;

            try
            {
                //Tratamento porque o método retorna throw não não existe registro na carga de dados
                //TOKEN_TIPO_EVENTO.PERIODO_FINANCEIRO
                var datas = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(seqCicloLetivo, seqCursoOfertaLocalidadeTurno, null, null);
                spec.DataInicio = datas.DataInicio;
                spec.DataFim = datas.DataFim;

                var registro = this.SearchByKey(spec, IncludesComponenteCurricularEmenta.ComponenteCurricular_TipoComponente).Transform<ComponenteCurricularEmentaVO>();

                return registro;
            }
            catch (SMCApplicationException ex)
            {
                var registro = this.SearchByKey(spec, IncludesComponenteCurricularEmenta.ComponenteCurricular_TipoComponente).Transform<ComponenteCurricularEmentaVO>();

                return registro;

            }            
        }
    }
}
