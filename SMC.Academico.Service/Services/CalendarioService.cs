using SMC.Academico.ServiceContract.Interfaces;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Service.Services
{
    public class CalendarioService : SMCServiceBase, ICalendarioService
    {
        private Calendarios.ServiceContract.Areas.CLD.Interfaces.ICalendarioService CalendarioServiceAGD
        {
            get { return Create<Calendarios.ServiceContract.Areas.CLD.Interfaces.ICalendarioService>(); }
        }

        public List<SMCDatasourceItem> BuscarCalendariosUnidadeResponsavelSelect(long seqUnidadeResponsavelAgd)
        {
            return CalendarioServiceAGD.BuscarCalendariosUnidadeResponsavelSelect(seqUnidadeResponsavelAgd);
        }

        public string BuscarNomeCalendario(long seqCalendario)
        {
            return CalendarioServiceAGD.BuscarNomeCalendario(seqCalendario);
        }
    }
}
