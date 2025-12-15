using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.TUR.Data
{
    public class DiarioTurmaCabecalhoData : ISMCMappable
    {
        public string NomeCursoOfertaLocalidade { get; set; }

        public string DescricaoTurno { get; set; }

        public string CodigoTurma { get; set; }

        public string DescricaoTurmaConfiguracaoComponente { get; set; }

        public string DescricaoCicloLetivo { get; set; }

        public bool? IndicadorDiarioFechado { get; set; }

        public DateTime? DataFechamentoDiario { get; set; }

        public string TurmaCabecalho
        {
            get
            {
                return string.Join(" - ", new object[] { NomeCursoOfertaLocalidade, DescricaoTurno }) + Environment.NewLine
                       + string.Join(" - ", new object[] { CodigoTurma, DescricaoTurmaConfiguracaoComponente });
            }
        }

        public string Curso
        {
            get
            {
                return string.Join(" - ", new object[] { NomeCursoOfertaLocalidade, DescricaoTurno });
            }
        }

        public string Disciplina
        {
            get
            {
                return string.Join(" - ", new object[] { CodigoTurma, DescricaoTurmaConfiguracaoComponente });
            }
        }

        public string TurmaCabecalhoRelatorio { get; set; }
    }
}
