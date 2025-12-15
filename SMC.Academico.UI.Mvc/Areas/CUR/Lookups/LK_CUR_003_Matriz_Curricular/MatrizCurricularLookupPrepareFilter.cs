using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Lookups
{
    public class MatrizCurricularLookupPrepareFilter : ISMCFilter<MatrizCurricularLookupFiltroViewModel>
    {
        public MatrizCurricularLookupFiltroViewModel Filter(SMCControllerBase controllerBase, MatrizCurricularLookupFiltroViewModel filter)
        {
            filter.CursoLeitura = filter.SeqCurso != null && filter.SeqCurso.Seq > 0;

            if (filter.SeqCurso != null && filter.SeqCurso.Seq > 0)
            {
                var curriculoService = controllerBase.Create<ICurriculoService>();
                filter.Curriculos = curriculoService.BuscarCurriculoPorCursoSelect(filter.SeqCurso.Seq.Value);
            }

            filter.CurriculoParametro = filter.SeqCurriculo != null && filter.SeqCurriculo > 0;

            filter.CursoOfertaLeitura = filter.SeqCursoOferta != null && filter.SeqCursoOferta.Seq > 0;

            if (filter.SeqCursoOferta != null && filter.SeqCursoOferta.Seq > 0)
            {
                var modalidadeService = controllerBase.Create<IModalidadeService>();
                filter.Modalidades = modalidadeService.BuscarModalidadesPorCursoOfertaSelect(filter.SeqCursoOferta.Seq.Value);
            }
            else
            {
                var instituicaoModalidadeService = controllerBase.Create<IInstituicaoNivelModalidadeService>();
                filter.Modalidades = instituicaoModalidadeService.BuscarModalidadesPorInstituicaoSelect();
            }

            filter.ModalidadeLeitura = filter.SeqModalidade != null && filter.SeqModalidade > 0;

            filter.CodigoLeitura = !string.IsNullOrEmpty(filter.Codigo);

            filter.DescricaoLeitura = !string.IsNullOrEmpty(filter.Descricao);

            return filter;
        }
    }
}