using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Academico.UI.Mvc.Areas.ORG.Lookups;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using System.Linq;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Lookups
{
    public class OfertaMatrizCurricularLookupPrepareFilter : ISMCFilter<OfertaMatrizCurricularLookupFiltroViewModel>
    {
        public OfertaMatrizCurricularLookupFiltroViewModel Filter(SMCControllerBase controllerBase, OfertaMatrizCurricularLookupFiltroViewModel filter)
        {           
            if (filter.SeqCursoOferta != null && filter.SeqCursoOferta.Seq > 0)
            {
                filter.CursoOfertaLeitura = true;

                var cursoOfertaService = controllerBase.Create<ICursoOfertaService>();
                filter.SeqCursoOferta = cursoOfertaService.BuscarCursoOferta(filter.SeqCursoOferta.Seq.Value).Transform<CursoOfertaLookupViewModel>();

                var modalidadeService = controllerBase.Create<IModalidadeService>();
                filter.Modalidades = modalidadeService.BuscarModalidadesPorCursoOfertaSelect(filter.SeqCursoOferta.Seq.Value);

                var turnoService = controllerBase.Create<ITurnoService>();
                filter.Turnos = turnoService.BuscarTurnosPorCursoOfertaSelect(filter.SeqCursoOferta.Seq.Value);
            }
            else
            {
                filter.CursoOfertaLeitura = false;

                var instituicaoModalidadeService = controllerBase.Create<IInstituicaoNivelModalidadeService>();
                filter.Modalidades = instituicaoModalidadeService.BuscarModalidadesPorInstituicaoSelect();

                var instituicaoTurnoService = controllerBase.Create<IInstituicaoNivelTurnoService>();
                filter.Turnos = instituicaoTurnoService.BuscarTurnosPorInstituicaoSelect();
            }

            filter.ModalidadeLeitura = filter.SeqModalidade != null && filter.SeqModalidade > 0;

            if (filter.SeqLocalidade != null && filter.SeqLocalidade.Seq > 0)
            {
                filter.LocalidadeLeitura = true;

                var hierarquiaEntidadeService = controllerBase.Create<IHierarquiaEntidadeItemService>();
                var listHierarquia = hierarquiaEntidadeService.BuscarHierarquiaEntidadeItemLookup(new HierarquiaEntidadeItemFiltroData() { Seq = filter.SeqLocalidade.Seq.Value }).TransformList<HierarquiaEntidadeLookupViewModel>();
                filter.SeqLocalidade = listHierarquia.FirstOrDefault();
            }
            else { filter.LocalidadeLeitura = false; }

            filter.TurnoLeitura = filter.SeqTurno != null && filter.SeqTurno > 0;
           
            filter.CodigoLeitura = !string.IsNullOrEmpty(filter.Codigo);

            filter.DescricaoLeitura = !string.IsNullOrEmpty(filter.DescricaoMatrizCurricular);

            return filter;
        }
    }
}
