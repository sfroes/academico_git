using SMC.Framework.UI.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.UI.Mvc.Areas.APR.Models
{
    public class DiarioTurmaCabecalhoViewModel : SMCViewModelBase
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
