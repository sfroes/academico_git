using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CNC.Interfaces
{
    public interface IClassificacaoInvalidadeDocumentoService : ISMCService
    {
        List<SMCDatasourceItem> BuscarDadosSelectClassificacaoInvalidadeDocumento(TipoInvalidade? tipoInvalidade);
    }
}
