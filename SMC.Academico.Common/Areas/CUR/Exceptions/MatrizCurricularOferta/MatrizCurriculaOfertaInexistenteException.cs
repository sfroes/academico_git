using SMC.Framework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMC.Academico.Common.Areas.CUR.Resources;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class MatrizCurriculaOfertaInexistenteException : SMCApplicationException
    {
        public MatrizCurriculaOfertaInexistenteException(string descricao, DateTime data) 
            : base(string.Format(ExceptionsResource.ERR_MatrizCurriculaOfertaInexistenteException, descricao, data.ToShortDateString()))
        { }
    }
}
