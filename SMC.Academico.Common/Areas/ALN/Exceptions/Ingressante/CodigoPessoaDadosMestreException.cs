using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class CodigoPessoaDadosMestreException : Framework.Exceptions.SMCApplicationException
    {
        public CodigoPessoaDadosMestreException(TipoPessoa tipo, long seqPessoa)
            : base(string.Format(Resources.ExceptionsResource.ERR_CodigoPessoaDadosMestreException, tipo.SMCGetDescription().ToLower(), seqPessoa))
        {
        }
    }
}