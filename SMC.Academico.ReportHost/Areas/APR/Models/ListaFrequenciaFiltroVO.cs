using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ReportHost.Areas.TUR.Models
{
    public class ListaFrequenciaFiltroVO : ISMCMappable
    {
        public string TurmaDescricao { get; set; }

        public string DescricaoOfertaTurno { get; set; }

        public List<string> DescricoesCursoOfertaLocalidadeTurno { get; set; }

        public DateTime? DiaEmissao { get; set; }

        public DateTime? HoraInicio { get; set; }

        public DateTime? HoraFim { get; set; }

        public List<long> Colaboradores { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public long? SeqDivisaoTurma { get; set; }

        public string QuantidadeAula { get; set; }
    }
}