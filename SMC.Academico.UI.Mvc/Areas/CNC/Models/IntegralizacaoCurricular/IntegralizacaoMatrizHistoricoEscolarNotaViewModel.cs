using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.CNC.Models
{
    public class IntegralizacaoMatrizHistoricoEscolarNotaViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }
                
        [SMCValueEmpty("-")]
        public string DescricaoCicloLetivo { get; set; }

        public SituacaoHistoricoEscolar SituacaoHistoricoEscolar { get; set; }

        [SMCDecimalDigits(0)]
        [SMCValueEmpty("-")]
        public decimal? Nota { get; set; }

        [SMCValueEmpty("-")]
        public string DescricaoConceito { get; set; }

        [SMCValueEmpty("-")]
        public short? Faltas { get; set; }

        [SMCValueEmpty("-")]
        public string CodigoTurma { get; set; }

        public List<string> Professores { get; set; }

        [SMCValueEmpty("-")]
        public string ProfessorTexto { get; set; }

    }
}
