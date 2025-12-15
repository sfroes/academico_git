using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class RequisitoListaItensDuplicadosException : SMCApplicationException
    {
        public RequisitoListaItensDuplicadosException()
            : base(ExceptionsResource.ERR_RequisitoListaItensDuplicados)
        {
        }
    }
}