using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class DisciplinaCursadaVO : ISMCMappable
    {
        public long? SeqHistoricoEscolar { get; set; }

        public string DescricaoCicloLetivo { get; set; }

        public string DescricaoComponenteCurricular { get; set; }

        public string DescricaoComponenteCurricularAssunto { get; set; }

        public string ListaProfessores { get; set; }

        public short? CargaHorariaAula { get; set; }

        public short? CargaHorariaRelogio { get; set; }

        public short? Credito { get; set; }

        public string Nota { get; set; }

        public string PercentualFrequencia { get; set; }

        public string DescricaoEscalaApuracaoItem { get; set; }

        public short? TipoArredondamento { get; set; }

        public DateTime? DataInicioEmenta { get; set; }

        public DateTime? DataFimEmenta { get; set; }

        public string Ementa { get; set; }
    }
}