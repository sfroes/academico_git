using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.APR.Models
{
    public class ListaFrequenciaFiltroViewModel : SMCViewModelBase
    {
        #region [Datasource]       

        public List<SMCDatasourceItem> GradeHoraria { get; set; }      

        #endregion

        public string TurmaDescricao { get; set; }

		public string DescricaoOfertaTurno { get; set; }

		public List<string> DescricoesCursoOfertaLocalidadeTurno { get; set; }

		[SMCConditionalDisplay(nameof(DisponibilizarGrade), false)]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid6_24)]
        public DateTime? DiaEmissao { get; set; }

        [SMCConditionalDisplay(nameof(DisponibilizarGrade), false)]
        [SMCDateTimeMode(SMCDateTimeMode.Time)]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid4_24)]
        public DateTime? HoraInicio { get; set; }

        [SMCConditionalDisplay(nameof(DisponibilizarGrade), false)]
        [SMCDateTimeMode(SMCDateTimeMode.Time)]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid4_24)]
        public DateTime? HoraFim { get; set; }

        public SMCPagerModel<ListaFrequenciaColaboradorViewModel> Colaboradores { get; set; }

        [SMCHidden]
        public long? SeqNivelEnsino { get; set; }

        [SMCHidden]
        public long? SeqDivisaoTurma { get; set; }

        [SMCHidden]
        public bool DisponibilizarGrade { get; set; }

        [SMCConditionalDisplay(nameof(DisponibilizarGrade), true)]
        [SMCFilter(true, true)]
        [SMCSelect(nameof(GradeHoraria))]
        [SMCSize(SMCSize.Grid14_24)]
        public long? SeqGradeHoraria { get; set; }

        [SMCHidden]
        public DateTime? DataInicioPeriodoLetivo { get; set; }

        [SMCHidden]
        public DateTime? DataFimPeriodoLetivo { get; set; }
    }
}
