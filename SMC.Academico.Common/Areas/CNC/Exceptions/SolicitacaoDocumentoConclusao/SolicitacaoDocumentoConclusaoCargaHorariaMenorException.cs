using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions.SolicitacaoDocumentoConclusao
{
    public class SolicitacaoDocumentoConclusaoCargaHorariaMenorException : SMCApplicationException
    {
        public SolicitacaoDocumentoConclusaoCargaHorariaMenorException()
            : base(ExceptionsResource.ERR_SolicitacaoDocumentoConclusaoCargaHorariaMenorException)
        { }
    }
}