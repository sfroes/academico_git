using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Lookups
{
    public class DispensaComponenteCurricularLookupPrepareFilter : ISMCFilter<DispensaComponenteCurricularLookupFiltroViewModel>
    {
        public DispensaComponenteCurricularLookupFiltroViewModel Filter(SMCControllerBase controllerBase, DispensaComponenteCurricularLookupFiltroViewModel filter)
        {

            var cursoOfertaService = controllerBase.Create<ICursoOfertaService>();
            filter.OfertasCurso = cursoOfertaService.BuscarCursosOfertasPorAlunoHistoricoEscolarSelect(filter.SeqPessoaAtuacao) ?? new List<SMCDatasourceItem>();
            
            return filter;
        }      
    }
}