using SMC.Framework.Model;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.CUR.Interfaces
{
    public interface IQualisPeriodicoService : ISMCService
    {
        List<SMCDatasourceItem> BuscarAreaAvaliacaoSelect();
    }
}
