using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.APR.Models
{
    public class AulaFiltroViewModel : SMCPagerViewModel
    {
        [SMCHidden]
        public long? Seq { get; set; }

        [SMCHidden]
        public string BackUrl { get; set; }

        [SMCHidden]
        public long SeqDivisaoTurma { get; set; }

        [SMCHidden]
        public bool GeraOrientacao { get; set; }

        [SMCSize(SMCSize.Grid3_24)]
        [SMCOrder(0)]
        [SMCMaxDate(nameof(DataFim))]
        public DateTime? DataInicio { get; set; }

        [SMCSize(SMCSize.Grid3_24)]
        [SMCMinDate(nameof(DataInicio))]
        [SMCConditionalReadonly(nameof(DataInicio), SMCConditionalOperation.Equals, "")]
        [SMCOrder(1)]
        public DateTime? DataFim { get; set; }

        [SMCIgnoreProp]
        public string DescricaoTurma { get; set; }

		[SMCIgnoreProp]
		public string DescricaoOfertaTurno { get; set; }

		[SMCIgnoreProp]
		public List<string> DescricoesCursoOfertaLocalidadeTurno { get; set; }

		[SMCRadioButtonList]
        [SMCSize(SMCSize.Grid24_24)]
        public bool AgruparAluosCurso { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        public SMCPagerModel<ListaFrequenciaColaboradorViewModel> Colaboradores { get; set; }
    }
}