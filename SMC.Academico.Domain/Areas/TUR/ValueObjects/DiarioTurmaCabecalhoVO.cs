using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.TUR.ValueObjects
{
    public class DiarioTurmaCabecalhoVO : ISMCMappable
    {
        public string NomeCursoOfertaLocalidade { get; set; }

		public string DescricaoTurno { get; set; }

        public string CodigoTurma { get; set; }

        public string DescricaoTurmaConfiguracaoComponente { get; set; }

        public string DescricaoCicloLetivo { get; set; }

        public bool? IndicadorDiarioFechado { get; set; }

        public DateTime? DataFechamentoDiario { get; set; }
    }
}
