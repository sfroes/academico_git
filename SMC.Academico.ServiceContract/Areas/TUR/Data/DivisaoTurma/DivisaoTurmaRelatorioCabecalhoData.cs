using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.TUR.Data
{
    public class DivisaoTurmaRelatorioCabecalhoData : ISMCMappable
    {
        public long SeqDivisaoTurma { get; set; }

        public string NomeCursoOfertaLocalidade { get; set; }

        public string DescricaoTurno { get; set; }

        public string DescricaoConfiguracaoComponente { get; set; }

        public string DescricaoComponenteCurricularAssunto { get; set; }

        public short? CargaHorariaComponenteCurricular { get; set; }

        public short? CreditoComponenteCurricular { get; set; }

        public string CodigoDivisaoFormatado { get; set; }

        public string DescricaoComponenteCurricularOrganizacao { get; set; }

        public string TipoDivisaoDescricao { get; set; }

        public short? CargaHoraria { get; set; }

        public FormatoCargaHoraria? FormatoCargaHoraria { get; set; }

        public string Curso { get; set; }

        public string Disciplina { get; set; }
    }
}
