using SMC.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.SGA.Administrativo.Areas.OFC.Services
{
    public interface IOFCDynamicControllerService
    {
        List<SMCSelectItem> BuscarFormasIngressoSelect();

        List<SMCSelectItem> BuscarTiposVinculoSelect(long? seqFormaIngresso = null);

        List<SMCSelectItem> BuscarRegimesLetivosSelect();
    }
}
