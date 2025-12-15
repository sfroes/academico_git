using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.APR.Models
{
    public class AulaViewModel : SMCViewModelBase
    {
        [SMCIgnoreProp]
        public string DescricaoTurma { get; set; }

        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public string BackUrl { get; set; }

        [SMCHidden]
        public long SeqDivisaoTurma { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24)]
        public DateTime DataAula { get; set; }

        [SMCHidden]
        public bool AlunoFormado => Ofertas?.SMCAny(a => a.ApuracoesFrequencia?.SMCAny(af => af.AlunoFormado) ?? false) ?? false;

        [SMCSize(SMCSize.Grid24_24)]
        public List<AulaOfertaViewModel> Ofertas { get; set; }

		[SMCIgnoreProp]
		public string DescricaoOfertaTurno { get; set; }

		[SMCIgnoreProp]
		public List<string> DescricoesCursoOfertaLocalidadeTurno { get; set; }
	}
}