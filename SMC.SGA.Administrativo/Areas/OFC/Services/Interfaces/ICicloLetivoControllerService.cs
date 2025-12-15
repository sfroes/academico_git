using SMC.Framework.Model;
using SMC.SGA.Administrativo.Areas.OFC.Models;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.OFC.Services
{
    public interface ICicloLetivoControllerService
    {

        SMCPagerData<CicloLetivoListaViewModel> BuscarCiclosLetivos(CicloLetivoFiltroViewModel filtros);

        CicloLetivoViewModel BuscarCicloLetivo(long seqCicloLetivo);

        long SalvarCicloLetivo(CicloLetivoViewModel modelo);

        void ExcluirCicloLetivo(long seqCicloLetivo);        

        List<SMCSelectItem> BuscarCiclosLetivosSelect();
    }
}