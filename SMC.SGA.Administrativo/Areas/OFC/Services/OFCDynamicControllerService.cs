using SMC.Academico.Service.OFC;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.Controllers.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.SGA.Administrativo.Areas.OFC.Services
{
    public class OFCDynamicControllerService : SMCControllerServiceBase, IOFCDynamicControllerService
    {

        #region Services

        public IOFCDynamicService OFCDynamicService 
        {
            get 
            {
                return this.Create<IOFCDynamicService>();
            }
        }

        #endregion

        public List<SMCSelectItem> BuscarFormasIngressoSelect() 
        {
            return this.OFCDynamicService.BuscarFormasIngressoSelect();
        }

        public List<SMCSelectItem> BuscarTiposVinculoSelect(long? seqFormaIngresso = null) 
        {
            return this.OFCDynamicService.BuscarTiposVinculoSelect();
        }

        public List<SMCSelectItem> BuscarRegimesLetivosSelect() 
        {
            return this.OFCDynamicService.BuscarRegimesLetivosSelect();
        }
    }
}
