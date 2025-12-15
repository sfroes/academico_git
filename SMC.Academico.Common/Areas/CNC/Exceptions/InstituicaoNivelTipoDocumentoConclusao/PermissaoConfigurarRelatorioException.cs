using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions.InstituicaoNivelTipoDocumentoConclusao
{
    public class PermissaoConfigurarRelatorioException : SMCApplicationException
    {
        public PermissaoConfigurarRelatorioException(string descricaoTipoDocumentoAcademico, string descricaoNivelEnsino)
            : base(string.Format(ExceptionsResource.ERR_PermissaoConfigurarRelatorioException, descricaoTipoDocumentoAcademico, descricaoNivelEnsino))
        { }
    }
}
