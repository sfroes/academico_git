using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.TUR.Data
{
    public class DivisaoTurmaDetalhesData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqTurma { get; set; }

        public string TurmaDescricaoFormatado { get; set; }

        public string DescricaoFormatada { get; set; }

		public string NomeCursoOfertaLocalidade { get; set; }

		public List<string> DescricoesCursoOfertaLocalidadeTurno { get; set; }

		public string DescricaoTurno { get; set; }

		public string GrupoFormatado { get; set; }

        public string DescricaoLocalidade { get; set; }

        public short? QuantidadeVagas { get; set; }

        public string InformacoesAdicionais { get; set; }

        public string DescricaoConfiguracaoComponente { get; set; }

        public string TipoDivisaoDescricao { get; set; }

        public List<DivisaoTurmaDetalhesColaboradorData> Colaboradores { get; set; }

        public bool GeraOrientacao { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public bool DiarioFechado { get; set; }

        public short? CargaHoraria { get; set; }

        public short? CargaHorariaGrade { get; set; }
        
        public bool Destaque { get; set; }

        public long SeqComponenteCurricular { get; set; }

        public bool? TurmaPossuiAgenda { get; set; }

        public DateTime? DataInicioPeriodoLetivo { get; set; }

        public DateTime? DataFimPeriodoLetivo { get; set; }

        public long SeqOrigemAvaliacao { get; set; }
    }
}
