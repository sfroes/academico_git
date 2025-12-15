using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Interfaces
{
    public interface IClassificacaoPeriodicoService : ISMCService
    {
        List<SMCDatasourceItem> BuscarClassificacaoPeriodicoSelect();
    }
}