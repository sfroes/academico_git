using SMC.Academico.Common.Areas.APR.Resources;
using SMC.Framework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Common.Areas.APR.Exceptions
{
    public class DivisaoTurmaDuracaoAulaException : SMCApplicationException
    {
        public DivisaoTurmaDuracaoAulaException(string duracaoMinima, string duracaoMaxima)
            : base(string.Format(ExceptionsResource.UN_divisao_turma_duracao_aula, duracaoMinima, duracaoMaxima))
        {
        }
    }
}
