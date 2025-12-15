using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;


namespace SMC.Academico.ServiceContract.Areas.ALN.Interfaces
{
    [ServiceContract(Namespace = NAMESPACES.SERVICE)]
    public interface IParceriaIntercambioInstituicaoExternaService : ISMCService
    { 

        List<SMCDatasourceItem> BuscarParceriaIntercambioInstituicoesExternasSelect(long SeqParceriaIntercambio, bool? ativo = null);
    }
}

 