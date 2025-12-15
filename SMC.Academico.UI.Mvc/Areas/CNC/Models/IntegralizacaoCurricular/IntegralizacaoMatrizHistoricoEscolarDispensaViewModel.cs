using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.CNC.Models
{
    public class IntegralizacaoMatrizHistoricoEscolarDispensaViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }

        public long? SeqSolicitacaoDispensa { get; set; }

        public string ProtocoloSolicitacaoDispensa { get; set; }

        [SMCValueEmpty("-")]
        public string DescricaoCicloLetivo { get; set; }

        public string SituacaoHistoricoEscolar { get; set; }

        [SMCDecimalDigits(1)]
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
