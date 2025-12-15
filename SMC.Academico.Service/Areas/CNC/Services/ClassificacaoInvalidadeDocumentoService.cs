using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Domain.Areas.CNC.DomainServices;
using SMC.Academico.ServiceContract.Areas.CNC.Interfaces;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CNC.Services
{
    public class ClassificacaoInvalidadeDocumentoService : SMCServiceBase, IClassificacaoInvalidadeDocumentoService
    {
        private ClassificacaoInvalidadeDocumentoDomainService ClassificacaoInvalidadeDocumentoDomainService => this.Create<ClassificacaoInvalidadeDocumentoDomainService>();

        public List<SMCDatasourceItem> BuscarDadosSelectClassificacaoInvalidadeDocumento(TipoInvalidade? tipoInvalidade)
        {
            return ClassificacaoInvalidadeDocumentoDomainService.BuscarDadosSelectClassificacaoInvalidadeDocumento(tipoInvalidade);
        }
    }
}
