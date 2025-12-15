using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Lookups
{
    /// <summary>
    /// Lookup de QualisPeriodico.
    /// </summary>
    public class PeriodicoCapesLookupAttribute : SMCLookupAttribute
    {
        /// <summary>
        /// Cria um novo lookup de periódico.
        /// </summary>
        /// <param name="configuracaoAtual">Informa se quer trazer apenas periódicos cuja configuração é a atual.</param>
        public PeriodicoCapesLookupAttribute(bool configuracaoAtual)
            : base("Periodico")
        {
            Filter = typeof(PeriodicoCapesLookupFiltroViewModel);
            if (configuracaoAtual)
            {
                PrepareFilter = typeof(PeriodicoCapesLookupPrepareFilter);
            }
            Model = typeof(PeriodicoCapesLookupListaViewModel);
            Service<IPeriodicoService>(nameof(IPeriodicoService.BuscarPeriodicosLookup));
            SelectService<IPeriodicoService>(nameof(IPeriodicoService.BuscarPeriodicoLookup));
            HideSeq = true;
        }
    }
}
