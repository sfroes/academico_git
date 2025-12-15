using SMC.Academico.Common.Areas.ALN.Exceptions;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using System.Linq;

namespace SMC.Academico.UI.Mvc.Areas.ALN.Lookups
{
    public class TermoIntercambioLookupPrepareFilter : ISMCFilter<TermoIntercambioLookupFiltroViewModel>
    {
        public TermoIntercambioLookupFiltroViewModel Filter(SMCControllerBase controllerBase, TermoIntercambioLookupFiltroViewModel filter)
        {
            //if (!string.IsNullOrEmpty(filter.Cpf) && !string.IsNullOrEmpty(filter.NumeroPassaporte))
            //    throw new CPFPassaporteTermoIntercambioLookupException();

            if ((!string.IsNullOrEmpty(filter.Cpf) || !string.IsNullOrEmpty(filter.NumeroPassaporte)) &&
                (!filter.TipoMobilidade.HasValue || !filter.SeqInstituicaoEnsino.HasValue || !filter.SeqNivelEnsino.HasValue || !filter.SeqTipoVinculoAluno.HasValue))
                throw new TipoMobilidadeTermoIntercambioLookupException();

            if ((string.IsNullOrEmpty(filter.Cpf) && string.IsNullOrEmpty(filter.NumeroPassaporte)) &&
                (filter.TipoMobilidade.HasValue || filter.SeqInstituicaoEnsino.HasValue || filter.SeqNivelEnsino.HasValue || filter.SeqTipoVinculoAluno.HasValue))
                throw new TermoIntercambioParametrosInvalidosException();

            filter.TiposTermosIntercambios = controllerBase.Create<ITipoTermoIntercambioService>().BuscarTiposTermosIntercambiosSelect();
			if (filter.SeqTipoTermoIntercambio.HasValue)
				filter.TiposTermosIntercambios = filter.TiposTermosIntercambios.Where(e => e.Seq == filter.SeqTipoTermoIntercambio).ToList();
            return filter;
        }
    }
}