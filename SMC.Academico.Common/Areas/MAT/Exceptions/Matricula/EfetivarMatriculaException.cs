using SMC.Academico.Common.Areas.ALN.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.MAT.Exceptions.Matricula
{
    public class EfetivarMatriculaException : SMCApplicationException
    {
        public EfetivarMatriculaException(string erro)
                                        : base(string.Format(ExceptionsResource.ERR_EfetivacaoMatriculaException, erro))
        {}
    }
}