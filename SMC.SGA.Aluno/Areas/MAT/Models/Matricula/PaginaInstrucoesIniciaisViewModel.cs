using SMC.Academico.Common.Areas.ALN.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Aluno.Areas.MAT.Models.Matricula
{
    public class PaginaInstrucoesIniciaisViewModel : MatriculaPaginaViewModelBase
    {
        public override string Token => MatriculaTokens.INSTRUCOES_INICIAIS_MATRICULA;
    }
}